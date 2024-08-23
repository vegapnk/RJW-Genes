using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;
using RimWorld;
using rjw;
namespace RJW_Genes
{
	public class JobDriver_Flirt : JobDriver
	{
		private Pawn Target
		{
			get
			{
				return (Pawn)((Thing)this.pawn.CurJob.GetTarget(TargetIndex.A));
			}
		}
		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			return true;
		}

		//Some wait toils to induce delay
		protected override IEnumerable<Toil> MakeNewToils()
		{
			this.FailOnDespawnedOrNull(TargetIndex.A);
			yield return Toils_Interpersonal.GotoInteractablePosition(TargetIndex.A);
			yield return Toils_General.Wait(300, TargetIndex.A);
			yield return Toils_Interpersonal.WaitToBeAbleToInteract(this.pawn);
			Toil toil = Toils_Interpersonal.GotoInteractablePosition(TargetIndex.A);
			toil.socialMode = RandomSocialMode.Off;
			yield return toil;
			yield return this.InteractToil();
			Toil toil1 = Toils_General.Wait(300, TargetIndex.A);
			toil1.socialMode = RandomSocialMode.Off;
			yield return toil1;
			yield break;
		}
		private Toil InteractToil()
		{
			return Toils_General.Do(delegate
			{
				if (this.pawn.interactions.TryInteractWith(this.Target, ThoughtDefOf.rjw_genes_flirt))
				{
					Need_Sex need_Sex = this.Target.needs.TryGetNeed<Need_Sex>();
					need_Sex.CurLevel += -0.01f;
				}
			});
		}

		private const TargetIndex TargetInd = TargetIndex.A;
	}
}

