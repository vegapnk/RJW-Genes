using HarmonyLib;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes.Genes.Patches
{
    /// <summary>
	/// This Patch hooks after "can_rape" and changes it to false for pawns that have no sex_need (are a-sexual). 
    /// This helps with #100, and is more of a non-intrusive improvement over the base game. 
	/// </summary>
	[HarmonyPatch(typeof(xxx), nameof(xxx.can_rape))]
    public class Patch_Asexual_CanRape
    {
        public static bool PostFix(Pawn pawn, ref bool __result)
        {
            if (pawn != null && pawn.genes != null && pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_no_sex_need))
            {
                __result = false;
            }
            return __result;
        }
    }
}
