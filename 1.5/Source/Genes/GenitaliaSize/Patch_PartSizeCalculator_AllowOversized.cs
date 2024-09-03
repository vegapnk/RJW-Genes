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
    /// This Patch was introduced after RJW 5.5 because the calculation / display of Genitalia-Sizes was changed.
    /// The primary difference was that the body-size was taken into account over the size noted in a genital.
    /// 
    /// For genes like evergrowth, this meant the size was "hardlocked" to the body size - but I don't want to change the bodysize. 
    /// So this patch carefully checks if the pawn has a relevant gene, and then instead returns the value from the hediff. 
    /// </summary>
    [HarmonyPatch(typeof(PartSizeCalculator), "GetScale")]
    public static class Patch_PartSizeCalculator_AllowOversized
    {
        public static void Postfix(Hediff hediff, ref float __result)
        {
            if (hediff is ISexPartHediff sexPart)
            {
                if (sexPart.GetPartComp().originalOwnerSize > __result)
                {
                    ModLog.Debug($"Found oversized Genital for {hediff.pawn}, changing from {__result} to {sexPart.GetPartComp().originalOwnerSize}");
                    __result = sexPart.GetPartComp().originalOwnerSize;
                }
            }
        }

        /*
        private static bool HasSupportedOversizingGene(Pawn pawn)
        {
            if (pawn == null || pawn.genes == null) return false;

            return pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_evergrowth);
        }
        */
    }
}
