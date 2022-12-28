using HarmonyLib;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace RJW_Genes
{

	[HarmonyPatch(typeof(SexUtility), nameof(SexUtility.SatisfyPersonal))]
	public static class Patch_LifeForce
	{
		public static void Postfix(SexProps props)
		{
			// ShortCuts: Exit Early if Pawn or Partner are null (can happen with Animals or Masturbation)
			if (props.pawn == null || !props.hasPartner())
				return;

			float factor = 1f;
			if (GeneUtility.HasLifeForce(props.pawn))
			{
				if (props.sexType == xxx.rjwSextype.Oral || props.sexType == xxx.rjwSextype.Fellatio || props.sexType == xxx.rjwSextype.Sixtynine)
				{
					AbsorbFertilin(props, factor);
					//Currently taking the sum of all penises, maybe I should just consider one at random
				}
				else if (props.sexType == xxx.rjwSextype.Vaginal && GeneUtility.HasGeneNullCheck(props.pawn, GeneDefOf.rjw_genes_vaginal_absorber))
				{
					AbsorbFertilin(props, factor);
				}
				else if (props.sexType == xxx.rjwSextype.Anal && GeneUtility.HasGeneNullCheck(props.pawn, GeneDefOf.rjw_genes_anal_absorber))
				{
					AbsorbFertilin(props, factor);
				}
				else if (props.sexType == xxx.rjwSextype.DoublePenetration)
                {
					if (GeneUtility.HasGeneNullCheck(props.pawn, GeneDefOf.rjw_genes_vaginal_absorber))
					{
						AbsorbFertilin(props, 0.5f);
					}
					if (GeneUtility.HasGeneNullCheck(props.pawn, GeneDefOf.rjw_genes_anal_absorber))
					{
						AbsorbFertilin(props, 0.5f);
					}
				}
			}
		}
		public static void AbsorbFertilin(SexProps props, float factor = 1f)
        {
			Pawn_GeneTracker genes = props.pawn.genes;
			Gene_LifeForce gene = genes.GetFirstGeneOfType<Gene_LifeForce>();
			gene.Resource.Value += CumUtility.GetTotalFluidAmount(props.partner) / 100 * factor;
		}
	}

	

}
