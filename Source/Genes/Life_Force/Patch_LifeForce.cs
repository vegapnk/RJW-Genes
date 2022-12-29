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

			float absorb_factor = 0f;
			if (GeneUtility.HasLifeForce(props.partner))
			{
				if (props.sexType == xxx.rjwSextype.Oral || props.sexType == xxx.rjwSextype.Fellatio || props.sexType == xxx.rjwSextype.Sixtynine)
				{
					absorb_factor += 1f;
					//Currently taking the sum of all penises, maybe I should just consider one at random
				}
				else if (props.sexType == xxx.rjwSextype.Vaginal && GeneUtility.HasGeneNullCheck(props.partner, GeneDefOf.rjw_genes_vaginal_absorber))
				{
					absorb_factor += 1f;
				}
				else if (props.sexType == xxx.rjwSextype.Anal && GeneUtility.HasGeneNullCheck(props.partner, GeneDefOf.rjw_genes_anal_absorber))
				{
					absorb_factor += 1f;
				}
				else if (props.sexType == xxx.rjwSextype.DoublePenetration)
                {
					if (GeneUtility.HasGeneNullCheck(props.partner, GeneDefOf.rjw_genes_vaginal_absorber))
					{
						absorb_factor += 0.5f;
					}
					if (GeneUtility.HasGeneNullCheck(props.partner, GeneDefOf.rjw_genes_anal_absorber))
					{
						absorb_factor += 0.5f;
					}
				}
				if (absorb_factor != 0)
                {
					AbsorbFertilin(props, absorb_factor);
				}
			}
		}
		public static void AbsorbFertilin(SexProps props, float absorb_factor = 1f)
        {
			Pawn_GeneTracker genes = props.partner.genes;
			Gene_LifeForce gene = genes.GetFirstGeneOfType<Gene_LifeForce>();
			float multiplier = Rand.Range(0.10f, 0.40f); //Around quarter get ejected everytime pawn cums
			Hediff fertelin_lost = props.pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Fertilin_Lost);
			if (fertelin_lost == null)
            {
				Hediff new_fertelin_lost = HediffMaker.MakeHediff(HediffDefOf.Fertilin_Lost, props.pawn);
				props.pawn.health.AddHediff(new_fertelin_lost);
				new_fertelin_lost.Severity = multiplier;
			}
            else
            {
				multiplier *= 1 - fertelin_lost.Severity;
				fertelin_lost.Severity += multiplier;
				
            }
			gene.Resource.Value += CumUtility.GetTotalFluidAmount(props.partner) / 100 * absorb_factor * multiplier;
		}
	}
}
