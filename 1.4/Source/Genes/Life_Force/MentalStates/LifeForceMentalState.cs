using System;
using Verse;
using Verse.AI;
using rjw;
namespace RJW_Genes
{
	public class LifeForceMentalState : MentalState
	{
		public override void MentalStateTick()
		{
			if (this.pawn.IsHashIntervalTick(150) && !GeneUtility.HasCriticalLifeForce(this.pawn))
			{
				Pawn_JobTracker jobs = this.pawn.jobs;
				if (!(((jobs != null) ? jobs.curDriver : null) is JobDriver_Sex))
				{
					base.RecoverFromState();
					return;
				}
			}
			base.MentalStateTick();
		}
	}
}