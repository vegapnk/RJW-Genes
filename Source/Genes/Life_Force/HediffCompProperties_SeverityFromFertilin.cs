using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
namespace RJW_Genes
{
    public class HediffCompProperties_SeverityFromFertilin : HediffCompProperties
	{
		public HediffCompProperties_SeverityFromFertilin()
		{
			this.compClass = typeof(HediffComp_SeverityFromFertilin);
		}

		// Token: 0x04001162 RID: 4450
		public float severityPerHourEmpty;

		// Token: 0x04001163 RID: 4451
		public float severityPerHourHemogen;
	}
}
