﻿using HarmonyLib;
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

	/// <summary>
	/// This patch enables cum-eater pawns to drain cumflations for more fertilin drain by passively having sex.
	/// It is hooked after RJWs Change-Psyfocus so that pawns that are having prolonged sex (e.g. by overdrive) can fully drain the cumflation over time.
	/// 
	/// It is conditionally loaded only when LicentiaLabs is enabled, as this is the necessary source for cumflation-hediffs. 
	/// The patched function is: [HarmonyPatch(typeof(JobDriver_Sex), nameof(JobDriver_Sex.ChangePsyfocus))]
	/// </summary>
	public static class Patch_SexTicks_ChangePsyfocus
	{
		public const float LIFEFORCE_GAIN_PER_TICK = 0.05f;
		public const float CUMFLATION_SEVERITY_LOSS_PER_TICK = 0.1f;

		//Using ChangePsyfocus as it is something that fires every 60 ticks
		public static void Postfix(ref JobDriver_Sex __instance, ref Pawn pawn, ref Thing target)
		{
			SexProps props = __instance.Sexprops;
			if (props != null && props.sexType == xxx.rjwSextype.Cunnilingus && props.partner != null && target != null)
            {
				Pawn pawn2 = target as Pawn;
				// Case 1: Pawn is "drinking" and has CumEater Gene
				if (props.isRevese && GeneUtility.IsCumEater(pawn))
                {
					if (RJW_Genes_Settings.rjw_genes_detailed_debug)
						ModLog.Message($"{pawn.Name} is draining {pawn2.Name}'s cumflation for additional fertilin (CumEater-Gene ChangePsyFocus-Trigger).");
					DrinkCumflation(pawn2, pawn);
				}
				// Case 2: Pawn2 is "drinking" and has CumEater Gene
				else if (GeneUtility.IsCumEater(pawn2))
				{
					if (RJW_Genes_Settings.rjw_genes_detailed_debug)
						ModLog.Message($"{pawn.Name} is draining {pawn2.Name}'s cumflation for additional fertilin (CumEater-Gene ChangePsyFocus-Trigger).");
					DrinkCumflation(pawn, pawn2);
				}
			}
		}

		public static void DrinkCumflation(Pawn source, Pawn consumer)
        {
			if (GeneUtility.HasLifeForce(consumer) && GeneUtility.IsCumEater(consumer)
				&& source.health.hediffSet.HasHediff(HediffDef.Named("Cumflation")))
			{
				Hediff cumflation = source.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("Cumflation"));
				Gene_LifeForce gene_LifeForce = consumer.genes.GetFirstGeneOfType<Gene_LifeForce>();
				cumflation.Severity = Math.Max(0f,cumflation.Severity - CUMFLATION_SEVERITY_LOSS_PER_TICK);
				gene_LifeForce.Resource.Value += LIFEFORCE_GAIN_PER_TICK;
			}
		}
	}
}
