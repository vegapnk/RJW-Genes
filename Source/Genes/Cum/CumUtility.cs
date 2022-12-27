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

		//Get total fluidamount a persom has.
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
							total_cum += CompHediff.FluidAmmount * multiplier;
						}
					}
				}

			}
			return total_cum;

		}
	}
}
