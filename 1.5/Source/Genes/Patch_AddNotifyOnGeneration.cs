using HarmonyLib;
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
            if (pawn.genes == null) return;

            foreach(var gene in pawn.genes.GenesListForReading)
            {
                if (gene is RJW_Gene rjwGene)
                    rjwGene.Notify_OnPawnGeneration();
            }
        }
    }
}
