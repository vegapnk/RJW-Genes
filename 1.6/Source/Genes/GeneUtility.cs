using System;
using System.Collections.Generic;
using Verse;
using RimWorld;
namespace RJW_Genes
{
    public class GeneUtility
    {

        //Split function so I can offsetlifeforce from gene without needing to look for the gene agian (for the constant drain tick)
        public static Gene_LifeForce GetLifeForceGene(Pawn pawn)
        {
            Pawn_GeneTracker genes = pawn.genes;
            Gene_LifeForce gene_LifeForce = genes.GetFirstGeneOfType<Gene_LifeForce>();
            return gene_LifeForce;
        }

        public static void OffsetLifeForce(IGeneResourceDrain drain, float offset)
        {
            if (drain == null || offset == 0.0)
                return;

            if (drain.Resource != null && drain.Resource.Active)
            {
                float old_value = drain.Resource.Value;
                drain.Resource.Value += offset;
                PostOffSetLifeForce(drain, old_value);
            }
        }

        public static void PostOffSetLifeForce(IGeneResourceDrain drain, float old_value)
        {

            if (drain.Resource != null && drain.Resource.Active)
            {
                if (old_value > 0.2f && drain.Resource.Value <= 0.2f)
                {
                    //TODO: Mood debuff
                }
                else if (old_value > 0f && drain.Resource.Value <= 0f)
                {
                    Pawn pawn = drain.Pawn;
                    if (!drain.Pawn.health.hediffSet.HasHediff(HediffDefOf.rjw_genes_fertilin_craving))
                    {
                        drain.Pawn.health.AddHediff(HediffDefOf.rjw_genes_fertilin_craving);
                    }
                }
            }
        }


        public static bool HasLowLifeForce(Pawn pawn)
        {
            if (HasLifeForce(pawn))
            {
                Gene_LifeForce gene = pawn.genes.GetFirstGeneOfType<Gene_LifeForce>();
                if (gene == null || !gene.Active)
                    return false;
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
                if (gene == null || !gene.Active)
                    return false;
                if (gene.Resource.Value < gene.MinLevelForAlert)
                {
                    return true;
                }
            }
            return false;
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
            return pawn.genes.HasActiveGene(genedef);
        }

        public static bool HasLifeForce(Pawn pawn) { return HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_lifeforce); }
        public static bool IsMechbreeder(Pawn pawn) { return HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_mechbreeder); }
        public static bool IsYouthFountain(Pawn pawn) { return HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_youth_fountain); }
        public static bool IsAgeDrainer(Pawn pawn) { return HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_sex_age_drain); }
        public static bool IsElastic(Pawn pawn) { return HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_elasticity); }
        public static bool IsGenerousDonor(Pawn pawn) { return HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_generous_donor); }
        public static bool IsPussyHealer(Pawn pawn) { return HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_pussyhealing); }
        public static bool IsUnbreakable(Pawn pawn) { return HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_unbreakable); }
        public static bool HasParalysingKiss(Pawn pawn) { return HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_paralysingkiss); }
        public static bool HasSeduce(Pawn pawn) { return HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_seduce); }
        public static bool IsSexualDrainer(Pawn pawn) { return HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_drainer); }
        public static bool IsCumEater(Pawn pawn) { return HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_cum_eater); }

    }
}
