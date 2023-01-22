using System;
using Verse;
using Verse.AI;

namespace RJW_Genes
{
	public class ThinkNode_ConditionalLowLifeForce : ThinkNode_Conditional
	{
		protected override bool Satisfied(Pawn p)
		{
			return GeneUtility.HasLowLifeForce(p);
		}
	}
}