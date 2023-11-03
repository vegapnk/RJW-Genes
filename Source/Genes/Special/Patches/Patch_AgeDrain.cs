﻿using HarmonyLib;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace RJW_Genes.Genes.Special
{
    [HarmonyPatch(typeof(SexUtility), "Aftersex")]
    public static class Patch_AgeDrain
    {
        /**
         * Update Issue #26: 
         * There are options that a 16 yo pawn and a 16 yo pawn have sex, 
         * or there are races that have a different age-limits.
         * I am not sure how I feel about this, but as some people that I consider "normal" asked me about this I changed it as requested in #26 and #28
         */

        const long AGE_TRANSFERED_FALLBACK = 120000; // 120k == 2 days
        // 18 Years * 60 Days / Year * 60k Ticks/Day + 1 for safety
        const long MINIMUM_AGE_FALLBACK = 18 * 60 * 60000 + 1;

        public static void Postfix(SexProps props)
        {
            if (props == null || props.pawn == null || props.partner == null || props.partner.IsAnimal() )
            {
                return;
            }

            Pawn pawn = props.pawn;
            Pawn partner = props.partner;

            if (GeneUtility.IsAgeDrainer(pawn) && !GeneUtility.IsAgeDrainer(partner))
            {
                TransferAge(pawn, partner);
            }
            else if (GeneUtility.IsAgeDrainer(partner) && !GeneUtility.IsAgeDrainer(pawn))
            {
                TransferAge(partner,pawn);
            }
            else if (GeneUtility.IsAgeDrainer(partner) && GeneUtility.IsAgeDrainer(pawn) && RJW_Genes_Settings.rjw_genes_detailed_debug)
            {
                ModLog.Message($"[Sexual Age Drainer] both {pawn} and {partner} are sexual-age-drainers - nothing happens.");
            }
        }

        /// <summary>
        /// Transfers age from the giver to the receiver.
        /// </summary>
        /// <param name="receiver">The pawn that will receive biological-Age-Ticks, and becomes younger if they are not already young. </param>
        /// <param name="giver">The pawn that will be giving biological-Age-Ticks. This pawn is always aged, even if the other pawn is too young.</param>
        private static void TransferAge(Pawn receiver, Pawn giver)
        {
            AgeTransferExtension transferExt = GeneDefOf.rjw_genes_sex_age_drain.GetModExtension<AgeTransferExtension>();
            long age_transfered = transferExt?.ageTickChange ?? AGE_TRANSFERED_FALLBACK;
            long minimum_age = transferExt?.minAgeInYears * 60 * 60000 + 1 ?? MINIMUM_AGE_FALLBACK;

            var pawnAge = receiver.ageTracker.AgeBiologicalTicks;

            if (RJW_Genes_Settings.rjw_genes_detailed_debug)
                ModLog.Message($"[Sexual Age Drainer] {receiver} is aging {giver} by {age_transfered} ({Math.Round(age_transfered / 60000.0, 2)} days)");

            // Giver ALWAYS ages 
            giver.ageTracker.AgeBiologicalTicks += age_transfered;

            // Make Receiver younger if they are older than minimum age
            if (pawnAge - age_transfered > minimum_age)
                receiver.ageTracker.AgeBiologicalTicks = Math.Max(minimum_age, (pawnAge - age_transfered));
            else {
                if (RJW_Genes_Settings.rjw_genes_detailed_debug)
                    ModLog.Message($"[Sexual Age Drainer] {receiver} was too young ({receiver.ageTracker.AgeBiologicalYears}), and remains unchanged.");
            }
        }
    }
}
