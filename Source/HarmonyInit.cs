using Verse;
using HarmonyLib;
using System;
using rjw;
using RJWLoveFeeding;

namespace RJW_Genes
{
    [StaticConstructorOnStartup]
    internal static class HarmonyInit
    {
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

            // Patch Licentia, if Licentia exists
            // Logic & Explanation taken from https://rimworldwiki.com/wiki/Modding_Tutorials/Compatibility_with_DLLs
            // Adjusted to use ModsConfig (which makes it work, the example above does not run out of the box)

        }
    }
}