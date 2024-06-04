using Verse;
using HarmonyLib;
using System;
using rjw;
using RJWLoveFeeding;
using RimWorld;
using System.Linq;

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
            harmony.Patch(AccessTools.Method(typeof(SexAppraiser), nameof(SexAppraiser.would_rape)),
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
            
            harmony.Patch(AccessTools.Method(typeof(Quirk), nameof(Quirk.CountSatisfiedQuirks)),
                postfix: new HarmonyMethod(typeof(QuirkPatcher), nameof(QuirkPatcher.CountSatisfiedPostfix)));

            // Patch Licentia, if Licentia exists
            // Logic & Explanation taken from https://rimworldwiki.com/wiki/Modding_Tutorials/Compatibility_with_DLLs
            // Adjusted to use ModsConfig (which makes it work, the example above does not run out of the box)
            try
            {
                ((Action)(() =>
                {
                    if (ModsConfig.IsActive("LustLicentia.RJWLabs"))
                    {
                        // Gene: Cumflation Immunity [Prefix Patch]
                        harmony.Patch(AccessTools.Method(typeof(LicentiaLabs.CumflationHelper), nameof(LicentiaLabs.CumflationHelper.Cumflation)),
                            prefix: new HarmonyMethod(typeof(Patch_Cumflation), nameof(Patch_Cumflation.Prefix)));
                        // Gene: Generous Donor [Postfix Patch]
                        harmony.Patch(AccessTools.Method(typeof(LicentiaLabs.CumflationHelper), nameof(LicentiaLabs.CumflationHelper.TransferNutrition)),
                            postfix: new HarmonyMethod(typeof(Patch_TransferNutrition), nameof(Patch_TransferNutrition.Postfix)));
                        // Gene: CumEater [Postfix Patch] -- This is not exactly licentia, but the Generous-Donor Gene is only active with Licentia
                        harmony.Patch(AccessTools.Method(typeof(rjw.JobDriver_Sex), nameof(rjw.JobDriver_Sex.ChangePsyfocus)),
                            postfix: new HarmonyMethod(typeof(Patch_SexTicks_ChangePsyfocus), nameof(Patch_SexTicks_ChangePsyfocus.Postfix)));
                    }
                }))();
            }
            catch (TypeLoadException ex)
            {
                // To be expected for people without Licentia Labs
            }

        }
    }
}