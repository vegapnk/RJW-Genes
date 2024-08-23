using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace RJW_Genes
{
	public class CompProperties_AbilityCockEater : CompProperties_AbilityEffect
	{
		public CompProperties_AbilityCockEater()
		{
			this.compClass = typeof(CompAbilityEffect_CockEater);
		}
	}
}
