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

        // Comment Below in for debugging, changes years
        // const long AGE_TRANSFERED = 6000000; // 6000k == 100 days
        public static void Postfix(SexProps props)
        {
            if (props == null || props.pawn == null || props.partner == null || props.partner.IsAnimal() )
            {
                return;
            }
            if (GeneUtility.IsAgeDrainer(props.pawn))
            {
                var pawnAge = props.pawn.ageTracker.AgeBiologicalTicks;
                var pawnMinAge = props.pawn.ageTracker.AdultMinAgeTicks;

                // Make Partner older
                props.partner.ageTracker.AgeBiologicalTicks += AGE_TRANSFERED;
                // Make Pawn younger
                props.pawn.ageTracker.AgeBiologicalTicks = Math.Max(pawnMinAge, pawnAge - AGE_TRANSFERED);
            }

        }
    }
}
