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

        const long AGE_REDUCTION = 60000; // 60k == 1 day
        // 20 Years * 60 Days / Year * 60k Ticks/Day + 1 for safety
        const long MINIMUM_AGE = 20 * 60 * 60000 + 1;

        // Comment Below in for debugging
        // const long AGE_REDUCTION = 6000000; // 6000k == 100 days
        public static void Postfix(SexProps props)
        {
            if (props == null || props.pawn == null || props.partner == null || props.partner.IsAnimal())
            {
                return;
            }
            if (GeneUtility.IsYouthFountain(props.pawn))
            {
                var partnerAge = props.partner.ageTracker.AgeBiologicalTicks;

                //ModLog.Error($"Firing Youth Fountain \nMinimum Age is \t{MINIMUM_AGE}\t{ticksToYears(MINIMUM_AGE)}y\nPawn Age is \t{partnerAge}\t{ticksToYears(partnerAge)}y \nTransferred \t {AGE_REDUCTION}\t{ticksToYears(AGE_REDUCTION)}y\nResulting in \t{partnerAge - AGE_REDUCTION}\t{ticksToYears(partnerAge - AGE_REDUCTION)}y");

                props.partner.ageTracker.AgeBiologicalTicks = Math.Max(MINIMUM_AGE, partnerAge - AGE_REDUCTION);
            }

        }

        private static float ticksToYears(long ticks)
        {
            return (ticks / 60000f) / 60f;
        }
    }

}
