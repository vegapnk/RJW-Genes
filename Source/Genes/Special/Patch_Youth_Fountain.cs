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
         * I don't want to account for "trust me this race is really adult with Age 6!!!"-stuff.
         * As it is somewhat a bug when the pawns age tho, I added that the youth-fountain also needs to have MIN_AGE. 
         * If you'd like a different behaviour, you have to do it yourself. 
         */


        const long AGE_REDUCTION = 60000; // 60k == 1 day
        // 18 Years * 60 Days / Year * 60k Ticks/Day + 1 for safety
        const long MINIMUM_AGE = 18 * 60 * 60000 + 1;

        // Comment Below in for debugging
        // const long AGE_REDUCTION = 6000000; // 6000k == 100 days
        public static void Postfix(SexProps props)
        {
            if (props == null || props.pawn == null || props.partner == null || props.partner.IsAnimal())
            {
                return;
            }
            if (GeneUtility.IsYouthFountain(props.pawn) && props.pawn.ageTracker.AgeBiologicalTicks >= MINIMUM_AGE)
            {
                var partnerAge = props.partner.ageTracker.AgeBiologicalTicks;

                props.partner.ageTracker.AgeBiologicalTicks = Math.Max(MINIMUM_AGE, partnerAge - AGE_REDUCTION);
            }

        }

        private static float ticksToYears(long ticks)
        {
            return (ticks / 60000f) / 60f;
        }
    }

}
