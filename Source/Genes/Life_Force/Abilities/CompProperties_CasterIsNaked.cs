using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace RJW_Genes
{
	public class CompProperties_CasterIsNaked : CompProperties_EffectWithDest
	{
		public CompProperties_CasterIsNaked()
		{
			this.compClass = typeof(CompAbilityEffect_CasterIsNaked);
		}
	}
}

