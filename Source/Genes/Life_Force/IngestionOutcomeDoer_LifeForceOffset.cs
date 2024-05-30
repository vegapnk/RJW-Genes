using RimWorld;
using Verse;

namespace RJW_Genes
{
	/// <summary>
	/// This class checks for pawns with LifeForce and Cumeater Gene to add Fertilin when eating cum (the Item from RJW-Sexperience).
	/// </summary>
    public class IngestionOutcomeDoer_LifeForceOffset : IngestionOutcomeDoer
	{
		public const float DEFAULT_FERTILIN_PER_UNIT = 1f;
		public float FertilinPerUnit = 1f;

		protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested, int ingestedCount)
		{
			if (GeneUtility.HasLifeForce(pawn) && GeneUtility.IsCumEater(pawn))
            {
				float num = ingestedCount * this.FertilinPerUnit / 100;
				GeneUtility.OffsetLifeForce(GeneUtility.GetLifeForceGene(pawn), num);
			}
		}

	}
}
