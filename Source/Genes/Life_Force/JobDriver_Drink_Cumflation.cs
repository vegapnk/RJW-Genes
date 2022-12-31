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
	public class JobDriver_DrinkCumflation : JobDriver_SexBaseInitiator
	{
		//Summary//
		//WIP is for custom interaction
		protected override IEnumerable<Toil> MakeNewToils()
		{
			base.setup_ticks();
			this.rape = !LovePartnerRelationUtility.LovePartnerRelationExists(this.pawn, this.Partner);
			JobDef PartnerJob =  rape? xxx.gettin_raped: xxx.getting_quickie;
			this.FailOnDestroyedNullOrForbidden(TargetIndex.A);
			this.FailOnSomeonePhysicallyInteracting(TargetIndex.A);
			this.FailOn(() => this.pawn.Drafted);
			this.FailOn(() => this.pawn.IsFighting());
			this.FailOn(() => this.Partner.IsFighting());
			yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.ClosestTouch);
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
			toil.defaultDuration = this.duration;
			toil.handlingFacing = true;
			toil.FailOn(() => this.Partner.CurJob.def != PartnerJob);
			toil.initAction = delegate ()
			{
				this.Partner.pather.StopDead();
				this.Partner.jobs.curDriver.asleep = false;
				this.SetInteraction();
				this.cumflation = this.Partner.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("Cumflation"));
				this.gene_LifeForce = (this.pawn.genes != null) ? this.pawn.genes.GetFirstGeneOfType<Gene_LifeForce>() : null;
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

		public void Reduce_Cumflation()
        {
			this.reductiontick--;
			if (reductiontick <= 0)
            {
				if (this.cumflation != null && this.gene_LifeForce != null)
				{
					this.cumflation.Severity =+ 0.01f;
					gene_LifeForce.Resource.Value += 0.01f;
				}
				this.reductiontick = 60;
			}
			
		}
		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			return this.pawn.Reserve(this.job.GetTarget(TargetIndex.A), this.job, 1, -1, null, errorOnFailed);
		}

		public void SetInteraction()
		{
			InteractionDef interaction = rape ? DefDatabase<InteractionDef>.GetNamed("Rape_Reverse_Cunnilingus") : DefDatabase<InteractionDef>.GetNamed("Sex_Reverse_Cunnilingus");

			SpecificInteractionInputs inputs = new SpecificInteractionInputs
			{
				Initiator = this.pawn,
				Partner = this.Partner,
				Interaction = interaction
			};
			InteractionOutputs interactionOutputs = SpecificLewdInteractionService.Instance.GenerateSpecificInteraction(inputs);
			this.Sexprops.sexType = interactionOutputs.Generated.RjwSexType;
			this.Sexprops.rulePack = interactionOutputs.Generated.RulePack.defName;
			this.Sexprops.dictionaryKey = interaction;
			this.Sexprops.isRapist = rape;
			this.Sexprops.isWhoring = false;
			this.Sexprops.isRevese = true;
		}

		public Hediff cumflation;
		public Gene_LifeForce gene_LifeForce;
		int reductiontick = 60; 
		bool rape = false; 
	}
}
