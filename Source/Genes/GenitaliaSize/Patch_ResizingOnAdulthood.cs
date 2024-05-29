using HarmonyLib;
using Verse;

namespace RJW_Genes
{
    /// <summary>
    /// This Patch adds behavior to all resizing genes: 
    /// At Age RESIZING_MIN_AGE the Pawns Resizing Genes will trigger again, if not already triggered somewhere else.
    /// This is meant to allow kids to grow up without resized genitals, and resize later (Fixing #11). 
    /// 
    /// See `Gene_GenitaliaResizingGene` for a short summary of Issue #34. 
    /// </summary>
    [HarmonyPatch(typeof(Pawn_AgeTracker), "BirthdayBiological")]
    public class Patch_ResizingOnAdulthood
    {

        static void Postfix(Pawn ___pawn, int birthdayAge)
        {
            if (birthdayAge >= RJW_Genes_Settings.rjw_genes_resizing_age)
            {
                foreach(Gene_GenitaliaResizingGene gene in GeneUtility.GetGenitaliaResizingGenes(___pawn))
                {
                    if (!gene.ResizingWasApplied)
                    {
                        gene.Resize();
                        gene.ResizingWasApplied = true;
                    }
                }
            }
        }
    }
}