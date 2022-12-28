using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;
using RimWorld;
using rjw;
using rjw.Modules.Interactions.Helpers;

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
				MoteMaker.ThrowText(pawn.DrawPos, pawn.Map, "Sex healed wounds", 3.65f);
				//pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.Pussy_Healed, pawn, null);
			}
			//this.AfterSex(any_wound_tended);
			//FleckMaker.AttachedOverlay(pawn, FleckDefOf.FlashHollow, Vector3.zero, 1.5f, -1f);
		}

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
				//AbilityUtility.ValidateHasTendableWound(pawn, throwMessages, this.parent);

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
