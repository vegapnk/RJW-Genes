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

        // Comment Below in for debugging
        //const long AGE_REDUCTION = 6000000; // 6000k == 100 days
        public static void Postfix(SexProps props)
        {
            if (props == null || props.pawn == null || props.partner == null || props.partner.IsAnimal())
            {
                return;
            }
            if (GeneUtility.IsYouthFountain(props.pawn))
            {
                var partnerAge = props.partner.ageTracker.AgeBiologicalTicks;
                var minAge = props.partner.ageTracker.AdultMinAgeTicks;

                props.partner.ageTracker.AgeBiologicalTicks = Math.Max(minAge, partnerAge - AGE_REDUCTION);
            }

        }

    }
}
