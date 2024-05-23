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
           
        }
    }
}