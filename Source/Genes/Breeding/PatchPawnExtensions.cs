using HarmonyLib;
using rjw;
using Verse;

namespace RJW_Genes
{
    /// <summary>
    /// Kindly provided by 'shabalox' https://github.com/Shabalox/RJW_Genes_Addons/
    /// </summary>
    [HarmonyPatch(typeof(PawnExtensions), "RaceImplantEggs")]
    public static class PatchPawnExtensions
    {
        [HarmonyPostfix]
        public static void Postfix(Pawn pawn, ref bool __result)
        {
            if (!__result)
            {
                __result = GeneUtility.isInsectBreeder(pawn);
            }
        }
    }
}