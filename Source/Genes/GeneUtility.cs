using System;
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
            Pawn_GeneTracker genes2 = pawn.genes;
            Gene_LifeForce gene_LifeForce = (genes2 != null) ? genes2.GetFirstGeneOfType<Gene_LifeForce>() : null;
            return gene_LifeForce;
        }

        public static void OffsetLifeForce(Gene_LifeForce gene_LifeForce, float offset, bool applyStatFactor = true)
        {
            if (gene_LifeForce != null)
            {
                float old_value = gene_LifeForce.Value;
                gene_LifeForce.Value += offset;
                PostOffSetLifeForce(gene_LifeForce, old_value);
            }
        }

        public static void PostOffSetLifeForce(Gene_LifeForce gene_LifeForce, float old_value)
        {
            if (old_value > 0.15f && gene_LifeForce.Resource.Value <= 0.15f)
            {
                Pawn pawn = gene_LifeForce.Pawn;
                
                //Give thoughtdef
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

        internal static bool IsAgeDrainer(Pawn pawn)
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
            return pawn.genes.HasGene(GeneDefOf.rjw_genes_pussyhealer);
        }

        public static bool IsUnbreakable(Pawn pawn)
        {
            if (pawn.genes == null)
            {
                return false;
            }
            return pawn.genes.HasGene(GeneDefOf.rjw_genes_unbreakable);
        }
    }
}