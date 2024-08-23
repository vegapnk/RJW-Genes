using HarmonyLib;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse; 

namespace RJW_Genes
{
    /// <summary>
    /// Makes the `can_masturbate` return false with certain genes.
    /// This is not the only relevant file, please check #127 and #147 on the matter.
    /// </summary>
    [HarmonyPatch(typeof(xxx), "can_masturbate")]
    public class Patch_BlockedMasturbation
    {
        public void PostFix(Pawn pawn, ref bool __result)
        {
            if (pawn != null && !pawn.IsAnimal() && pawn.genes != null)
            {
                __result = __result 
                    && !pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_blocked_masturbation)
                    && !pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_infectious_blocked_masturbation);
            }
        }

    }
}
