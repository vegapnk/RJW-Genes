using rjw;
using Verse;

namespace RJW_Genes
{
    /// <summary>
    /// This Gene regularly removes the broken hediff of a pawn. 
    /// Blocking / Removing thoughts are done in an XML Patch.
    /// </summary>
    public class Gene_Unbreakable : Gene
    {
        /// DevNote: I first tried to Harmony-Postfix the AfterSexUtility and never add it - but that failed? 

        private const int RESET_INTERVAL = 30000; // 30k should be 0.5 day 
        public override void PostAdd()
        {
            if (pawn.kindDef == null) return;   //Added to catch Rimworld creating statues of pawns.
            base.PostAdd();
            RemoveBrokenHediff();
        }

        public override void Tick()
        {
            base.Tick();
            if (pawn.IsHashIntervalTick(RESET_INTERVAL))
                RemoveBrokenHediff();
        }



        private void RemoveBrokenHediff()
        {
            // Clean-Up of existing feeling brokens
            var maybeBrokenHediff = pawn.health.hediffSet.GetFirstHediffOfDef(xxx.feelingBroken);
            if (maybeBrokenHediff != null)
            {
                pawn.health.RemoveHediff(maybeBrokenHediff);
            }
        }
    }
}
