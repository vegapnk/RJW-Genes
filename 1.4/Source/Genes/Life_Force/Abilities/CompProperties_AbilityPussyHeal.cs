using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace RJW_Genes
{
	public class CompProperties_AbilityPussyHeal : CompProperties_AbilityEffect
	{
		public CompProperties_AbilityPussyHeal()
		{
			this.compClass = typeof(CompAbilityEffect_PussyHeal);
		}

		public FloatRange tendQualityRange;
	}
}
