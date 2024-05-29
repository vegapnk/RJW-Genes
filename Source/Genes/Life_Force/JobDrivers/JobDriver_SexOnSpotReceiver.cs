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
	//Modified JobDriver_SexBaseRecieverLoved from rjw
	public class JobDriver_SexOnSpotReciever : JobDriver_SexBaseReciever
	{
		protected override IEnumerable<Toil> MakeNewToils()
		{
			base.setup_ticks();
			this.parteners.Add(base.Partner);
			if (this.pawn.relations.OpinionOf(base.Partner) < 0)
			{
				this.ticks_between_hearts += 50;
			}
			else if (this.pawn.relations.OpinionOf(base.Partner) > 60)
			{
				this.ticks_between_hearts -= 25;
			}
			this.FailOnDespawnedOrNull(this.iTarget);
			this.FailOn(() => !base.Partner.health.capacities.CanBeAwake);
			this.FailOn(() => this.pawn.Drafted);
			this.FailOn(() => base.Partner.Drafted);
			yield return Toils_Reserve.Reserve(this.iTarget, 1, 0, null);
			Toil toil2 = this.MakeSexToil();
			toil2.handlingFacing = false;
			yield return toil2;
			yield break;
		}

		private Toil MakeSexToil()
		{
			Toil toil = new Toil();
			toil.defaultCompleteMode = ToilCompleteMode.Never;
			toil.socialMode = RandomSocialMode.Off;
			toil.handlingFacing = true;
			toil.tickAction = delegate ()
			{
				if (this.pawn.IsHashIntervalTick(this.ticks_between_hearts))
				{
					base.ThrowMetaIconF(this.pawn.Position, this.pawn.Map, FleckDefOf.Heart);
				}
			};
			toil.AddEndCondition(delegate
			{
				if (this.parteners.Count <= 0)
				{
					return JobCondition.Succeeded;
				}
				return JobCondition.Ongoing;
			});
			toil.AddFinishAction(delegate
			{

				GlobalTextureAtlasManager.TryMarkPawnFrameSetDirty(this.pawn);
				Hediff submitting = this.pawn.health.hediffSet.GetFirstHediffOfDef(xxx.submitting);
				if (submitting != null)
				{
					this.pawn.health.RemoveHediff(submitting);
					this.pawn.stances.stunner.StunFor(60, this.pawn, true, true);
				}
			});
			toil.socialMode = RandomSocialMode.Off;
			return toil;
		}
	}
}
