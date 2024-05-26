using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using Verse.AI;
using rjw;

namespace RJW_Genes
{
	//Summary//
	//Returns invalid if a pawn is not naked
	//Summary//
	public class CompAbilityEffect_CasterIsNaked : CompAbilityEffect_WithDest
	{
		private new CompProperties_CasterIsNaked Props
		{
			get
			{
				return (CompProperties_CasterIsNaked)this.props;
			}
		}

		public override bool GizmoDisabled(out string reason)
		{
			Pawn pawn = this.CasterPawn;
			if (pawn != null)
			{
				//Copied from ThoughtWorker_NudistNude.CurrentStateInternal
				List<Apparel> wornApparel = pawn.apparel.WornApparel;
				for (int i = 0; i < wornApparel.Count; i++)
				{
					Apparel apparel = wornApparel[i];
					if (apparel.def.apparel.countsAsClothingForNudity)
					{
						for (int j = 0; j < apparel.def.apparel.bodyPartGroups.Count; j++)
						{
							if (apparel.def.apparel.bodyPartGroups[j] == BodyPartGroupDefOf.Torso)
							{
								reason = pawn.Name + " is not naked";
								return true;
							}
							if (apparel.def.apparel.bodyPartGroups[j] == BodyPartGroupDefOf.Legs)
							{
								reason = pawn.Name + " is not naked";
								return true;

							}
						}
					}
				}
			}
			reason = null;
			return false;
		}
	}
}
