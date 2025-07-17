using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using Verse.AI;

namespace CumpilationPatcher
{
    internal class Patch_JobGiver_GetLifeForce
    {
        //Patches the 'empty' GetStoredCum method in 'JobGiver_GetLifeForce'
        public static void PostFix(Pawn pawn, ref Thing __result)
        {
            Thing carriedThing = pawn.carryTracker.CarriedThing;
            ThingDef cumThingDef = Cumpilation.DefOfs.Cumpilation_Cum;

            if (cumThingDef == null) { return; }    //__result is already null;

            if (carriedThing != null && carriedThing.def == cumThingDef)
            {
                __result = carriedThing;
                return;
            }
            for (int i = 0; i < pawn.inventory.innerContainer.Count; i++)
            {
                if (pawn.inventory.innerContainer[i].def == cumThingDef)
                {
                    __result = pawn.inventory.innerContainer[i];
                    return;
                }
            }
            __result = GenClosest.ClosestThing_Global_Reachable(pawn.Position, pawn.Map, pawn.Map.listerThings.ThingsOfDef(cumThingDef), PathEndMode.OnCell, TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false, false, false), 9999f, (Thing t) => pawn.CanReserve(t, 1, -1, null, false) && !t.IsForbidden(pawn), null);
            return;
        }
    }
}
