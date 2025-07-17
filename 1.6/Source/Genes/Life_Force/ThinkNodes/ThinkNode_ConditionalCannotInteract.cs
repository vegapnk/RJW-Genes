using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;
namespace RJW_Genes
{
    public class ThinkNode_ConditionalCannotInteract : ThinkNode_Conditional
    {
        protected override bool Satisfied(Pawn pawn)
        {
            Pawn target = pawn.mindState.duty.focus.Pawn;
            if (target == null)
            {
                return true;    
            }
            return (target.jobs != null && target.jobs.curDriver.asleep) || !pawn.CanReach(target, PathEndMode.InteractionCell, Danger.Deadly);
        }
    }
}
