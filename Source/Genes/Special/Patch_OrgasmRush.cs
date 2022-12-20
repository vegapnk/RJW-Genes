using HarmonyLib;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJW_Genes
{
	
	[HarmonyPatch(typeof(SexUtility), nameof(SexUtility.SatisfyPersonal))]
	public static class Patch_OrgasmRush
	{

		private const float REST_INCREASE = 0.05f;

		public static void Postfix(SexProps props)
		{
			// ShortCuts: Exit Early if Pawn or Partner are null (can happen with Animals or Masturbation)
			if (props.pawn == null || !props.hasPartner())
				return;

			if (props.pawn.genes != null && props.pawn.genes.HasGene(GeneDefOf.rjw_genes_orgasm_rush))
            {
				props.pawn.needs.rest.CurLevel += REST_INCREASE;
            }
		}
	}

}
