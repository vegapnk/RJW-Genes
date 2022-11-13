using rjw;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace RJW_Genes
{
    internal class GenitaliaChanger
    {

        /// <summary>
        /// This method changes the pawns genitalia to the given types of genitalia. 
        /// All genitals will be changed, e.g. pawns with 2 penises (for whatever reason) or is a futa, all genitals will be changed.
		/// Anuses are currently not changed, due to a small bug.
        /// </summary>
        /// <param name="pawn">the pawn who's genitals will be changed</param>
        /// <param name="penisReplacement">the new type of penis</param>
        /// <param name="vaginaReplacement">the new type of vagina</param>
        /// <param name="anusReplacement">the new type of anus (currently disabled)</param>
        public static void changeGenitalia(Pawn pawn, HediffDef penisReplacement, HediffDef vaginaReplacement, HediffDef anusReplacement)
        {
            var oldParts = Genital_Helper.get_AllPartsHediffList(pawn); 
			var partBPR = Genital_Helper.get_genitalsBPR(pawn);

			if (!oldParts.NullOrEmpty())
			{
				Hediff replacementGenital;
				CompHediffBodyPart CompHediff;

				foreach (Hediff existingGenital in oldParts)
				{
					if (IsArtificial(existingGenital))
						continue;

					replacementGenital = null;
					CompHediff = null;
					if (IsPenis(existingGenital))
						replacementGenital = HediffMaker.MakeHediff(penisReplacement, pawn, partBPR);

					if (IsVagina(existingGenital))
						replacementGenital = HediffMaker.MakeHediff(vaginaReplacement, pawn, partBPR);

					//TODO: Adding Anus in this way had an issue by multiplying Anusses. At the moment Anus are not covered.

					if (replacementGenital != null)
					{
						CompHediff = replacementGenital.TryGetComp<rjw.CompHediffBodyPart>();
						if (CompHediff != null)
						{
							CompHediff.initComp(pawn);
							CompHediff.updatesize();
						}
						GenderHelper.ChangeSex(pawn, () =>
						{
							pawn.health.RemoveHediff(existingGenital);
							pawn.health.AddHediff(replacementGenital, partBPR);
						});
					}
				}
			}
			else
			{
				Messages.Message("RJW_Genes_GenitalsNotAlterable".Translate(pawn), pawn, MessageTypeDefOf.SilentInput);
				return;
			}

		}

		private static bool IsPenis(Hediff candidate)
        {
            return 
                candidate.def.defName.ToLower().Contains("penis") ||
                candidate.def.defName.ToLower().Contains("ovipositorm") ||
                candidate.def.defName.ToLower().Contains("tentacle");
        }

        private static bool IsVagina(Hediff candidate)
        {
            return
                candidate.def.defName.ToLower().Contains("vagina") ||
                candidate.def.defName.ToLower().Contains("ovipositorf");
        }

        private static bool IsAnus(Hediff candidate)
        {
            return candidate.def.defName.ToLower().Contains("anus");        }

		private static bool IsArtificial(Hediff candidate)
        {
			return candidate.def.defName.ToLower().Contains("bionic") || candidate.def.defName.ToLower().Contains("archo");
        }

    }
}
