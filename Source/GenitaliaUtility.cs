using RimWorld;
using Verse;
using rjw;

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
                if (gene.def.defName.Contains("rjw_genes") && gene.def.defName.EndsWith("_genitalia"))
                    if (!gene.Overridden)
                        return gene.def;
            }
                
            return GeneDefOf.rjw_genes_human_genitalia;
        }

        /// <summary>
        /// Adds a genital created from a given Def to the pawn.
        /// Does not alter/touch gender.
        /// </summary>
        /// <param name="pawn">The pawn whom to add the genital to,</param>
        /// <param name="genitalToAdd">The type of genital to be added</param>
        public static void AddGenitalToPawn(Pawn pawn,HediffDef genitalToAdd)
        {
            if (pawn == null || genitalToAdd == null)
                return;

            var partBPR = Genital_Helper.get_genitalsBPR(pawn);
            var additionalGenital = HediffMaker.MakeHediff(genitalToAdd, pawn);

            var CompHediff = additionalGenital.TryGetComp<rjw.CompHediffBodyPart>();
            if (CompHediff != null)
            {
                CompHediff.initComp(pawn);
                CompHediff.updatesize();
            }

            pawn.health.AddHediff(additionalGenital, partBPR);
        }


        public static HediffDef GetPenisForGene(GeneDef gene)
        {
            switch (gene.defName)
            {
                case "rjw_genes_human_genitalia":       return Genital_Helper.average_penis;
                case "rjw_genes_equine_genitalia":      return Genital_Helper.equine_penis;
                case "rjw_genes_canine_genitalia":      return Genital_Helper.canine_penis;
                case "rjw_genes_feline_genitalia":      return Genital_Helper.feline_penis;
                case "rjw_genes_demonic_genitalia":     return Genital_Helper.demon_penis;
                case "rjw_genes_dragon_genitalia":      return Genital_Helper.dragon_penis;
                case "rjw_genes_slime_genitalia":       return Genital_Helper.slime_penis;
                case "rjw_genes_ovipositor_genitalia":  return Genital_Helper.ovipositorM;

                default: return Genital_Helper.average_penis;
            }
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
    }
}
