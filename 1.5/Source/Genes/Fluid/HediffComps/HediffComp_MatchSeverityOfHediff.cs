using Verse;

namespace RJW_Genes
{
    public class HediffComp_MatchSeverityOfHediff : HediffComp
    {
        public HediffCompProperties_MatchSeverityOfHediff Props => (HediffCompProperties_MatchSeverityOfHediff)this.props;

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            bool hasMatchingHediff = parent.pawn.health.hediffSet.HasHediff(Props.hediffToMatch);

            if (!hasMatchingHediff) {
                parent.Severity = 0.0f;
            }
            else
            {
                Hediff match = parent.pawn.health.hediffSet.GetFirstHediffOfDef(Props.hediffToMatch);
                if (match == null) parent.Severity = 0.0f;
                parent.Severity = match.Severity;
            }
        }
    }
}
