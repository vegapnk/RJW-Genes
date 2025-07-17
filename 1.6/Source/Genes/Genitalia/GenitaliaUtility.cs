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
            return candidate.def.defName.ToLower().Contains("breast") || candidate.def.defName.ToLower().Contains("udder");
        }

        /// <summary>
        /// Returns the biggest penis of a pawn. 
        /// In case of a identical severity, the highest body size is returned. 
        /// For women, or pawns without a penis, null is returned. 
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns>The biggest penis of a pawn. Null on women or error.</returns>
        public static Hediff GetBiggestPenis(Pawn pawn)
        {
            Hediff best = null;
            var parts = Genital_Helper.get_AllPartsHediffList(pawn);

            foreach (var part in parts)
            {
                if (Genital_Helper.is_sex_part(part) && Genital_Helper.is_penis(part))
                {
                    if (best == null) best = part;

                    // On a draw of size, we check the body-size. 
                    if (part.Severity == best.Severity) {
                        var partSize = part.TryGetComp<rjw.HediffComp_SexPart>();
                        var bestSize = part.TryGetComp<rjw.HediffComp_SexPart>();
                        if (partSize == null || bestSize == null) { continue; }

                        best = partSize.originalOwnerSize > bestSize.originalOwnerSize ? part : best;
                    } else if (part.Severity > best.Severity) {
                        best = part;
                    }
                }
            }

            return best;
        }

        public static float GetBodySizeOfSexPart(Hediff part)
        {
            if (part == null || part.TryGetComp<rjw.HediffComp_SexPart>() == null)
                return 0.0f;
            else
                return part.TryGetComp<rjw.HediffComp_SexPart>().originalOwnerSize;
        }

        /// <summary>
        /// Checks whether a pawn needs to have breasts, for genes that add or change breast sizes and numbers. 
        /// This was a little oversight noticed in #138.
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns>True if the Pawn with his current gender and genes should have breasts. False otherwise. </returns>
        public static bool ShouldHaveBreasts(Pawn pawn)
        {
            // if anything is missing, just return True for Women and False for anyone else.
            if (pawn == null || pawn.genes == null)
                return pawn.gender == Gender.Female;

            if (pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_featureless_chest))
                return false;
            if (pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_no_breasts))
                return false;

            if (pawn.gender == Gender.Female)
            {
                if (pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_femboy))
                    return false;

                // Default Case: Women do have breasts.
                return true;
            } 
            if (pawn.gender == Gender.Male)
            {
                // Default Case: Men do not have Breasts.
                return false;
            }

            return false;
        }
    }

}
