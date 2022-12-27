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
    public class GenesPartKindUsageRule : IPartPreferenceRule
    {
		public IEnumerable<Weighted<LewdablePartKind>> ModifiersForDominant(InteractionContext context)
		{
			Pawn pawn = context.Internals.Dominant.Pawn;
			if (GeneUtility.HasCriticalLifeForce(pawn))
			{
				Log.Message("Critical");
				yield return new Weighted<LewdablePartKind>(50f, LewdablePartKind.Mouth);
			}
			else if (GeneUtility.HasLowLifeForce(pawn))
			{
				Log.Message("Low");
				yield return new Weighted<LewdablePartKind>(10f, LewdablePartKind.Mouth);
			}
			else if (GeneUtility.HasLifeForce(pawn))
			{
				Log.Message("normal");
				yield return new Weighted<LewdablePartKind>(2f, LewdablePartKind.Mouth);
			}
			yield break;
		}

		public IEnumerable<Weighted<LewdablePartKind>> ModifiersForSubmissive(InteractionContext context)
		{
			Pawn pawn = context.Internals.Submissive.Pawn;
			if (GeneUtility.HasCriticalLifeForce(pawn))
			{
				Log.Message("Critical");
				yield return new Weighted<LewdablePartKind>(50f, LewdablePartKind.Mouth);
			}
			else if (GeneUtility.HasLowLifeForce(pawn))
			{
				Log.Message("Low");
				yield return new Weighted<LewdablePartKind>(10f, LewdablePartKind.Mouth);
			}
			else if (GeneUtility.HasLifeForce(pawn))
			{
				Log.Message("normal");
				yield return new Weighted<LewdablePartKind>(2f, LewdablePartKind.Mouth);
			}
			yield break;
		}
	}
}
