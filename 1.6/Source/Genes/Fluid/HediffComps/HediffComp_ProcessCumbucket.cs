
using RimWorld;
using System;
using Verse;

namespace RJW_Genes
{
    public class HediffComp_ProcessCumbucket : HediffComp
    {

        public HediffsCompProperties_ProcessCumbucketMTB Props
        {
            get
            {
                return (HediffsCompProperties_ProcessCumbucketMTB)this.props;
            }
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (this.Props.mtbDaysPerStage[this.parent.CurStageIndex] > 0f && base.Pawn.IsHashIntervalTick(60) && Rand.MTBEventOccurs(this.Props.mtbDaysPerStage[this.parent.CurStageIndex], 60000f, 60f))
            {
                ModLog.Debug($"Triggered HediffComp_ProcessCumbucket CompPostTick - Starting a JobDriver ProcessCumbucket for {this.parent.pawn}");
                this.Pawn.jobs.StartJob(JobMaker.MakeJob(DefDatabase<JobDef>.GetNamed("RJW_Genes_ProcessCumbucket")), lastJobEndCondition: Verse.AI.JobCondition.InterruptForced, resumeCurJobAfterwards: true);
            }
        }
    }
}
