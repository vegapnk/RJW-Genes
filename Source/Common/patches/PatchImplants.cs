using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using Verse;
using rjw;
using LewdBiotech;

namespace RJW_Genes
{

    public class PatchImplants
    {
        public static readonly ThoughtDef regretsStealingLovin = DefDatabase<ThoughtDef>.GetNamed("RegretsStealingLovin");
        public static readonly ThoughtDef stoleSomeLovin = DefDatabase<ThoughtDef>.GetNamed("StoleSomeLovin");
        public static readonly ThoughtDef bloodlustStoleSomeLovin = DefDatabase<ThoughtDef>.GetNamed("BloodlustStoleSomeLovin");
        public static readonly TraitDef rapist = DefDatabase<TraitDef>.GetNamed("Rapist");
        public static readonly TraitDef sadist = DefDatabase<TraitDef>.GetNamed("rjw_genes_sadist");
        static Dictionary<string, LaborState> laborStateMap = new Dictionary<string, LaborState>();
        static public void would_rape_PostFix(ref bool __result, Pawn rapist)
        {
            if (rapist.health.hediffSet.HasHediff(HediffDef.Named("LimbicStimulator")))
            {
                if (LBTSettings.devMode)
                {
                    RJW_GenesLogger.MessageGroupHead("Found LimbicStimulator hediff during xxx.would_rape check");
                    RJW_GenesLogger.MessageGroupBody("Pawn: " + rapist.NameShortColored + " (" + rapist.ThingID + ")");
                    RJW_GenesLogger.MessageGroupBody("__result (Before roll): " + __result);
                }
                __result = Rand.Chance(0.95f);
                if (LBTSettings.devMode)
                {
                    RJW_GenesLogger.MessageGroupFoot("__result (After roll): " + __result);
                }
            }
        }

        static public void is_rapist_PostFix(ref bool __result, Pawn pawn)
        {
            if (pawn.health.hediffSet.HasHediff(HediffDef.Named("LimbicStimulator")))
            {
                if (LBTSettings.devMode)
                {
                    RJW_GenesLogger.Message("Found LimbicStimulator hediff during xxx.is_rapist check for " + pawn.NameShortColored + " (" + pawn.ThingID + ")" + " with __result = " + __result + " - forcing to true");
                    __result = true;
                }
            }
        }

        static public void think_about_sex_Rapist_PostFix(ref ThoughtDef __result, Pawn pawn)
        {
            if (LBTSettings.regretStealingLovinThoughtDisabled) return;

            if (pawn.health.hediffSet.HasHediff(HediffDef.Named("LimbicStimulator")) && (__result == stoleSomeLovin || __result == bloodlustStoleSomeLovin) && !pawn.story.traits.HasTrait(rapist) && !pawn.story.traits.HasTrait(sadist))
            {
                __result = regretsStealingLovin;
            }
        }


        public static void MultiplyPregnancy(ref float __result, Pawn pawn)
        {
            if (pawn != null && pawn.health.hediffSet.HasHediff(HediffDef.Named("Bioscaffold")))
            {
                __result *= 2f;
            }
        }
    }
}
