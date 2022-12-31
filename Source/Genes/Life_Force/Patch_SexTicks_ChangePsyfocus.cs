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

	//[HarmonyPatch(typeof(JobDriver_Sex), nameof(JobDriver_Sex.ChangePsyfocus))]
	public static class Patch_SexTicks_ChangePsyfocus
	{
		//Using ChangePsyfocus as it is something that fires every 60 ticks
		public static void Postfix(ref JobDriver_Sex __instance, ref Pawn pawn, ref Thing target)
		{
			if (__instance.Sexprops.sexType == xxx.rjwSextype.Cunnilingus)
			{
				if (target != null)
				{
					Pawn pawn2 = target as Pawn;
					if (pawn2 != null)
					{
						//We need who the pawn on top is and if reverse we need to make the sub the pawn on top
						if (__instance.Sexprops.isRevese)
						{
							
							DrinkCumflation(pawn2, pawn);
						}
						else
						{
							//
							DrinkCumflation(pawn, pawn2);
							return;
						}
					}
				}
				
			}
		}

		public static void DrinkCumflation(Pawn dom, Pawn sub)
        {
			Log.Message("Firese");
			Log.Message(dom.Name.ToString());
			Log.Message(sub.Name.ToString());
			if (GeneUtility.HasLifeForce(sub) && dom.health.hediffSet.HasHediff(HediffDef.Named("Cumflation")))
			{
				Hediff cumflation = dom.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("Cumflation"));
				Gene_LifeForce gene_LifeForce = sub.genes.GetFirstGeneOfType<Gene_LifeForce>();
				cumflation.Severity -= 0.1f;
				gene_LifeForce.Resource.Value += 0.05f;
			}
		}
		//Maybe i can store instance and hediff so I dont need to look them up every time
	}
}
