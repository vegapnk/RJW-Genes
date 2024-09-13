using HarmonyLib;
using rjw;
using System.Linq;
using Verse;

namespace RJW_Genes.Genes
{
    [HarmonyPatch]
    public static class Patch_AddNotifyOnGeneration
    {
        [HarmonyPatch(typeof(PawnGenerator), "GenerateGenes")]
        [HarmonyPostfix]
        public static void PawnGenerator_GenerateGenes_Postfix(Pawn pawn)
        {
            if (pawn == null || pawn.genes == null) return;

            if (GenitaliaUtility.PawnStillNeedsGenitalia(pawn)) return;

            foreach (var gene in pawn.genes.GenesListForReading)
            {
                if (gene != null && gene is RJW_Gene rjwGene)
                    rjwGene.Notify_OnPawnGeneration();
            }
        }
    }
}
