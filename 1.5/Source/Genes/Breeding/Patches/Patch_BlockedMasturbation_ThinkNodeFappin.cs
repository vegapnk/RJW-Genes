
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
    /// This is a second Patch that blocks masturbation by making it "unthinkable".
    /// I kept the original Patch_BlockedMasturbation.cs because maybe someone else is checking the "xxx.canMasturbate(Pawn)"
    /// The behaviour was extracted from Sexperience by tinymechanoid333 in #147.
    /// Issue was tracked in #127.
    /// </summary>
    [HarmonyPatch(typeof(ThinkNode_ChancePerHour_Fappin), "MtbHours")]
    public static class Patch_BlockedMasturbation_ThinkNodeFappin
    {
        public static void Postfix(Pawn p, ref float __result)
        {
            if (p != null && !p.IsAnimal() && p.genes != null)
            {
                if (p.genes.HasActiveGene(GeneDefOf.rjw_genes_blocked_masturbation) || p.genes.HasActiveGene(GeneDefOf.rjw_genes_infectious_blocked_masturbation))
                {
                    __result = -2f;
                }
            }
        }
    }
}