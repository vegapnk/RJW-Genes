using System;
using System.Collections.Generic;
using Verse;

namespace RJW_Genes
{
    public class GeneUtility
    {
        public static bool IsMechbreeder(Pawn pawn)
        {
            if (pawn.genes == null)
            {
                return false;
            }
            return pawn.genes.HasGene(GeneDefOf.rjw_genes_mechbreeder);
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

        public static bool IsUnbreakable(Pawn pawn)
        {
            if (pawn.genes == null)
            {
                return false;
            }
            return pawn.genes.HasGene(GeneDefOf.rjw_genes_unbreakable);
        }


        public static bool HasGenitaliaResizingGenes(Pawn pawn)
        {
            return !GetGenitaliaResizingGenes(pawn).NullOrEmpty();
        }

        public static List<Gene> GetGenitaliaResizingGenes(Pawn pawn)
        {
            var ResizingGenes = new List<Gene>();

            // Error Handling: Issue with Pawn or Genes return empty.
            if (pawn == null || pawn.genes == null)
                return ResizingGenes;

            foreach (Gene g in pawn.genes.GenesListForReading)
                if (g is Gene_GenitaliaResizingGene)
                    ResizingGenes.Add(g);

            return ResizingGenes;
        }
    }
}