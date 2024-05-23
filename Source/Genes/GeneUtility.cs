using System;
using System.Collections.Generic;
using Verse;
using RimWorld;
namespace RJW_Genes
{
    public class GeneUtility
    {
        
        public static float MaxEggSizeMul(Pawn pawn)
        {
            float MaxEggSize = 1;
          
            return MaxEggSize;
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

        /// <summary>
        /// Unified small check for a pawn if it has a specified Gene. 
        /// Handles some errors and returns false as default.
        /// </summary>
        /// <param name="pawn">The pawn for which to look up a gene.</param>
        /// <param name="genedef">The gene to look up.</param>
        /// <returns></returns>
        public static bool HasGeneNullCheck(Pawn pawn, GeneDef genedef)
        {
            if (pawn.genes == null)
            {
                return false;
            }
            return pawn.genes.HasGene(genedef);
        }

        public static bool IsMechbreeder(Pawn pawn) { return HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_mechbreeder); }
        public static bool IsYouthFountain(Pawn pawn) { return HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_youth_fountain); }
        public static bool IsAgeDrainer(Pawn pawn) { return HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_sex_age_drain); }
        public static bool IsElastic(Pawn pawn) { return HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_elasticity); }
        public static bool IsCumflationImmune(Pawn pawn) { return HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_cumflation_immunity); }
        public static bool IsGenerousDonor(Pawn pawn) { return HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_generous_donor); }
        public static bool IsUnbreakable(Pawn pawn) { return HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_unbreakable); }

    }
}
