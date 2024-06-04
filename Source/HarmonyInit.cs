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
        public static Type Gene_Randomizer_Instance = null;
        static HarmonyInit()
        {
            Harmony harmony = new Harmony("rjw_genes");

            var original = typeof(Hediff_Pregnant).GetMethod("Tick");
            harmony.Unpatch(original, HarmonyPatchType.Prefix, "rjw");

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
            harmony.Patch(AccessTools.Method(typeof(Hediff_LaborPushing), nameof(Hediff_LaborPushing.PostRemoved)),
                postfix: new HarmonyMethod(typeof(PatchLitteredBirth), nameof(PatchLitteredBirth.Hediff_LaborPushing_PostRemovedPostFix)));
  
            harmony.Patch(AccessTools.Method(typeof(Quirk), nameof(Quirk.CountSatisfiedQuirks)),
                postfix: new HarmonyMethod(typeof(QuirkPatcher), nameof(QuirkPatcher.CountSatisfiedPostfix)));

            /*
            try
            {
                Gene_Randomizer_Instance = (from asm in AppDomain.CurrentDomain.GetAssemblies()
                                            from type in asm.GetTypes()
                                            where type.IsClass && type.Name == "Gene_Randomizer"
                                            select type).Single();
            }
            catch (Exception ex) { }
            Def mytosis_mutation = DefDatabase<GeneDef>.GetNamed("rjw_genes_mytosis_mutation", false);
            if (mytosis_mutation != null)
            {
                harmony.Patch(AccessTools.Method(Gene_Randomizer_Instance, "PostAdd"),
                prefix: new HarmonyMethod(typeof(Patch_Waster), nameof(Patch_Waster.Gene_Randomizer_Prefix)));
            }*/



        }
    }
}