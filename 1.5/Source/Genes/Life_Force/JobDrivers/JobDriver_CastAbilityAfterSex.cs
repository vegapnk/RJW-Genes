using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using Verse.AI;
using rjw;

namespace RJW_Genes
{
	public class JobDriver_CastAbilityAfterSex : JobDriver_SexBaseInitiator
	{
		//Summary//
		//Similar to jobdriver rape, but it cast an ability after sex and tries to limit what kind of sexinteractions are allowed.
		protected override IEnumerable<Toil> MakeNewToils()
		{
			base.setup_ticks();
			//this.FailOnDespawnedOrNull(TargetIndex.A);
			//this.FailOnCannotTouch(TargetIndex.B, PathEndMode.OnCell);
			this.FailOnDespawnedNullOrForbidden(this.iTarget);
			//this.FailOn(() => !target.health.capacities.CanBeAwake);
			JobDef PartnerJob = xxx.gettin_raped;
			yield return Toils_Goto.Goto(TargetIndex.A, PathEndMode.OnCell);
			yield return new Toil
			{
				defaultCompleteMode = ToilCompleteMode.Instant,
				socialMode = RandomSocialMode.Off,
				initAction = delegate ()
				{
					Job newJob = JobMaker.MakeJob(PartnerJob, this.pawn, this.Partner);
					this.Partner.jobs.StartJob(newJob, JobCondition.InterruptForced, null, false, true, null, null, false, false, null, false, true);
				}
			};
			Toil toil = new Toil();
			toil.defaultCompleteMode = ToilCompleteMode.Never;
			toil.socialMode = RandomSocialMode.Off;
			toil.defaultDuration = this.duration;
			toil.handlingFacing = true;
			toil.FailOn(() => this.Partner.CurJob.def != PartnerJob);
			toil.initAction = delegate ()
			{
				this.Partner.pather.StopDead();
				this.Partner.jobs.curDriver.asleep = false;

				//Tries to find CompProperties_SexInteractionRequirements and if it finds it it will try and generate sexprops based on the sexpropsrequirements.
				foreach (AbilityComp comp in this.job.ability.comps)
				{
					if (comp.props is CompProperties_SexInteractionRequirements)
					{
						CompProperties_SexInteractionRequirements sexpropsreq = comp.props as CompProperties_SexInteractionRequirements;
						this.Sexprops = CustomSexInteraction_Helper.GenerateSexProps(this.pawn, this.Partner, sexpropsreq);
					}
				}
				this.Start();
				this.Sexprops.usedCondom = (CondomUtility.TryUseCondom(this.pawn) || CondomUtility.TryUseCondom(this.Partner));
			};
			toil.AddPreTickAction(delegate
			{
				if (this.pawn.IsHashIntervalTick(this.ticks_between_hearts))
				{
					this.ThrowMetaIconF(this.pawn.Position, this.pawn.Map, FleckDefOf.Heart);
				}
				this.SexTick(this.pawn, this.Partner, true, true);
				SexUtility.reduce_rest(this.Partner, 1f);
				SexUtility.reduce_rest(this.pawn, 1f);
				if (this.ticks_left <= 0)
				{
					this.ReadyForNextToil();
				}
			});
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
			yield return Toils_Combat.CastVerb(TargetIndex.A, TargetIndex.B, false);
			yield break;
		}
	}
}
