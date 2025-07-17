using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rjw;
using rjw.Modules.Attraction;
using Verse;
using Verse.AI;
namespace RJW_Genes
{
    public class ThinkNode_NewFlirtTarget : ThinkNode
    {
        public override ThinkResult TryIssueJobPackage(Pawn pawn, JobIssueParams jobParams)
        {
            List<Pawn> validTargets = ValidTargets(pawn, pawn.Map).ToList();
            Pawn new_target = validTargets.NullOrEmpty() ? null : validTargets.RandomElement();
            if (new_target != null)
            {
                pawn.mindState.duty.focus = new_target;
            }
            return ThinkResult.NoJob;
        }

        private IEnumerable<Pawn> ValidTargets(Pawn pawn, Map map)
        {
            foreach (Pawn pawn2 in map.mapPawns.FreeAdultColonistsSpawned)
            {
                if (pawn != null && pawn2 != null && pawn != pawn2 && !pawn2.jobs.curDriver.asleep && AttractionUtility.Evaluate(pawn, pawn2) > 0.1f)
                {
                    yield return pawn2;
                }
            }
            //IEnumerator<Pawn> enumerator = null;
            yield break;
        }
    }
}
