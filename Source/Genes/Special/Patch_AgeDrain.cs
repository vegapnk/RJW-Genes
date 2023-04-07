using HarmonyLib;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        const long AGE_TRANSFERED = 120000; // 120k == 2 days
        // 18 Years * 60 Days / Year * 60k Ticks/Day + 1 for safety
        const long MINIMUM_AGE = 18 * 60 * 60000 + 1;

        public static void Postfix(SexProps props)
        {
            if (props == null || props.pawn == null || props.partner == null || props.partner.IsAnimal() )
            {
                return;
            }
            if (GeneUtility.IsAgeDrainer(props.pawn) && props.pawn.ageTracker.AgeBiologicalTicks > MINIMUM_AGE)
            {
                var pawnAge = props.pawn.ageTracker.AgeBiologicalTicks;
                //ModLog.Error($"Firing Age Drain \nMinimum Age is \t{MINIMUM_AGE} \nPawn Age is \t{pawnAge} \nTransferred \t{AGE_TRANSFERED}\nResulting in \t{pawnAge - AGE_TRANSFERED}");

                // Make Partner older
                props.partner.ageTracker.AgeBiologicalTicks += AGE_TRANSFERED;
                // Make Pawn younger if he is older than minimum age
                if (pawnAge - AGE_TRANSFERED > MINIMUM_AGE)
                    props.pawn.ageTracker.AgeBiologicalTicks = Math.Max(MINIMUM_AGE, (pawnAge - AGE_TRANSFERED));
            }

        }
    }
}
