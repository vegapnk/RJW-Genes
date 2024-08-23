using HarmonyLib;
using RimWorld;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using static HarmonyLib.Code;

namespace RJW_Genes
{
    [HarmonyPatch(typeof(SexUtility), "Aftersex")]
    public class Patch_PregnancyOverwrite
    {
        public const int FACTION_GOODWILL_CHANGE = -5;

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
            if (GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_pregnancy_overwrite)
                && GeneUtility.HasGeneNullCheck(partner, GeneDefOf.rjw_genes_pregnancy_overwrite))
                return;

            // If both are pregnant, we have some weird interaction. Exit Early 
            if (pawn.IsPregnant() && partner.IsPregnant())
                return;
            // If neither are pregnant, nothing can happen.
            if (!pawn.IsPregnant() && !partner.IsPregnant())
                return;

            if (pawn.IsPregnant() 
                && GeneUtility.HasGeneNullCheck(partner, GeneDefOf.rjw_genes_pregnancy_overwrite))
                    TryReplacePregnancy(partner, pawn);

            if (partner.IsPregnant() 
                && GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_pregnancy_overwrite))
                TryReplacePregnancy(pawn, partner);
        }

        /// <summary>
        /// Tries to replace an existing pregnancy with a new pregnancy at the same gestation process. 
        /// The new pregnancy will have the same mother, but a new father and a new set of genes. 
        /// 
        /// There is a check for pregnancy that checks for the general fertility (using Vanilla Functions) and multiplies it with a xml-configurable chance.
        /// If anything is replaced, there will be a faction penalty applied. 
        /// </summary>
        /// <param name="replacer"></param>
        /// <param name="pregnant"></param>
        public static void TryReplacePregnancy(Pawn replacer, Pawn pregnant)
        {

            // DevNote:
            // There are some issues with just checking PregnancyUtility.PregnancyChanceForPartners or rjw.PregnancyHelper.CanImpregnate
            // Both do give 0.0 chance when the pawn is already pregnant, which does not help me :/
            Hediff pregnancyHediff = PregnancyUtility.GetPregnancyHediff(pregnant);
            if (pregnancyHediff == null)
                return;

            if (DiseaseHelper.IsImmuneAgainstGeneticDisease(pregnant, GeneDefOf.rjw_genes_pregnancy_overwrite))
            {
                ModLog.Debug($"{pregnant} is immune against rjw_genes_pregnancy_overwrite from {replacer}");
                return;
            }

            ChanceExtension chanceExt = GeneDefOf.rjw_genes_pregnancy_overwrite.GetModExtension<ChanceExtension>();
            float chance = chanceExt != null ? chanceExt.chance : 0.25f;
            float replacerFert = replacer.GetStatValueForPawn(StatDefOf.Fertility, replacer);
            chance *=  replacerFert ;
            double roll = (new Random()).NextDouble();
            if (roll < chance)
            {
                ModLog.Debug($"Pregnancy-Overwrite for {replacer} and {pregnant}.");
                float gestationProgress = pregnancyHediff.Severity;

                PregnancyUtility.ForceEndPregnancy(pregnant);

                PregnancyHelper.StartVanillaPregnancy(pregnant, replacer);
                Hediff replacementPregnancyHediff = PregnancyUtility.GetPregnancyHediff(pregnant);
                replacementPregnancyHediff.Severity = gestationProgress;

                FactionUtility.HandleFactionGoodWillPenalties(replacer, pregnant, "rjw_genes_GoodwillChangedReason_OverwritePregnancy", FACTION_GOODWILL_CHANGE);
            } else
            {
                ModLog.Debug($"Did not Pregnancy-Overwrite for {replacer} and {pregnant}. Failed: Rolled {roll} <({chanceExt.chance}[XML-Chance] x {replacerFert} [Fert:{replacer}])");
            }
        }

    }
}
