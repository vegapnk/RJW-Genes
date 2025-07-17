using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using Verse.AI;
using rjw;
//using rjw.Modules.Interactions.Enums;
//using rjw.Modules.Interactions.Helpers;
//using rjw.Modules.Interactions.Objects;
//using rjw.Modules.Interactions.Contexts;
//using rjw.Modules.Interactions.Implementation;

namespace RJW_Genes
{
	public class JobDriver_Seduced : JobDriver
	{
		//Summary//
		//Makes a pawn move to seducing pawn and then tries to rape them.
		protected override IEnumerable<Toil> MakeNewToils()
		{
			
			this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
			this.FailOn(() => !this.pawn.CanReserve(TargetA, xxx.max_rapists_per_prisoner, 0, null, false));
			this.FailOn(() => this.pawn.IsFighting());
			this.FailOn(() => this.pawn.Drafted);
	
			Pawn partner = this.job.GetTarget(TargetIndex.A).Pawn;
			yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
			yield return new Toil
			{
				defaultCompleteMode = ToilCompleteMode.Instant,
				socialMode = RandomSocialMode.Off,
				initAction = delegate ()
				{
					if(partner != null)
                    {
						partner.drafter.Drafted = false;
						this.pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.rjw_genes_seduced, partner, null);
						Job newJob = JobMaker.MakeJob(JobDefOf.sex_on_spot, pawn);
						partner.jobs.StartJob(newJob, JobCondition.InterruptForced, null, false, true, null, null, false, false, null, false, true);
					}
				}
			};
			yield break;
		}

		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			return this.pawn.Reserve(TargetA, this.job, xxx.max_rapists_per_prisoner, 0, null, errorOnFailed);
		}
	}
}
