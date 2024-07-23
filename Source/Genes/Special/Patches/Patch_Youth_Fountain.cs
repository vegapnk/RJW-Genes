using HarmonyLib;
using RimWorld;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes.Genes.Special
{
    [HarmonyPatch(typeof(SexUtility), "Aftersex")]
    public static class Patch_Youth_Fountain
    {
        /**
         * Update Issue #26: 
         * There are options that a 16 yo pawn and a 16 yo pawn have sex, 
         * or there are races that have a different age-limits.
         * I am not sure how I feel about this, but as some people that I consider "normal" asked me about this I changed it as requested in #26 and #28
         */

        const long AGE_REDUCTION_FALLBACK = 60000; // 60k == 1 day
        // 18 Years * 60 Days / Year * 60k Ticks/Day + 1 for safety
        const long MINIMUM_AGE_FALLBACK = 18 * 60 * 60000 + 1;

        const int FACTION_GOODWILL_CHANGE = 1;

        public static void Postfix(SexProps props)
        {
            if (props == null || props.pawn == null || props.partner == null || props.partner.IsAnimal())
            {
                return;
            }

            if (props.pawn == props.partner || props.sexType == xxx.rjwSextype.Masturbation || props.sexType == xxx.rjwSextype.None)
            {
                // This case was reported but is a bit strange, I hardened it after reports in #99
                return;
            }

            if (GeneUtility.IsYouthFountain(props.pawn))
            {
                ChangeAgeForPawn(props.partner, props.pawn);
                FactionUtility.HandleFactionGoodWillPenalties(props.pawn, props.partner, "rjw_genes_GoodwillChangedReason_youthed_pawn_with_sex_gene",+1);
            }
            if (GeneUtility.IsYouthFountain(props.partner))
            {
                ChangeAgeForPawn(props.pawn,props.partner);
                FactionUtility.HandleFactionGoodWillPenalties(props.pawn, props.partner, "rjw_genes_GoodwillChangedReason_youthed_pawn_with_sex_gene", +1);
            }

        }

        private static void ChangeAgeForPawn(Pawn ToYouth, Pawn YouthingPawn)
        {
            AgeTransferExtension transferExt = GeneDefOf.rjw_genes_youth_fountain.GetModExtension<AgeTransferExtension>();
            long age_reduction = transferExt?.ageTickChange ?? AGE_REDUCTION_FALLBACK;
            long minimum_age = transferExt?.minAgeInYears * 60 * 60000 + 1 ?? MINIMUM_AGE_FALLBACK;

            var partnerAge = ToYouth.ageTracker.AgeBiologicalTicks;

            if (RJW_Genes_Settings.rjw_genes_detailed_debug)
                ModLog.Message($"Firing Youth Fountain - {YouthingPawn} is youthing {ToYouth} by {age_reduction} ({Math.Round(age_reduction / 60000.0, 2)} days)");

            if (partnerAge - age_reduction > minimum_age) { 
                ToYouth.ageTracker.AgeBiologicalTicks = Math.Max(minimum_age, partnerAge - age_reduction);
            }
            else if (RJW_Genes_Settings.rjw_genes_detailed_debug)
                ModLog.Message($"[Youth Fountain] {ToYouth} was too young ({ToYouth.ageTracker.AgeBiologicalYears}), and remains unchanged.");
        }


    }

}
