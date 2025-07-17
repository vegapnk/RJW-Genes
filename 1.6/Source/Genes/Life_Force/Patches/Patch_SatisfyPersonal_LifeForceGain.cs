using HarmonyLib;
using rjw;
using RimWorld;
using Verse;

namespace RJW_Genes
{
	/// <summary>
	/// This Patch hooks after "SatisfyPersonal"(i.E. when the pawn finished fucking) and covers LifeForceGain. 
	/// If the pawn has LifeForce, all relevant Genes are checked and applied. 
	/// </summary>
	[HarmonyPatch(typeof(SexUtility), nameof(SexUtility.SatisfyPersonal))]
	public static class Patch_SatisfyPersonal_LifeForceGain
	{
		public const float LIFEFORCE_GAINED_FROM_DRAINER_GENE = 0.25f;

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
			// Exit if the pawn has ONLY an archotech penis, and no other penises. Issue #72
			if (props.pawn.health.hediffSet.hediffs.Any(x => x.def == rjw.Genital_Helper.archotech_penis) 
				&& !(Genital_Helper.has_multipenis(props.pawn)))
			{
				return;
			}

			//Summary//
			//We use the positions of the pawn (dom or sub) and based on that which interactions will transfer fertilin 
			//By checking isreceiver we know if the succubus is the dom or the sub and if the situation is reverse we also swap the function we use
			float absorb_factor = 0f;
			if (GeneUtility.HasLifeForce(props.partner))
			{
				Pawn PawnWithLifeForce = props.partner;

				if (!props.isRevese)
				{
					if (props.isReceiver)
					{
						// Scenario Dom Succubus, normal
						absorb_factor = BaseDom(props, PawnWithLifeForce);
					}
					else
					{
						// Scenario Sub Succubus, normal
						absorb_factor = BaseSub(props, PawnWithLifeForce);
					}
				}
				else
				{
					if (props.isReceiver)
					{
						// Scenario Dom Succubus, Reverse
						absorb_factor = BaseSub(props, PawnWithLifeForce);
					}
					else
					{
						// Scenario Sub Succubus, Reverse
						absorb_factor = BaseDom(props, PawnWithLifeForce);
					}
				}

				// If we remove this check fertilin is always lost, but the succubus doesn't always gain any
				if (absorb_factor != 0f)
				{
					TransferFertilin(props, absorb_factor);
				}

				// Handle Gene: Sexual_Drainer
				// to be drained, a pawn must not-be-drained-already and drainers cannot be drained either.
				if (GeneUtility.IsSexualDrainer(PawnWithLifeForce) 
					&& !props.pawn.health.hediffSet.HasHediff(HediffDefOf.rjw_genes_succubus_drained) 
					&& !GeneUtility.IsSexualDrainer(props.pawn))
				{
					if (GeneUtility.IsGenerousDonor(props.pawn) && RJW_Genes_Settings.rjw_genes_generous_donor_cheatmode)
					{
						// Cheatmode is on, do not drain but give life 
						GeneUtility.OffsetLifeForce(GeneUtility.GetLifeForceGene(PawnWithLifeForce), LIFEFORCE_GAINED_FROM_DRAINER_GENE); 
						if (RJW_Genes_Settings.rjw_genes_detailed_debug)
							ModLog.Message($"{props.pawn.Name} was not (sexually) drained by {PawnWithLifeForce.Name}, because Cheatmode for Generous Donors is on");
					} else
                    {
						if (RJW_Genes_Settings.rjw_genes_detailed_debug)
							ModLog.Message($"{props.pawn.Name} has been (sexually) drained by {PawnWithLifeForce.Name}");
						props.pawn.health.AddHediff(HediffDefOf.rjw_genes_succubus_drained);
						GeneUtility.OffsetLifeForce(GeneUtility.GetLifeForceGene(PawnWithLifeForce), LIFEFORCE_GAINED_FROM_DRAINER_GENE);
					}
				}
			}
		}

		public static void TransferFertilin(SexProps props, float absorb_percentage = 1f)
		{
			Pawn_GeneTracker genes = props.partner.genes;
			Gene_LifeForce gene = genes.GetFirstGeneOfType<Gene_LifeForce>();

			Hediff fertilin_lost = props.pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.rjw_genes_fertilin_lost);
			//Around quarter get ejected everytime pawn cums
			float multiplier = Rand.Range(0.10f, 0.40f);

			if (GeneUtility.IsGenerousDonor(props.pawn) && RJW_Genes_Settings.rjw_genes_generous_donor_cheatmode)
			{
				// Do nothing, Cheatmode is on
				multiplier = 1;
			} 
			else 
			{ 
				//Create a new ferilin_lost hediff or increase it
				if (fertilin_lost == null)
				{
					Hediff new_fertilin_lost = HediffMaker.MakeHediff(HediffDefOf.rjw_genes_fertilin_lost, props.pawn);
					props.pawn.health.AddHediff(new_fertilin_lost);
					new_fertilin_lost.Severity = multiplier;
				} else
				{
					multiplier *= 1 - fertilin_lost.Severity;
					fertilin_lost.Severity += multiplier;
				}
			}

			multiplier *= absorb_percentage;
			//Currently taking the sum of all penises, maybe I should just consider one at random
			float valuechange = TotalFertilinAmount(props, multiplier);

			if (props.partner.IsAnimal())
            {
				if (RJW_Genes_Settings.rjw_genes_detailed_debug)
					ModLog.Message($"Fertilin-Source of {props.pawn.Name} was an Animal, Fertilin-Gain is being adjusted by {RJW_Genes_Settings.rjw_genes_fertilin_from_animals_factor}%");
				valuechange *= RJW_Genes_Settings.rjw_genes_fertilin_from_animals_factor;
            }

			GeneUtility.OffsetLifeForce(GeneUtility.GetLifeForceGene(props.partner), valuechange);
		}

		public static float TotalFertilinAmount(SexProps props, float multiplier)
        {
			float total_fluid = FluidUtility.GetTotalFluidAmount(props.pawn) / 100;


            //TODO
            //1.6 QUIRK subsystem disabled.
            /*
			//More in the tank means more to give
			if (props.pawn.Has(Quirk.Messy))
			{
				total_fluid *= 2;
			}
			*/
            if (props.pawn.RaceProps.Animal)
            {
				total_fluid *= 0.1f; //Should make this settable in settings
            }

			return total_fluid;
		}

		/// <summary>
		/// Handles the Case that the Life-Force wielder initiated the Sex (They are "Dom"). 
		/// </summary>
		/// <param name="props">The summary of the sex act, used for checking conditions.</param>
		/// <param name="PawnWithLifeForce">The pawn that might gain LifeForce through this method.</param>
		/// <returns>A factor between 0 and 1 how much of output-fertilin will be used for input-lifeforce</returns>
		public static float BaseDom(SexProps props, Pawn PawnWithLifeForce)
		{
			float absorb_factor = 0f;
			if (props.sexType == xxx.rjwSextype.Sixtynine && GeneUtility.IsCumEater(PawnWithLifeForce))
			{
				absorb_factor += 1f;
			}
			return absorb_factor;
		}

		/// <summary>
		/// Handles the Case that the Life-Force wielder got initiated into sex (They are "Sub").
		/// </summary>
		/// <param name="props">The summary of the sex act, used for checking conditions.</param>
		/// <param name="PawnWithLifeForce">The pawn that might gain LifeForce through this method.</param>
		/// <returns>A factor between 0 and 1 how much of output-fertilin will be used for input-lifeforce</returns>
		public static float BaseSub(SexProps props, Pawn PawnWithLifeForce)
        {
			float absorb_factor = 0f;
			if ((props.sexType == xxx.rjwSextype.Oral || props.sexType == xxx.rjwSextype.Fellatio || props.sexType == xxx.rjwSextype.Sixtynine) 
				&& GeneUtility.IsCumEater(PawnWithLifeForce))
			{
				absorb_factor += 1f;
			}
			else if (props.sexType == xxx.rjwSextype.Vaginal && GeneUtility.HasGeneNullCheck(PawnWithLifeForce, GeneDefOf.rjw_genes_fertilin_absorber))
			{
				absorb_factor += 1f;
			}
			else if (props.sexType == xxx.rjwSextype.Anal && GeneUtility.HasGeneNullCheck(PawnWithLifeForce, GeneDefOf.rjw_genes_fertilin_absorber))
			{
				absorb_factor += 1f;
			}
			else if (props.sexType == xxx.rjwSextype.DoublePenetration && GeneUtility.HasGeneNullCheck(PawnWithLifeForce, GeneDefOf.rjw_genes_fertilin_absorber))
			{
				absorb_factor += 1f;
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
