using Verse;
using HarmonyLib;
using System;
using rjw;
using RJWLoveFeeding;
using RimWorld;
using System.Linq;
using LicentiaLabs;

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


            // Patch Cumpilation, if Cumpilation exists
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                ((Action)(() =>
                {
                    if (ModsConfig.IsActive("vegapnk.cumpilation"))
                    {
                        // Gene: Inflatable [Postfix Patch]
                        harmony.Patch(AccessTools.Method(typeof(SexUtility), nameof(SexUtility.TransferFluids)),
                            postfix: new HarmonyMethod(typeof(Patch_Cumpilation_Inflatable), nameof(Patch_Cumpilation_Inflatable.PostFix)));
                        // Gene: Inflation-Resistance [Postfix Patch]
                        harmony.Patch(AccessTools.Method(typeof(Cumpilation.Cumflation.CumflationUtility), nameof(Cumpilation.Cumflation.CumflationUtility.CanBeCumflated)),
                            postfix: new HarmonyMethod(typeof(Patch_Cumpilation_BlockCumflation), nameof(Patch_Cumpilation_BlockCumflation.PostFix)));
                        // Gene: Inflation-Resistance [Postfix Patch]
                        harmony.Patch(AccessTools.Method(typeof(Cumpilation.Cumflation.StuffingUtility), nameof(Cumpilation.Cumflation.StuffingUtility.CanBeStuffed)),
                            postfix: new HarmonyMethod(typeof(Patch_Cumpilation_BlockStuffing), nameof(Patch_Cumpilation_BlockStuffing.PostFix)));
                        // Gene: Living Cumbucket [Postfix Patch] 
                        harmony.Patch(AccessTools.Method(typeof(SexUtility), nameof(SexUtility.SatisfyPersonal)),
                            postfix: new HarmonyMethod(typeof(Patch_LivingCumbucket_StackHediff), nameof(Patch_LivingCumbucket_StackHediff.PostFix)));
                    }
                }))();
            }
            catch (TypeLoadException ex)
            {
                // To be expected for people without Cumpilation
                // It's ok to do nothing then. 
            }
#pragma warning restore CS0168 // Variable is declared but never used
        }
    }
}