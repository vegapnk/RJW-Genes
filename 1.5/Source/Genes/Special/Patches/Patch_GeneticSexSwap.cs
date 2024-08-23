using HarmonyLib;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{

    [HarmonyPatch(typeof(SexUtility), "Aftersex")]
    public class Patch_GeneticSexSwap
    {
        public static void Postfix(SexProps props)
        {
            if (props == null || props.pawn == null || props.partner == null || props.partner.IsAnimal())
            {
                return;
            }

            Pawn pawn = props.pawn;
            Pawn partner = props.partner;

            if (pawn.genes == null || partner.genes == null) return;

            // If both have the swap gene, nothing happens
            if (GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_sexual_genetic_swap) 
                && GeneUtility.HasGeneNullCheck(partner, GeneDefOf.rjw_genes_sexual_genetic_swap))
                return;
            // If neither has the swap gene, nothing happens
            if (!GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_sexual_genetic_swap)
                && !GeneUtility.HasGeneNullCheck(partner, GeneDefOf.rjw_genes_sexual_genetic_swap))
                return;

            ChanceExtension chanceExt = GeneDefOf.rjw_genes_sexual_genetic_swap.GetModExtension<ChanceExtension>();
            if (chanceExt != null && (new Random()).NextDouble() < chanceExt.chance)
                SwapOneRandomGene(pawn, partner);
        }

        /// <summary>
        /// Removes a random gene from one pawn and adds it too the other as xenogene.
        /// The "gene swap" gene cannot be swapped!
        /// </summary>
        private static void SwapOneRandomGene(Pawn a, Pawn b, bool AddAsXenogene = true)
        {

            var geneFromA = a.genes.GenesListForReading
                .Where(gene => a.genes.HasActiveGene(gene.def))
                .Where(gene => gene.def != GeneDefOf.rjw_genes_sexual_genetic_swap)
                .RandomElement();
            var geneFromB = b.genes.GenesListForReading
                .Where(gene => b.genes.HasActiveGene(gene.def))
                .Where(gene => gene.def != GeneDefOf.rjw_genes_sexual_genetic_swap)
                .RandomElement();

            if (geneFromA == null || geneFromB == null) return;

            ModLog.Debug($"Sexual Genetic Swap: Swapping {geneFromA.def} from {a} with {geneFromB.def} from {b}");

            a.genes.AddGene(geneFromB.def, AddAsXenogene);
            b.genes.AddGene(geneFromA.def, AddAsXenogene);
            a.genes.RemoveGene(geneFromA);
            b.genes.RemoveGene(geneFromB);
        }

    }
}
