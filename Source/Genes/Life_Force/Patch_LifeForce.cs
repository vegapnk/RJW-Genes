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
	
	[HarmonyPatch(typeof(SexUtility), nameof(SexUtility.SatisfyPersonal))]
	public static class Patch_LifeForce
	{
		
		public static void Postfix(SexProps props)
		{
			// ShortCuts: Exit Early if Pawn or Partner are null (can happen with Animals or Masturbation)
			if (props.pawn == null || !props.hasPartner())
				return;

			if (GeneUtility.HasLifeForce(props.pawn))
			{
				if (props.sexType == xxx.rjwSextype.Oral || props.sexType == xxx.rjwSextype.Fellatio || props.sexType == xxx.rjwSextype.Sixtynine)
                {
					Pawn_GeneTracker genes = props.pawn.genes;
					Gene_LifeForce gene = genes.GetFirstGeneOfType<Gene_LifeForce>();
					gene.Resource.Value += CumUtility.GetTotalFluidAmount(props.partner); //total amount may need to be modified to be balanced
				}
            }
		}
	}

}
