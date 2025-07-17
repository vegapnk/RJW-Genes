using HarmonyLib;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
	
	[HarmonyPatch(typeof(SexUtility), nameof(SexUtility.SatisfyPersonal))]
	public static class Patch_OrgasmRush
	{

		private const float REST_INCREASE = 0.05f;
		private const float ORGASMS_NEEDED_FOR_SUPERCHARGE = 3.0f;

		public static void Postfix(SexProps props)
		{
			// ShortCuts: Exit Early if Pawn or Partner are null (can happen with Masturbation or other nieche-cases)
			if (props == null || props.pawn == null || !props.hasPartner())
				return;

			// Exit for Animals - Animals can't get or trigger Orgasm Rushes. Fixes #15
			if (props.pawn.IsAnimal() || props.partner.IsAnimal())
				return;

			if (props.pawn.genes != null && props.pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_orgasm_rush))
            {

				// Pump up Wake-Ness
				if (props.pawn.needs.rest != null)
					props.pawn.needs.rest.CurLevel += REST_INCREASE;

				// Add or Update Hediff for Orgasm Rush
				Hediff rush = GetOrgasmRushHediff(props.pawn);
				float added_severity = props.orgasms / ORGASMS_NEEDED_FOR_SUPERCHARGE;
				rush.Severity += added_severity;
				// Severity should be capped to 1 by the XML logic
			}

		}

		/// <summary>
		/// Helps to get the Orgasm Rush Hediff of a Pawn. If it does not exist, one is added. 
		/// </summary>
		/// <param name="orgasmed">The pawn that had the orgasm, for which a hediff is looked up or created.</param>
		/// <returns></returns>
		public static Hediff GetOrgasmRushHediff(Pawn orgasmed)
		{
			Hediff orgasmRushHediff = orgasmed.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.rjw_genes_orgasm_rush_hediff);
			if (orgasmRushHediff == null)
			{
				orgasmRushHediff = HediffMaker.MakeHediff(HediffDefOf.rjw_genes_orgasm_rush_hediff, orgasmed);
				orgasmRushHediff.Severity = 0;
				orgasmed.health.AddHediff(orgasmRushHediff);
			}
			return orgasmRushHediff;
		}
	}


}
