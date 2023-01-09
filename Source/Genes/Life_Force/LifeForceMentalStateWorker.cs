using System;
using Verse;
using Verse.AI;
using rjw;
namespace RJW_Genes
{
	public class LifeForceMentalStateWorker : MentalStateWorker
	{
		public override bool StateCanOccur(Pawn pawn)
		{
			return base.StateCanOccur(pawn) && (xxx.is_human(pawn) && JobGiver_LifeForce_RandomRape.can_rape(pawn));
		}
	}
}
