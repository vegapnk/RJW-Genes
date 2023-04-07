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
    public static class Patch_Youth_Fountain
    {
        /**
         * Update Issue #26: 
         * There are options that a 16 yo pawn and a 16 yo pawn have sex, 
         * or there are races that have a different age-limits.
         * I am not sure how I feel about this, but as some people that I consider "normal" asked me about this I changed it as requested in #26 and #28
         */

        const long AGE_REDUCTION = 60000; // 60k == 1 day
        // 18 Years * 60 Days / Year * 60k Ticks/Day + 1 for safety
        const long MINIMUM_AGE = 18 * 60 * 60000 + 1;

        public static void Postfix(SexProps props)
        {
            if (props == null || props.pawn == null || props.partner == null || props.partner.IsAnimal())
            {
                return;
            }
            if (GeneUtility.IsYouthFountain(props.pawn) && props.pawn.ageTracker.AgeBiologicalTicks >= MINIMUM_AGE)
            {
                var partnerAge = props.partner.ageTracker.AgeBiologicalTicks;

                if(partnerAge - AGE_REDUCTION > MINIMUM_AGE)
                    props.partner.ageTracker.AgeBiologicalTicks = Math.Max(MINIMUM_AGE, partnerAge - AGE_REDUCTION);
            }

        }

    }

}
