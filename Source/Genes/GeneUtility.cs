using System;
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
    }
}