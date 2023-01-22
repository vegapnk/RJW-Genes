using System.Collections.Generic;
using Verse;
using RimWorld;
using rjw;

namespace RJW_Genes
{
	public class CompAbilityEffect_PussyHeal : CompAbilityEffect
	{
		private new CompProperties_AbilityPussyHeal Props
		{
			get
			{
				return (CompProperties_AbilityPussyHeal)this.props;
			}
		}
		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{
			base.Apply(target, dest);
			Pawn pawn = target.Pawn;
			if (pawn == null)
			{
				return;
			}
			bool any_wound_tended = AbilityUtility.Heal(pawn, this.Props.tendQualityRange);
			if (any_wound_tended)
			{
				MoteMaker.ThrowText(pawn.DrawPos, pawn.Map, "Sex tended wounds", 3.65f);
			}
		}

		//Not yet implemented, but the heal should also trigger after normal sex
		public void AfterSex(Pawn pawn, Pawn target)
		{
			List<Hediff> hediffs = target.health.hediffSet.hediffs;
			for (int i = 0; i < hediffs.Count; i++)
			{
				if ((hediffs[i] is Hediff_Injury || hediffs[i] is Hediff_MissingPart) && hediffs[i].TendableNow(false))
				{
					//target.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.Pussy_Healed, pawn, null);
					break;
				}
			}
			//InteractionHelper.GetWithExtension(dictionaryKey).DominantHasTag("CanBePenetrated")


		}

		public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
		{
			Pawn pawn = target.Pawn;
			if (pawn != null)
			{
				//to be replaced with severel checks to make it clear why target is unable to have sex
				if (!CasualSex_Helper.CanHaveSex(pawn))
				{
					if (throwMessages)
					{
						Messages.Message(pawn.Name + " is unable to have sex", pawn, MessageTypeDefOf.RejectInput, false);
					}
					return false;
				}
				else if (pawn.IsAnimal() && !RJWSettings.bestiality_enabled)
				{
					if (throwMessages)
					{
						Messages.Message("bestiality is disabled", pawn, MessageTypeDefOf.RejectInput, false);
					}
					return false;
				}
				//TODO: Only make pawns targetable that have tendable wounds 

			}
			return base.Valid(target, throwMessages);
		}

		public override bool GizmoDisabled(out string reason)
		{
			reason = null;
			if (!Genital_Helper.has_vagina(this.parent.pawn))
			{
				reason = this.parent.pawn.Name + " has no vagina to use.";
				return true;
			}
			else if (!RJWSettings.rape_enabled)
			{
				reason = "Rape is disabled";
				return true;
			}
			return false;
		}


	}
}
