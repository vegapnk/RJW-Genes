using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace RJW_BGS
{
    public class InheritanceUtility
    {
        public static List<GeneDef> AnimalInheritedGenes(Pawn father, Pawn mother)
        {
            //One parent must be an animal and the other must be human, so only one needs to return
            List<GeneDef> genelist = new List<GeneDef>();
            if (father != null && !father.RaceProps.Humanlike)
            {
                return SelectGenes(father);
            }

            if (mother != null && !mother.RaceProps.Humanlike)
            {
                return SelectGenes(mother);
                //PawnKindDef pawnKindDef = mother.kindDef;
                //RaceGeneDef raceGeneDef = RJWcopy.GetRaceGenDefInternal(pawnKindDef);
                //if (raceGeneDef != null)
                //{
                //    GeneDef gene = null;
                    //In case you hit a modded gene not currently active try again.
                //    for (int i = 0; i < 50 || gene == null; i++)
                //    {
                //        if (raceGeneDef.genes.Any())
                //        {
                //            gene = DefDatabase<GeneDef>.GetNamed(raceGeneDef.genes.RandomElement());
                //        }
                //    }
                //    if (gene != null)
                //    {
                //        genelist.Add(gene);
//
  //                  }
    //                
      //          }
            }
            return genelist;
        }

        public static List<GeneDef> SelectGenes(Pawn pawn)
        {
            List<GeneDef> genelist = new List<GeneDef>();
            PawnKindDef pawnKindDef = pawn.kindDef;
            RaceGeneDef raceGeneDef = RJWcopy.GetRaceGenDefInternal(pawnKindDef);
            if (raceGeneDef != null)
            {
                foreach (GeneChance gene in raceGeneDef.genes)
                {
                    if (gene.chance >= Rand.Range(0.01f,1f))
                    {
                        genelist.Add(DefDatabase<GeneDef>.GetNamed(gene.defName));
                    }
                }
            }
            return genelist;
        }

        public static void AddGenes(Pawn pawn, List<GeneDef> genes)
        {
            foreach (GeneDef gene in genes)
            {
                pawn.genes.AddGene(gene, false);
            }
        }

        public static void NewGenes(Pawn mother, Pawn dad, Pawn baby)
        {
            if (baby.RaceProps.Humanlike)
            {
                if (baby.genes == null)
                {
                    baby.genes = new Pawn_GeneTracker(baby);
                }

                //Remove the hair and skin genes pawns always start with, should get correct ones from human parent anyway.
                for (int i = baby.genes.Endogenes.Count - 1; i >= 0; i--)
                {
                    baby.genes.RemoveGene(baby.genes.Endogenes[i]);
                }

                List<GeneDef> genes = PregnancyUtility.GetInheritedGenes(dad, mother);
                List<GeneDef> beastgenes = InheritanceUtility.AnimalInheritedGenes(dad, mother);
                InheritanceUtility.AddGenes(baby, beastgenes);
                InheritanceUtility.AddGenes(baby, genes);
                if(baby.genes.GetFirstEndogeneByCategory(EndogeneCategory.Melanin) == null)
                {
                    AddSkinColor(mother, dad, baby);
                }
            }
        }

        public static void AddSkinColor(Pawn mother, Pawn father, Pawn baby)
        {
            if (mother != null && mother.genes != null)
            {
                GeneDef gene = mother.genes.GetFirstEndogeneByCategory(EndogeneCategory.Melanin);
                if (gene != null)
                {
                    baby.genes.AddGene(gene, false);
                }
            }
            else if (father != null && father.genes != null)
            {
                GeneDef gene = father.genes.GetFirstEndogeneByCategory(EndogeneCategory.Melanin);
                if (gene != null)
                {
                    baby.genes.AddGene(gene, false);
                }
            }
            else
            {
                Log.Message("Could not find skincolor of " + baby.Name + "'s parents, giving random skincolor.");
                baby.genes.AddGene(PawnSkinColors.RandomSkinColorGene(baby), false);
            }
        }

    }
}
