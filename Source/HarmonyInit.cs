using Verse;
using HarmonyLib;


namespace RJW_Genes
{
    [StaticConstructorOnStartup]
    internal static class HarmonyInit
    {
        static HarmonyInit()
        {
            Harmony harmony = new Harmony("rjw_genes");
            harmony.PatchAll();
        }
    }
}