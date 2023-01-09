using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;

namespace RJW_Genes
{
	// Token: 0x02000F65 RID: 3941
	public class CompProperties_AbilityLifeForceCost : CompProperties_AbilityEffect
	{
		// Token: 0x06005D16 RID: 23830 RVA: 0x001FA73F File Offset: 0x001F893F
		public CompProperties_AbilityLifeForceCost()
		{
			this.compClass = typeof(CompAbilityEffect_LifeForceCost);
		}

		// Token: 0x06005D17 RID: 23831 RVA: 0x001FA757 File Offset: 0x001F8957
		public override IEnumerable<string> ExtraStatSummary()
		{
			yield return "AbilityFertilinCost" + ": " + Mathf.RoundToInt(this.fertilinCost * 100f);
			yield break;
		}

		// Token: 0x040038CD RID: 14541
		public float fertilinCost;
	}
}
