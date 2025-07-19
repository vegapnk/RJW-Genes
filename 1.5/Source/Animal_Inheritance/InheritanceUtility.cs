using System.Collections.Generic;
using Verse;
using RimWorld;
using rjw;
using RJW_Genes;
using System.Linq;

namespace RJW_BGS
{
    public class InheritanceUtility
    {
        public static List<GeneDef> AnimalInheritedGenes(Pawn father, Pawn mother)
        {
            List<GeneDef> genelist = new List<GeneDef>();
            //If Both are Humans, or Both are animals, do nothing & return empty GeneList 
            if (!mother.RaceProps.Humanlike && !father.RaceProps.Humanlike)
                return genelist;
            if (mother.RaceProps.Humanlike && father.RaceProps.Humanlike)
                return genelist;

            RJW_Genes.ModLog.Message($"Trigger an Animal-Gene-Inheritance for {father.Name} and {mother.Name}");
            //One parent must be an animal and the other must be human, so only one needs to return
            if (father != null && !father.RaceProps.Humanlike)
            {
                RJW_Genes.ModLog.Debug($"Father was found to be animal - looking up genes for {father.Name}");
                return SelectGenes(father);
            }
            if (mother != null && !mother.RaceProps.Humanlike)
            {
                RJW_Genes.ModLog.Debug($"Mother was found to be animal - looking up genes for {mother.Name}");
                return SelectGenes(mother);
            }

            return genelist;
        }

        /// <summary>
        /// Looks up potential genes for an animal, 
        /// checks their chance and returns all 'triggered' genes.
        /// </summary>
        /// <param name="pawn">The animal for which to look up genes (Animals are Pawns in RW)</param>
        /// <returns>The genes that will be inherited from this animal.</returns>
        public static List<GeneDef> SelectGenes(Pawn pawn)
        {
            List<GeneDef> genelist = new List<GeneDef>();
            RaceGeneDef raceGeneDef = RaceGeneDef_Helper.GetRaceGeneDefInternal(pawn);
            if (raceGeneDef != null)
            {
                foreach (BestialityGeneInheritanceDef gene in raceGeneDef.genes)
                {
                    if (gene.chance * RJW_BGSSettings.rjw_bgs_global_gene_chance  >= Rand.Range(0.01f,1f))
                    {
                        GeneDef tmpGene = DefDatabase<GeneDef>.GetNamed(gene.defName, false);
                        if (tmpGene != null) 
                        { 
                            genelist.Add(tmpGene); 
                        } 
                        else
                        {
                            RJW_Genes.ModLog.Warning($"Unable to find gene {gene.defName}, skipping. May need to update {raceGeneDef.defName} definition.");
                        }
                    }
                }
            }
            RJW_Genes.ModLog.Debug($"From {raceGeneDef.genes.Count} possible genes in {raceGeneDef.defName}, {genelist.Count} were added by chance ({RJW_BGSSettings.rjw_bgs_global_gene_chance} chance multiplier from Settings).");
            return genelist;
        }


        /// <summary>
        /// Adds a list of Genes to the pawns existing GeneSet. 
        /// Whether it is added as a Xenogene or Endogene is configured in Mod-Settings.
        /// </summary>
        /// <param name="pawn">The pawn for which Genes will be added</param>
        /// <param name="genes">The Genes to add (Endogene by default, Xenogene with Mod Settings)</param>
        public static void AddGenes(Pawn pawn, List<GeneDef> genes)
        {
            foreach (GeneDef gene in genes)
            {
                pawn.genes.AddGene(gene, RJW_BGSSettings.rjw_bgs_animal_genes_as_xenogenes);
            }
        }

        /// <summary>
        /// Initiates a bestiality baby with genes if the baby does not exist earlier. 
        /// This is used to make rjw-egg-pregnancies work. 
        /// Related file: PatchRJWHediffInsect_Egg.cs
        /// </summary>
        /// <param name="mother">The mother of the baby.</param>
        /// <param name="dad">The father of the baby.</param>
        /// <param name="baby">The baby created in non-pregnancy-way (has 0 Genes yet)</param>
        public static void NewGenes(Pawn mother, Pawn dad, Pawn baby)
        {
            if (!RJW_BGSSettings.rjw_bgs_enabled)
            {
                return;
            }

            RJW_Genes.ModLog.Message($"Triggering an New-Gene Animal-Gene-Inheritance for {baby.Name} ({dad.Name} + {mother.Name})");
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

        /// <summary>
        /// Used only for debugging, to see what you loaded and how it looks.
        /// </summary>
        private static void LogAllFoundRaceGroupGenes()
        {
            foreach (RaceGroupDef def in DefDatabase<RaceGroupDef>.AllDefs)
            {
                Log.Message("defName = " + def.defName);
                if (def.raceNames != null)
                {
                    foreach (string race in def.raceNames)
                    {
                        Log.Message(race);
                    }
                }
            }
        }
    }
}
