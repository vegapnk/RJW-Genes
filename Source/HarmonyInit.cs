using Verse;
using HarmonyLib;
using System;

namespace RJW_Genes
{
    [StaticConstructorOnStartup]
    internal static class HarmonyInit
    {
        static HarmonyInit()
        {
            Harmony harmony = new Harmony("rjw_genes");
            harmony.PatchAll();

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
                        //Eat cumflation 
                        harmony.Patch(AccessTools.Method(typeof(rjw.JobDriver_Sex), nameof(rjw.JobDriver_Sex.ChangePsyfocus)),
                            postfix: new HarmonyMethod(typeof(Patch_SexTicks_ChangePsyfocus), nameof(Patch_SexTicks_ChangePsyfocus.Postfix)));
                    }
                }))();
            }
            catch (TypeLoadException ex) {
                // To be expected for people without Licentia Labs
            }
        }
    }
}