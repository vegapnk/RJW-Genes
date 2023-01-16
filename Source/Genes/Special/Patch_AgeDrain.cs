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

        const long AGE_TRANSFERED = 120000; // 120k == 2 days
        // 20 Years * 60 Days / Year * 60k Ticks/Day + 1 for safety
        const long MINIMUM_AGE = 20 * 60 * 60000 + 1;

        // Comment Below in for debugging, changes years
        // const long AGE_TRANSFERED = 12000000; 
        public static void Postfix(SexProps props)
        {
            if (props == null || props.pawn == null || props.partner == null || props.partner.IsAnimal() )
            {
                return;
            }
            if (GeneUtility.IsAgeDrainer(props.pawn))
            {
                var pawnAge = props.pawn.ageTracker.AgeBiologicalTicks;
                //ModLog.Error($"Firing Age Drain \nMinimum Age is \t{MINIMUM_AGE} \nPawn Age is \t{pawnAge} \nTransferred \t{AGE_TRANSFERED}\nResulting in \t{pawnAge - AGE_TRANSFERED}");

                // Make Partner older
                props.partner.ageTracker.AgeBiologicalTicks += AGE_TRANSFERED;
                // Make Pawn younger
                props.pawn.ageTracker.AgeBiologicalTicks = Math.Max(MINIMUM_AGE, (pawnAge - AGE_TRANSFERED));
            }

        }
    }
}
