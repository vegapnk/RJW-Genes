using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
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
    public class Patch_GeneticSexThief
    {

        public const int FACTION_GOODWILL_CHANGE = -10;

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
            if (GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_sexual_genetic_thief)
                && GeneUtility.HasGeneNullCheck(partner, GeneDefOf.rjw_genes_sexual_genetic_thief))
                return;
            
            if (GeneUtility.HasGeneNullCheck(pawn,GeneDefOf.rjw_genes_sexual_genetic_thief) &&
                !GeneUtility.HasGeneNullCheck(partner, GeneDefOf.rjw_genes_genetic_disease_immunity))
            {
                ChanceExtension chanceExt = GeneDefOf.rjw_genes_sexual_genetic_thief.GetModExtension<ChanceExtension>();
                if (chanceExt != null && (new Random()).NextDouble() < chanceExt.chance)
                    StealRandomGene(pawn, partner);
            }

            if (GeneUtility.HasGeneNullCheck(partner, GeneDefOf.rjw_genes_sexual_genetic_thief) &&
                !GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_genetic_disease_immunity))
            {
                ChanceExtension chanceExt = GeneDefOf.rjw_genes_sexual_genetic_thief.GetModExtension<ChanceExtension>();
                if (chanceExt != null && (new Random()).NextDouble() < chanceExt.chance)
                    StealRandomGene(partner, pawn);
            }
        }

        /// <summary>
        /// Removes a random gene from one pawn and adds it too the other as xenogene.
        /// </summary>
        private static void StealRandomGene(Pawn stealer, Pawn victim, bool AddAsXenogene = true)
        {
            var stolenGene = victim.genes.GenesListForReading
                .Where(gene => victim.genes.HasActiveGene(gene.def))
                .RandomElement();

            if (stolenGene == null) return;

            ModLog.Debug($"Sexual Gene Thief: {stealer} steals {stolenGene.def} from {victim}");

            stealer.genes.AddGene(stolenGene.def, AddAsXenogene);
            victim.genes.RemoveGene(stolenGene);

            FactionUtility.HandleFactionGoodWillPenalties(stealer, victim, "rjw_genes_GoodwillChangedReason_StoleGene", FACTION_GOODWILL_CHANGE);
        }

    }
}
