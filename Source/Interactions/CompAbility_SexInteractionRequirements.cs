using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace RJW_Genes
{
	public class CompAbility_SexInteractionRequirements : AbilityComp
	{
		public CompProperties_SexInteractionRequirements Props
		{
			get
			{
				return (CompProperties_SexInteractionRequirements)this.props;
			}
		}

		
	}
}
