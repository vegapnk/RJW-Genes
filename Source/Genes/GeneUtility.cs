using System;
using System.Collections.Generic;
using Verse;
using RimWorld;
namespace RJW_Genes
{
    public class GeneUtility
    {
        //Instead of seperate functions this should be simpeler
        public static bool HasGeneNullCheck(Pawn pawn, GeneDef genedef)
        {
            if (pawn.genes == null)
            {
                return false;
            }
            return pawn.genes.HasGene(genedef);
        }

        //Split function so I can offsetlifeforce from gene without needing to look for the gene agian (for the constant drain tick)
        public static Gene_LifeForce GetLifeForceGene(Pawn pawn)
        {
            Pawn_GeneTracker genes = pawn.genes;
            Gene_LifeForce gene_LifeForce = genes.GetFirstGeneOfType<Gene_LifeForce>();
            return gene_LifeForce;
        }

        public static void OffsetLifeForce(IGeneResourceDrain drain, float offset)
        {                
            float old_value = drain.Resource.Value;
            drain.Resource.Value += offset;
            PostOffSetLifeForce(drain, old_value);     
        }

        public static void PostOffSetLifeForce(IGeneResourceDrain drain, float old_value)
        {
            if (old_value > 0.2f && drain.Resource.Value <= 0.2f)
            {                
                //Mood debuff
            }
            else if (old_value > 0f && drain.Resource.Value <= 0f)
            {
                if (!drain.Pawn.health.hediffSet.HasHediff(HediffDefOf.rjw_genes_fertilin_craving))
                {
                    drain.Pawn.health.AddHediff(HediffDefOf.rjw_genes_fertilin_craving);
                }
            }
        }

        public static bool IsMechbreeder(Pawn pawn)
        {
            if (pawn.genes == null)
            {
                return false;
            }
            return pawn.genes.HasGene(GeneDefOf.rjw_genes_mechbreeder);
        }

        public static bool HasLifeForce(Pawn pawn)
        {
            if (pawn.genes == null)
            {
                return false;
            }
            return pawn.genes.HasGene(GeneDefOf.rjw_genes_lifeforce);
        }

        public static bool HasLowLifeForce(Pawn pawn)
        {
            if (HasLifeForce(pawn))
            {
                Gene_LifeForce gene = pawn.genes.GetFirstGeneOfType<Gene_LifeForce>();
                if (gene.Resource.Value < gene.targetValue)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasCriticalLifeForce(Pawn pawn)
        {
            if (HasLifeForce(pawn))
            {
                Gene_LifeForce gene = pawn.genes.GetFirstGeneOfType<Gene_LifeForce>();
                if (gene.Resource.Value < gene.MinLevelForAlert)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsInsectIncubator(Pawn pawn)
        {
            if (pawn.genes == null)
            {
                return false;
            }
            return pawn.genes.HasGene(GeneDefOf.rjw_genes_insectincubator);
        }

        public static bool IsYouthFountain(Pawn pawn)
        {
            if (pawn.genes == null)
            {
                return false;
            }
            return pawn.genes.HasGene(GeneDefOf.rjw_genes_youth_fountain);
        }

        public static bool IsAgeDrainer(Pawn pawn)
        {
            if (pawn.genes == null)
            {
                return false;
            }
            return pawn.genes.HasGene(GeneDefOf.rjw_genes_sex_age_drain);
        }

        public static bool IsInsectBreeder(Pawn pawn)
        {
            if (pawn.genes == null)
            {
                return false;
            }
            return pawn.genes.HasGene(GeneDefOf.rjw_genes_insectbreeder);
        }

        public static float MaxEggSizeMul(Pawn pawn)
        {
            float MaxEggSize = 1;
            if (IsInsectIncubator(pawn))
            {
                MaxEggSize *= 2;
            }
            return MaxEggSize;
        }

        internal static bool IsElastic(Pawn pawn)
        {
            if (pawn.genes == null)
            {
                return false;
            }
            return pawn.genes.HasGene(GeneDefOf.rjw_genes_elasticity);
        }

        public static bool IsCumflationImmune(Pawn pawn)
        {
            if (pawn.genes == null)
            {
                return false;
            }
            return pawn.genes.HasGene(GeneDefOf.rjw_genes_cumflation_immunity);
        }
        public static bool IsGenerousDonor(Pawn pawn)
        {
            if (pawn.genes == null)
            {
                return false;
            }
            return pawn.genes.HasGene(GeneDefOf.rjw_genes_generous_donor);
        }

        public static bool isPussyHealer(Pawn pawn)
        {
            if (pawn.genes == null)
            {
                return false;
            }
            return pawn.genes.HasGene(GeneDefOf.rjw_genes_pussyhealing);
        }

        public static bool IsUnbreakable(Pawn pawn)
        {
            if (pawn.genes == null)
            {
                return false;
            }
            return pawn.genes.HasGene(GeneDefOf.rjw_genes_unbreakable);
        }

        public static List<Gene_GenitaliaResizingGene> GetGenitaliaResizingGenes(Pawn pawn)
        {
            var ResizingGenes = new List<Gene_GenitaliaResizingGene>();

            // Error Handling: Issue with Pawn or Genes return empty.
            if (pawn == null || pawn.genes == null)
                return ResizingGenes;

            foreach (Gene gene in pawn.genes.GenesListForReading)
                if (gene is Gene_GenitaliaResizingGene resizing_gene)
                    ResizingGenes.Add(resizing_gene);

            return ResizingGenes;
        }
    }
}