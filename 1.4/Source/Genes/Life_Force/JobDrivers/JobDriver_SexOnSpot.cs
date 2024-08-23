using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using Verse.AI;
using rjw;
using rjw.Modules.Interactions.Enums;
using rjw.Modules.Interactions.Helpers;
using rjw.Modules.Interactions.Objects;
using rjw.Modules.Interactions.Contexts;
using rjw.Modules.Interactions.Implementation;

namespace RJW_Genes
{
	public class JobDriver_SexOnSpot : JobDriver_SexBaseInitiator
	{
		protected override IEnumerable<Toil> MakeNewToils()
		{
			if (RJWSettings.DebugRape)
			{
				ModLog.Message(base.GetType().ToString() + "::MakeNewToils() called");
			}
			base.setup_ticks();
			JobDef PartnerJob = JobDefOf.sex_on_spot_reciever;
			this.FailOnDespawnedNullOrForbidden(this.iTarget);
			this.FailOn(() => !this.pawn.CanReserve(this.Partner, xxx.max_rapists_per_prisoner, 0, null, false));
			this.FailOn(() => this.pawn.IsFighting());
			this.FailOn(() => this.Partner.IsFighting());
			this.FailOn(() => this.pawn.Drafted);
			yield return Toils_Goto.GotoThing(this.iTarget, PathEndMode.Touch);
			if (this.pawn.HostileTo(this.Partner))
            {
				Partner.health.AddHediff(xxx.submitting);
            }
			yield return Toils_Goto.GotoThing(this.iTarget, PathEndMode.OnCell);
			//Give thought malus to partner (I was seduced into having sex against my will)
			yield return new Toil
			{
				defaultCompleteMode = ToilCompleteMode.Instant,
				socialMode = RandomSocialMode.Off,
				initAction = delegate ()
				{
					if (!(this.Partner.jobs.curDriver is JobDriver_SexOnSpotReciever))
					{
						Job newJob = JobMaker.MakeJob(PartnerJob, this.pawn);
						Building_Bed building_Bed = null;
						if (this.Partner.GetPosture() == PawnPosture.LayingInBed)
						{
							building_Bed = this.Partner.CurrentBed();
						}
						this.Partner.jobs.StartJob(newJob, JobCondition.InterruptForced, null, false, true, null, null, false, false, null, false, true);
						if (building_Bed != null)
						{
							JobDriver_SexOnSpotReciever jobDriver_SexOnSpotReciever = this.Partner.jobs.curDriver as JobDriver_SexOnSpotReciever;
							if (jobDriver_SexOnSpotReciever == null)
							{
								return;
							}
							jobDriver_SexOnSpotReciever.Set_bed(building_Bed);
						}
					}
				}
			};
			Toil toil = new Toil();
			toil.defaultCompleteMode = ToilCompleteMode.Never;
			toil.defaultDuration = this.duration;
			toil.handlingFacing = true;
			toil.FailOn(() => this.Partner.CurJob.def != PartnerJob);
			toil.initAction = delegate ()
			{
				this.Partner.pather.StopDead();
				this.Partner.jobs.curDriver.asleep = false;
				this.Start();
			};
			toil.tickAction = delegate ()
			{
				if (this.pawn.IsHashIntervalTick(this.ticks_between_hearts))
				{
					this.ThrowMetaIconF(this.pawn.Position, this.pawn.Map, FleckDefOf.Heart);
				}
				this.SexTick(this.pawn, this.Partner, true, true);
				SexUtility.reduce_rest(this.Partner, 1f);
				SexUtility.reduce_rest(this.pawn, 2f);
				if (this.ticks_left <= 0)
				{
					this.ReadyForNextToil();
				}
			};
			toil.AddFinishAction(delegate
			{
				this.End();
			});
			yield return toil;
			yield return new Toil
			{
				initAction = delegate ()
				{
					SexUtility.ProcessSex(this.Sexprops);
				},
				defaultCompleteMode = ToilCompleteMode.Instant
			};
			yield break;
		}
	}
}
