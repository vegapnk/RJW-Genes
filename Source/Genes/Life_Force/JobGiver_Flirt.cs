using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;
using RimWorld;
namespace RJW_Genes
{
	public class JobGiver_Flirt : ThinkNode_JobGiver
	{
		// Token: 0x0600405A RID: 16474 RVA: 0x0017271C File Offset: 0x0017091C
		protected override Job TryGiveJob(Pawn pawn)
		{
			Pawn target = pawn.mindState.duty.focus.Pawn;
			if (pawn.CanReach(target, PathEndMode.InteractionCell, Danger.Deadly) && !target.jobs.curDriver.asleep)
            {
				return JobMaker.MakeJob(JobDefOf.rjw_genes_flirt, target);
			}
			return null;
		}
	}
}
