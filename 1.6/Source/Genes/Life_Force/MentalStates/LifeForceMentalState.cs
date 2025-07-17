using System;
using Verse;
using Verse.AI;
using rjw;
namespace RJW_Genes
{
	public class LifeForceMentalState : MentalState
	{
        //1.6 Change, delta is now passed to MentalStateTick
        public override void MentalStateTick(int delta)
		{
            //1.6 Update change
            //if (this.pawn.IsHashIntervalTick(150) && !GeneUtility.HasCriticalLifeForce(this.pawn))
            if (this.pawn.IsHashIntervalTick(150, delta) && !GeneUtility.HasCriticalLifeForce(this.pawn))
            {
				Pawn_JobTracker jobs = this.pawn.jobs;
				if (!(((jobs != null) ? jobs.curDriver : null) is JobDriver_Sex))
				{
					base.RecoverFromState();
					return;
				}
			}
			//1.6 Change, delta is now passed to MentalStateTick
			base.MentalStateTick(delta);
		}
	}
}