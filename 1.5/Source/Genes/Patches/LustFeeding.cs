using RimWorld;
using rjw;
using Verse;
using System;
using RimWorld.Planet;



namespace RJWLoveFeeding
{
    static class LustFeeding
    {
        //[HarmonyPostfix]
        static Def LoveFeed = DefDatabase<GeneDef>.GetNamed("RS_LoveFeed", false);
        static Def VampireLover = DefDatabase<GeneDef>.GetNamed("VU_VampireLover", false);
        static Def LovinDependency = DefDatabase<GeneDef>.GetNamed("VRE_LovinDependency", false);
        static NeedDef VRE_Lovin = DefDatabase<NeedDef>.GetNamed("VRE_Lovin", false);
        public static void Postfix(SexProps props)
		{
			try
			{
				LustFeeding.RJWLustFeeding(props);
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}

		public static void RJWLustFeeding(SexProps props)
        {
            if((props.pawn != null) && (props.partner != null) && !xxx.is_animal(props.pawn) && !xxx.is_animal(props.partner))
            {
                //ModLog.Message($" Patch Worked");
                if(!props.pawn.IsCaravanMember() && !props.partner.IsCaravanMember())
                {
                    FillNeed(props.pawn);
                    FillNeed(props.partner);

                    RJWTryTakeBlood(props.pawn, props.partner);
					RJWTryTakeBlood(props.partner, props.pawn);
				}

            }

        }

        public static void FillNeed(Pawn pawn)
        {
            Pawn_GeneTracker genes;
            if (LovinDependency != null)
            {
                genes = pawn.genes;

                if (genes == null)
                {
                    return;
                }
                if (pawn.genes.HasActiveGene(RJW_Genes.GeneDefOf.VRE_LovinDependency))
                {
                    if (VRE_Lovin != null)
                    {
                        Pawn_NeedsTracker needs = pawn.needs;
                        ((needs != null) ? needs.TryGetNeed(VRE_Lovin) : null).CurLevel = 1f;
                    }
                }
            }
        }


            public static bool RJWTryTakeBlood(Pawn pawn, Pawn bloodBag)
        {

            if (bloodBag == null || pawn == null) return false;
            Pawn_GeneTracker genes = bloodBag.genes;
            if (genes!=null)
            if ((genes.GetFirstGeneOfType<Gene_Hemogen>() != null))
            {
                return false;
            }

            genes = pawn.genes;

            if (genes == null)
            {
                return false;
            }

            bool isLoveFeeder = false;

            if(LoveFeed != null)
            {
                if (RJWSettings.DevMode) RJW_Genes.ModLog.Message("LoveFeed checks");
                if (genes.HasActiveGene(RJW_Genes.GeneDefOf.RS_LoveFeed))
                {   

                    isLoveFeeder = true;
                }
            }
            if (VampireLover != null)
            {
                if (RJWSettings.DevMode) RJW_Genes.ModLog.Message("LoveFeed checks");
                if (genes.HasActiveGene(RJW_Genes.GeneDefOf.VU_VampireLover))
                {
                    isLoveFeeder = true;
                }
            }


            if (isLoveFeeder && (genes.GetFirstGeneOfType<Gene_Hemogen>() != null))
            {
                ModLog.Message($" Lovefeeder just finished loving: {xxx.get_pawnname(pawn)}");

                Gene_Hemogen gene_Hemogen = genes.GetFirstGeneOfType<Gene_Hemogen>();
                if (gene_Hemogen != null)
                {
                    if (gene_Hemogen.Value < gene_Hemogen.targetValue)
                    {
                        Hediff bloodBagBloodLoss = bloodBag.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.BloodLoss, false);
                        if (bloodBagBloodLoss != null)
                        {
                            float afterBite = bloodBagBloodLoss.Severity + 0.25f;
                            if ((afterBite > HediffDefOf.BloodLoss.lethalSeverity) || (afterBite > 0.49f))
                            {
                                //ModLog.Message($"{xxx.get_pawnname(pawn)} would have killed someone. {afterBite} > {HediffDefOf.BloodLoss.lethalSeverity}");
                                return false;
                            }

                        }                       
                        SanguophageUtility.DoBite(pawn, bloodBag, 0.2f, 0.1f, 0.2f, 1f, IntRange.one, ThoughtDefOf.FedOn, ThoughtDefOf.FedOn_Social);
                        ModLog.Message($"{xxx.get_pawnname(pawn)} snacked on {xxx.get_pawnname(bloodBag)}");
                        return true;
                        
                    }
                    else
                    {
                        ModLog.Message($"{xxx.get_pawnname(pawn)} not hungry. {gene_Hemogen.Value} > {gene_Hemogen.targetValue}");
                    }
                }
                


            }
            return false; ;
        }

    }

    
}
