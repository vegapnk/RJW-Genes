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

			// Exit if pawn has fertilin themself, it won't give any if it has lifeforce themself.
			if (GeneUtility.HasLifeForce(props.pawn))
			{
				return;
			}

			//Summary//
			//We use the positions of the pawn (dom or sub) and based on that which interactions will transfer fertilin 
			//By checking isreceiver we know if the succubus is the dom or the sub and if the situation is reverse we also swap the function we use
			//
			float absorb_factor = 0f;
			if (GeneUtility.HasLifeForce(props.partner))
			{
				Pawn succubus = props.partner;

				if (!props.isRevese)
				{
					if (props.isReceiver)
					{
						// Scenario Dom Succubus, normal
						absorb_factor = BaseDom(props, succubus);
					}
					else
					{
						// Scenario Sub Succubus, normal
						absorb_factor = BaseSub(props, succubus);
					}
				}
				else
				{
					if (props.isReceiver)
					{
						// Scenario Dom Succubus, Reverse
						absorb_factor = BaseSub(props, succubus);
					}
					else
					{
						// Scenario Sub Succubus, Reverse
						absorb_factor = BaseDom(props, succubus);
					}
				}

				//If we remove this check fertelin is always lost, but the succubus doesn't always gain any
				if (absorb_factor != 0f)
				{
					AbsorbFertilin(props, absorb_factor);
				}

				if (GeneUtility.HasGeneNullCheck(succubus, GeneDefOf.rjw_genes_drainer) && !props.pawn.health.hediffSet.HasHediff(HediffDefOf.Succubus_Drained))
				{
					props.pawn.health.AddHediff(HediffDefOf.Succubus_Drained);
					GeneUtility.OffsetLifeForce(GeneUtility.GetLifeForceGene(succubus), 0.25f);
				}
			}
		}
		public static void AbsorbFertilin(SexProps props, float absorb_factor = 1f)
		{
			Pawn_GeneTracker genes = props.partner.genes;
			Gene_LifeForce gene = genes.GetFirstGeneOfType<Gene_LifeForce>();
			Hediff fertilin_lost = props.pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Fertilin_Lost);
			//Around quarter get ejected everytime pawn cums
			float multiplier = Rand.Range(0.10f, 0.40f); 
			

			//Create a new ferilin_lost hediff or reduce multiplier
			if (fertilin_lost == null)
			{
				Hediff new_fertilin_lost = HediffMaker.MakeHediff(HediffDefOf.Fertilin_Lost, props.pawn);
				props.pawn.health.AddHediff(new_fertilin_lost);
				new_fertilin_lost.Severity = multiplier;
			}
			else
			{
				multiplier *= 1 - fertilin_lost.Severity;
				fertilin_lost.Severity += multiplier;
			}
			//More in the tank means more to give
			if (props.pawn.Has(Quirk.Messy))
			{
				multiplier *= 2;
			}
			//Currently taking the sum of all penises, maybe I should just consider one at random
			float valuechange = CumUtility.GetTotalFluidAmount(props.pawn) / 100 * absorb_factor * multiplier;
			GeneUtility.OffsetLifeForce(GeneUtility.GetLifeForceGene(props.partner), valuechange);
			//gene.Resource.Value += CumUtility.GetTotalFluidAmount(props.pawn) / 100 * absorb_factor * multiplier;
		}


		public static float BaseDom(SexProps props, Pawn succubus)
		{
			float absorb_factor = 0f;
			if (props.sexType == xxx.rjwSextype.Sixtynine)
			{
				absorb_factor += 1f;
			}
			return absorb_factor;
		}

		public static float BaseSub(SexProps props, Pawn succubus)
        {
			float absorb_factor = 0f;
			if (props.sexType == xxx.rjwSextype.Oral || props.sexType == xxx.rjwSextype.Fellatio || props.sexType == xxx.rjwSextype.Sixtynine)
			{
				absorb_factor += 1f;
			}
			else if (props.sexType == xxx.rjwSextype.Vaginal && GeneUtility.HasGeneNullCheck(succubus, GeneDefOf.rjw_genes_vaginal_absorber))
			{
				absorb_factor += 1f;
			}
			else if (props.sexType == xxx.rjwSextype.Anal && GeneUtility.HasGeneNullCheck(succubus, GeneDefOf.rjw_genes_anal_absorber))
			{
				absorb_factor += 1f;
			}
			else if (props.sexType == xxx.rjwSextype.DoublePenetration)
			{
				if (GeneUtility.HasGeneNullCheck(succubus, GeneDefOf.rjw_genes_vaginal_absorber))
				{
					absorb_factor += 0.5f;
				}
				if (GeneUtility.HasGeneNullCheck(succubus, GeneDefOf.rjw_genes_anal_absorber))
				{
					absorb_factor += 0.5f;
				}
			}
			else if (props.sexType == xxx.rjwSextype.Scissoring || props.sexType == xxx.rjwSextype.Cunnilingus)
			{
				//with vaginal cum absorbtion 
				//absorb_factor += 1f;
			}
			return absorb_factor;
		}
	}
}
