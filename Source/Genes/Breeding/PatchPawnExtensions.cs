using HarmonyLib;
using rjw;
using Verse;

namespace RJW_Genes
{
    /// <summary>
    /// Kindly provided by 'shabalox' https://github.com/Shabalox/RJW_Genes_Addons/
    /// 
    /// Note on the logic: the result mentioned below is changing the result of fertilization (true or false) to true if the pawn has the insect-breeder gene.
    /// </summary>
    [HarmonyPatch(typeof(PawnExtensions), "RaceImplantEggs")]
    public static class PatchPawnExtensions
    {
        [HarmonyPostfix]
        public static void Postfix(Pawn pawn, ref bool __result)
        {
            if (!__result)
            {
                __result = GeneUtility.IsInsectBreeder(pawn);
            }
        }
    }
}