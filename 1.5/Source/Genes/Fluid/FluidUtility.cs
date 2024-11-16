using Verse;
using rjw;
using RimWorld;

namespace RJW_Genes
{
	public class FluidUtility
	{

		public static void MultiplyFluidAmountBy(Pawn pawn, float multiplier)
		{
			var partBPR = Genital_Helper.get_genitalsBPR(pawn);
			var parts = Genital_Helper.get_PartsHediffList(pawn, partBPR);

			foreach (Hediff part in parts)
			{
				// Right now: Ignore Breasts, only do 
				if (part is ISexPartHediff sexPart && (Genital_Helper.is_penis(part) || Genital_Helper.is_vagina(part)))
					sexPart.GetPartComp().partFluidMultiplier *= multiplier;
			}
		}

		/// <summary>
		/// Looks up the "MultiplierExtensions" Value for a given Gene, with a fall back. 
		/// Returns the fallback if there is no Extension, or if the Multiplier is smaller than 0. 
		/// </summary>
		public static float LookupFluidMultiplier(Gene gene, float FALLBACK = 3.0f) => LookupFluidMultiplier(gene.def, FALLBACK);
		public static float LookupFluidMultiplier(GeneDef def, float FALLBACK = 3.0f)
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
			float total_fluid = 0;
			if (!parts.NullOrEmpty())
			{
				foreach (Hediff part in parts)
				{
					if (GenitaliaChanger.IsArtificial(part))
						continue;

					if (part is ISexPartHediff sexPart)
					{
						total_fluid += sexPart.GetPartComp().FluidAmount;
					}
				}
			}
			return total_fluid;
		}

		public static void ChangeFluids(Pawn pawn, SexFluidDef penisFluidDefs = null, SexFluidDef vaginaFluidDefs = null, SexFluidDef breastFluidDefs = null, SexFluidDef otherFluidDefs = null)
		{
			if (pawn == null) return;

			var parts = Genital_Helper.get_AllPartsHediffList(pawn);
			foreach (Hediff part in parts)
			{
				if (part is ISexPartHediff sexPart)
				{
					var comp = sexPart.GetPartComp();
					if (penisFluidDefs != null && Genital_Helper.is_penis(part))
						comp.Fluid = penisFluidDefs;
					else if (vaginaFluidDefs != null && Genital_Helper.is_vagina(part))
						comp.Fluid = vaginaFluidDefs;
					else if (breastFluidDefs != null && GenitaliaUtility.IsBreasts(part))
						comp.Fluid = breastFluidDefs;
					else
						comp.Fluid = otherFluidDefs;
				}
			}
		}
	}
}
