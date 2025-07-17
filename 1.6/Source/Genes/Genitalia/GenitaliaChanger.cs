using rjw;
using RimWorld;
using Verse;
using System.Collections.Generic;
//using rjw.Modules.Interactions.DefModExtensions;

namespace RJW_Genes
{
    internal class GenitaliaChanger
    {

        /// <summary>
        /// This method changes the pawns genitalia to the given types of genitalia. 
        /// All genitals will be changed, e.g. pawns with 2 penises (for whatever reason) or is a futa, all genitals will be changed.
        /// </summary>
        /// <param name="pawn">the pawn who's genitals will be changed</param>
        /// <param name="penisReplacement">the new type of penis</param>
        /// <param name="vaginaReplacement">the new type of vagina</param>
        /// <param name="anusReplacement">the new type of anus</param>
        public static void ChangeGenitalia(Pawn pawn, HediffDef penisReplacement = null, HediffDef vaginaReplacement = null, HediffDef anusReplacement = null , HediffDef breastsReplacement = null)
        {
            var oldParts = Genital_Helper.get_AllPartsHediffList(pawn);
			BodyPartRecord correctBPR;

			if (!oldParts.NullOrEmpty())
			{
				Hediff replacementGenital;
				HediffComp_SexPart CompHediff;

				foreach (Hediff existingGenital in oldParts)
				{
					if (IsArtificial(existingGenital))
						continue;
					correctBPR = Genital_Helper.get_genitalsBPR(pawn);

					replacementGenital = null;
					CompHediff = null;
					if (Genital_Helper.is_penis(existingGenital) && penisReplacement != null && existingGenital.def != penisReplacement)
						replacementGenital = HediffMaker.MakeHediff(penisReplacement, pawn, correctBPR);

					if (Genital_Helper.is_vagina(existingGenital) && vaginaReplacement != null && existingGenital.def != vaginaReplacement)
						replacementGenital = HediffMaker.MakeHediff(vaginaReplacement, pawn, correctBPR);

					if (IsBreast(existingGenital) && breastsReplacement != null && existingGenital.def != breastsReplacement)
					{
                        correctBPR = Genital_Helper.get_breastsBPR(pawn);
                        replacementGenital = HediffMaker.MakeHediff(breastsReplacement, pawn, correctBPR);
					}

                    if (IsAnus(existingGenital) && anusReplacement != null && existingGenital.def != anusReplacement)
					{
						correctBPR = Genital_Helper.get_anusBPR(pawn);
						replacementGenital = HediffMaker.MakeHediff(anusReplacement, pawn, correctBPR);
					}

					if (replacementGenital != null)
					{
						CompHediff = replacementGenital.TryGetComp<rjw.HediffComp_SexPart>();
						if (CompHediff != null)
						{
							CompHediff.Init(pawn);
							CompHediff.UpdateSeverity();
						}
						GenderHelper.ChangeSex(pawn, () =>
						{
							pawn.health.RemoveHediff(existingGenital);
							pawn.health.AddHediff(replacementGenital, correctBPR);
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

        public static bool IsAnus(Hediff candidate)
        {
			if (candidate == null || !(candidate is ISexPartHediff)) return false;
			return candidate.def.defName.ToLower().Contains("anus");        
		}
        
        private static bool IsBreastFamiliy(GenitalFamily family) => family switch
        {
	        GenitalFamily.Breasts => true,
	        _ => false
        };


        public static bool IsBreast(Hediff candidate)
        {
            if (candidate == null || !(candidate is ISexPartHediff)) return false;
            if (candidate.def is not HediffDef_SexPart def) return false;
	        return IsBreastFamiliy(def.genitalFamily);
        }

        public static bool IsArtificial(Hediff candidate)
        {
            if (candidate == null || !(candidate is ISexPartHediff)) return false;
            return candidate.def.defName.ToLower().Contains("bionic") || candidate.def.defName.ToLower().Contains("archo");
        }

		public static void RemoveAllGenitalia(Pawn pawn)
		{
			if (pawn == null) return;
			var parts = Genital_Helper.get_AllPartsHediffList(pawn);
			foreach (var part in parts)
            {
				pawn.health.RemoveHediff(part);
            }
		}



    }
}
