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

            ModLog.Debug("Firing Pregnancy Overwrite Patch - Passed Simple NullChecks");

            if (pawn.IsPregnant() 
                && GeneUtility.HasGeneNullCheck(partner, GeneDefOf.rjw_genes_pregnancy_overwrite))
                    TryReplacePregnancy(partner, pawn);

            if (partner.IsPregnant() 
                && GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_pregnancy_overwrite))
                TryReplacePregnancy(pawn, partner);
        }

        public static void TryReplacePregnancy(Pawn replacer, Pawn pregnant)
        {
            // TODO: This mostly works, but needs some more checks.
            // - Check if there is a pregnancy occurring
            // - Check for Disease Immunity
            // - Add Faction Penalties

            ModLog.Debug($"Firing Pregnancy Overwrite for {replacer} and {pregnant}");

            // The "CanImpregnate" does not work as I want, as the pawn is already pregnant, so it wont allow to be pregnated. 
            //PregnancyHelper.CanImpregnate(pawn, partner, props.sexType)

            Hediff pregnancyHediff = PregnancyUtility.GetPregnancyHediff(pregnant);
            if (pregnancyHediff == null)
                return;

            float gestationProgress = pregnancyHediff.Severity;

            PregnancyUtility.ForceEndPregnancy(pregnant);

            PregnancyHelper.StartVanillaPregnancy(pregnant, replacer);
            Hediff replacementPregnancyHediff = PregnancyUtility.GetPregnancyHediff(pregnant);
            replacementPregnancyHediff.Severity = gestationProgress;
        }

    }
}
