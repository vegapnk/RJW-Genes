using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace RJW_Genes
{
	public class CompProperties_AbilitySpawnSpelopede : CompProperties_AbilityEffect
	{
		public PawnKindDef pawnKindDef;
		public float sensitivityMultiplier;
		public bool tamed;
		public CompProperties_AbilitySpawnSpelopede()
		{
			this.compClass = typeof(CompAbilityEffect_SpawnSpelopede);
		}
	}
}
