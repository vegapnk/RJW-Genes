using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace RJW_Genes
{
    public class IngestionOutcomeDoer_LifeForceOffset : IngestionOutcomeDoer
    {
		protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested)
		{
			if (GeneUtility.HasLifeForce(pawn) && GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_cum_eater))
            {
				float num = ingested.stackCount * this.FertilinPerUnit / 100;
				GeneUtility.OffsetLifeForce(GeneUtility.GetLifeForceGene(pawn), num);
			}
		}
		public float FertilinPerUnit = 1f;
	}
}
