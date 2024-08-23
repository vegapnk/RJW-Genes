using Verse;
using rjw;

namespace RJW_Genes
{
    public class CumUtility
    {

        public static void MultiplyFluidAmountBy(Pawn pawn, float multiplier)
        {
			var partBPR = Genital_Helper.get_genitalsBPR(pawn);
			var parts = Genital_Helper.get_PartsHediffList(pawn, partBPR);

			if (!parts.NullOrEmpty())
			{
				CompHediffBodyPart CompHediff;

				foreach (Hediff part in parts)
				{
					if (GenitaliaChanger.IsArtificial(part))
						continue;

					if (rjw.Genital_Helper.is_penis(part))
					{
						CompHediff = part.TryGetComp<rjw.CompHediffBodyPart>();
						if (CompHediff != null)
						{
							CompHediff.FluidAmmount *= multiplier;
						}
					}
				}

			}

		}

		/// <summary>
		/// Looks up the "MultiplierExtensions" Value for a given Gene, with a fall back. 
		/// Returns the fallback if there is no Extension, or if the Multiplier is smaller than 0. 
		/// </summary>

        public static float LookupCumMultiplier(Gene gene, float FALLBACK = 3.0f) => LookupCumMultiplier(gene.def,FALLBACK);
        public static float LookupCumMultiplier(GeneDef def, float FALLBACK = 3.0f)
        {
            MultiplierExtension multiplier = def.GetModExtension<MultiplierExtension>();
            if (multiplier == null || multiplier.multiplier < 0)
                return FALLBACK;
            else return multiplier.multiplier;
        }


        //Get total fluidamount a person has.
        public static float GetTotalFluidAmount(Pawn pawn, float multiplier = 1f)
		{
			var partBPR = Genital_Helper.get_genitalsBPR(pawn);
			var parts = Genital_Helper.get_PartsHediffList(pawn, partBPR);
			float total_cum = 0;
			if (!parts.NullOrEmpty())
			{
				CompHediffBodyPart CompHediff;

				foreach (Hediff part in parts)
				{
					if (GenitaliaChanger.IsArtificial(part))
						continue;

					if (rjw.Genital_Helper.is_penis(part))
					{
						CompHediff = part.TryGetComp<rjw.CompHediffBodyPart>();
						if (CompHediff != null)
						{
							total_cum += CompHediff.FluidAmmount * CompHediff.FluidModifier * multiplier;
						}
					}
				}

			}
			return total_cum;

		}
	}
}
