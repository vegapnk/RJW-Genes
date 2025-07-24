using System;
using System.Linq;
using HarmonyLib;
using LLStretcher;
using RimWorld;
using rjw;
using RJWLoveFeeding;
using Verse;

namespace RJW_Genes
{
    [StaticConstructorOnStartup]
    internal static class HarmonyInit
    {
        
        static HarmonyInit()
        {
            Harmony harmony = new Harmony("rjw_genes");

            var RJW_Pregnancy_Tick_Prefixes = typeof(Hediff_Pregnant).GetMethod("Tick");
            harmony.Unpatch(RJW_Pregnancy_Tick_Prefixes, HarmonyPatchType.Prefix, "rjw");

            harmony.PatchAll();
            if (ModsConfig.BiotechActive)
            {
                harmony.Patch(typeof(SexUtility).GetMethod("ProcessSex"), new HarmonyMethod(typeof(LustFeeding), "Postfix", null));
            }
            harmony.Patch(AccessTools.Method(typeof(SexAppraiser), nameof(SexAppraiser.InMoodForRape)),
                postfix: new HarmonyMethod(typeof(PatchImplants), nameof(PatchImplants.would_rape_PostFix)));
            harmony.Patch(AccessTools.Method(typeof(xxx), nameof(xxx.is_rapist)),
                postfix: new HarmonyMethod(typeof(PatchImplants), nameof(PatchImplants.is_rapist_PostFix)));

            harmony.Patch(AccessTools.Method(typeof(AfterSexUtility), nameof(AfterSexUtility.think_about_sex_Rapist)),
                postfix: new HarmonyMethod(typeof(PatchImplants), nameof(PatchImplants.think_about_sex_Rapist_PostFix)));

            harmony.Patch(AccessTools.Method(typeof(PawnUtility), nameof(PawnUtility.BodyResourceGrowthSpeed)),
                postfix: new HarmonyMethod(typeof(PatchImplants), nameof(PatchImplants.MultiplyPregnancy)));

            harmony.Patch(AccessTools.Method(typeof(Hediff_Labor), nameof(Hediff_Labor.PostRemoved)),
                postfix: new HarmonyMethod(typeof(PatchLitteredBirth), nameof(PatchLitteredBirth.Hediff_Labor_PostRemovedPostFix)));

            // OvaryAgitator/Gene_LitteredBirths multibirth logic
            harmony.Patch(AccessTools.Method(typeof(Hediff_LaborPushing), nameof(Hediff_LaborPushing.PostRemoved)),
                postfix: new HarmonyMethod(typeof(PatchLitteredBirth), nameof(PatchLitteredBirth.Hediff_LaborPushing_PostRemovedPostFix)));

            //TODO:
            //1.6 quirks migrated to submod, disableing this patch for the time being.
            //harmony.Patch(AccessTools.Method(typeof(Quirk), nameof(Quirk.CountSatisfiedQuirks)),
            //postfix: new HarmonyMethod(typeof(QuirkPatcher), nameof(QuirkPatcher.CountSatisfiedPostfix)));

            
            //RJW.Sexualizer.sexualize_pawn
            harmony.Patch(AccessTools.Method(typeof(Sexualizer), nameof(Sexualizer.sexualize_pawn)),
                prefix: new HarmonyMethod(typeof(Patch_sexualize_pawn), nameof(Patch_sexualize_pawn.PreFix)));


            //Patch for Elastic Gene support with Eltro's Streching mod.
            if (ModLister.GetActiveModWithIdentifier("eltoro.stretching") != null)
            {
                ModLog.Debug("Patching eltoro.Streching for elasticity gene Support.");
                harmony.Patch(AccessTools.Method(typeof(Stretcher), "ApplyInjury"),
                    prefix: new HarmonyMethod(typeof(Patch_eltoro_streching), nameof(Patch_eltoro_streching.Prefix)));
            }

        }
    }
}