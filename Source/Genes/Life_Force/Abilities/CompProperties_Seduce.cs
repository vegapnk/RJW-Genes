using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace RJW_Genes
{
	public class CompProperties_Seduce : CompProperties_EffectWithDest
	{
		public CompProperties_Seduce()
		{
			this.compClass = typeof(CompAbilityEffect_Seduce);
		}
		
		public StatDef durationMultiplier;
	}
}

