using RimWorld;
using Verse;
using rjw;
using System;

namespace RJW_Genes
{
    public class GenitaliaUtility
    {

        /// <summary>
        /// Returns the first (non) overwritten gene from the rjw_genes genitalia genes.
        /// In case the pawn has none, as default the human one is returned.
        /// </summary>
        /// <param name="pawn">the pawn whom to find genitaliagenes for</param>
        /// <returns>The first GeneDef of the pawn related to GenitaliaTypes</returns>
        public static GeneDef GetGenitaliaTypeGeneForPawn(Pawn pawn)
        {
            foreach (var gene in pawn.genes.GenesListForReading)         
            { 
                if (gene is Gene_GenitaliaType)
                    if (!gene.Overridden)
                            return gene.def;                       
            }
            return null;
        }


        public static HediffDef GetPenisForGene(GeneDef gene)
        {
            return gene?.GetModExtension<GenitaliaTypeExtension>()?.penis ?? Genital_Helper.average_penis;
        }


        public static HediffDef GetVaginaForGene(GeneDef gene)
        {
            return gene?.GetModExtension<GenitaliaTypeExtension>()?.vagina ?? Genital_Helper.average_vagina;
        }

        public static HediffDef GetAnusForGene(GeneDef gene)
        {
            //TODO: Do I want the default to be generic or average for feline,equine and canine?
            return gene?.GetModExtension<GenitaliaTypeExtension>()?.anus ?? Genital_Helper.average_anus;
        }

        public static HediffDef GetBreastsForGene(GeneDef gene)
        {
            return gene?.GetModExtension<GenitaliaTypeExtension>()?.breasts ?? Genital_Helper.average_breasts;
        }

        public static bool PawnStillNeedsGenitalia(Pawn pawn)
        {
            // There is the issue that the genes fire in a pseudo-random order 
            // Hence it can happen that the pawn still needs genitalia 
            // I wanted to make a simple lookup, but I think the genes are applied for all humans encountered so it could be huge
            // So the heuristic is to check if the pawn has any of the 3 standard genitalia OR has all genes ticked that says "I don't want genitalia".
            if (pawn == null) return false;

            bool pawn_has_any_genitalia = 
                Genital_Helper.has_genitals(pawn) || Genital_Helper.has_anus(pawn) || Genital_Helper.has_breasts(pawn);

            bool pawn_is_not_supposed_to_have_genitalia =
                pawn.genes.GenesListForReading.Any(x => x.def.defName == "rjw_genes_no_penis");

            if (pawn_is_not_supposed_to_have_genitalia)
                return false;
            else
                return !pawn_has_any_genitalia;

        }

        public static bool IsBreasts(Hediff candidate)
        {
            return candidate.def.defName.ToLower().Contains("breast");
        }
    }
}
