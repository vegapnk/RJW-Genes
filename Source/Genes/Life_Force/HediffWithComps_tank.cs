using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
namespace RJW_Genes
{
    internal class HediffWithComps_tank : HediffWithComps
    {
		public override string LabelInBrackets
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(base.LabelInBrackets);
				stringBuilder.Append(this.Severity.ToStringPercent());
				return stringBuilder.ToString();
			}
		}
	}
}
