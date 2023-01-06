using System;
using System.Collections.Generic;
using rjw;
using rjw.Modules.Interactions.Contexts;
using rjw.Modules.Interactions.Enums;
using rjw.Modules.Interactions.Rules.PartKindUsageRules;
using rjw.Modules.Shared;
using Verse;

namespace RJW_Genes.Interactions
{
	//Summary//
	//Set custom preferences for pawn. Gets integrated to rjw by AddtoIPartPreferenceRule in First
	//Depending on the level of lifeforce increase the chance for using the mouth.
	public class GenesPartKindUsageRule : IPartPreferenceRule
    {
		public IEnumerable<Weighted<LewdablePartKind>> ModifiersForDominant(InteractionContext context)
		{
			Pawn pawn = context.Internals.Dominant.Pawn;
			if (GeneUtility.HasCriticalLifeForce(pawn))
			{
				yield return new Weighted<LewdablePartKind>(50f, LewdablePartKind.Mouth);
			}
			else if (GeneUtility.HasLowLifeForce(pawn))
			{
				yield return new Weighted<LewdablePartKind>(10f, LewdablePartKind.Mouth);
			}
			else if (GeneUtility.HasLifeForce(pawn))
			{
				yield return new Weighted<LewdablePartKind>(2f, LewdablePartKind.Mouth);
			}
			yield break;
		}

		public IEnumerable<Weighted<LewdablePartKind>> ModifiersForSubmissive(InteractionContext context)
		{
			Pawn pawn = context.Internals.Submissive.Pawn;
			if (GeneUtility.HasCriticalLifeForce(pawn))
			{
				yield return new Weighted<LewdablePartKind>(50f, LewdablePartKind.Mouth);
			}
			else if (GeneUtility.HasLowLifeForce(pawn))
			{
				yield return new Weighted<LewdablePartKind>(10f, LewdablePartKind.Mouth);
			}
			else if (GeneUtility.HasLifeForce(pawn))
			{
				yield return new Weighted<LewdablePartKind>(2f, LewdablePartKind.Mouth);
			}
			yield break;
		}
	}
}
