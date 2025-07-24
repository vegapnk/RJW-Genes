using Verse;


namespace RJW_Genes
{
    /// <summary>
    /// This Gene adds Licentia-Labs Elasticised Hediff to a Pawn. 
    /// Note: I had a HarmonyPatch first, similar to skipping cumflation, but the Stretching Logic is called quite a lot and for both pawns actually.
    /// Hence, I think choosing the Elasticiced Hediff was good as then everything is covered by "Licentia-Logic". 
    /// </summary>
    public class Gene_Elasticity : Gene
    {
        private const int RESET_INTERVAL = 60000; // 60k should be 1 day 

        
        public override void PostAdd()
        {
            if (pawn.kindDef == null) return;   //Added to catch Rimworld creating statues of pawns.
            base.PostAdd();
            
            
            
            // Doing it like this will add the hediff with a severity of ~0.5, but it will decay.
            // Hence we check with the Ticks to update.
            //this.pawn.health.AddHediff(Licentia.HediffDefs.Elasticised);
            //ResetSeverity();
        }

        public override void Tick()
        {
            base.Tick();
            //if (pawn.IsHashIntervalTick(RESET_INTERVAL))
            //    ResetSeverity();
        }

        public override void PostRemove()
        {
            //Hediff candidate = pawn.health.hediffSet.GetFirstHediffOfDef(Licentia.HediffDefs.Elasticised);
            //if (candidate != null)
            //{
            //    pawn.health.RemoveHediff(candidate);
            //}
            base.PostRemove();
        }


        //private void ResetSeverity(float severity = 0.7f)
        //{
        //    Hediff candidate = pawn.health.hediffSet.GetFirstHediffOfDef(Licentia.HediffDefs.Elasticised);
        //    if (candidate != null)
        //    {
        //        candidate.Severity = severity;
        //    }
        //}
        
    }
}