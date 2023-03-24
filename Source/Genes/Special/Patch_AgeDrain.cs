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
         * I don't want to account for "trust me this race is really adult with Age 6!!!"-stuff.
         * As it is somewhat a bug when the pawns age tho, I added that the youth-fountain also needs to have MIN_AGE. 
         * If you'd like a different behaviour, you have to do it yourself. 
         */

        const long AGE_TRANSFERED = 120000; // 120k == 2 days
        // 18 Years * 60 Days / Year * 60k Ticks/Day + 1 for safety
        const long MINIMUM_AGE = 18 * 60 * 60000 + 1;

        // Comment AGE_TRANSFERED in for debugging, changes years
        // const long AGE_TRANSFERED = 12000000; 
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
                // Make Pawn younger
                props.pawn.ageTracker.AgeBiologicalTicks = Math.Max(MINIMUM_AGE, (pawnAge - AGE_TRANSFERED));
            }

        }
    }
}
