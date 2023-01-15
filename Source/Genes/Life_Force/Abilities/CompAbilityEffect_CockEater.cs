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
	public class CompAbilityEffect_CockEater : CompAbilityEffect
	{
		private new CompProperties_AbilityCockEater Props
		{
			get
			{
				return (CompProperties_AbilityCockEater)this.props;
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
			var partBPR = Genital_Helper.get_genitalsBPR(pawn);
			var parts = Genital_Helper.get_PartsHediffList(pawn, partBPR);
			if (!parts.NullOrEmpty())
			{
				foreach (Hediff part in parts)
				{
					if (GenitaliaChanger.IsArtificial(part))
						continue;

					if (Genital_Helper.is_penis(part))
					{
						GeneUtility.OffsetLifeForce(GeneUtility.GetLifeForceGene(this.parent.pawn), part.Severity); ;
						pawn.health.RemoveHediff(part);
						pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.rjw_genes_cock_eaten, pawn, null);
						break; //Only one penis at the time
					}
				}

			}
		}

		public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
		{
			Pawn pawn = target.Pawn;
			if (pawn != null)
			{
				bool flag = pawn.Faction == this.parent.pawn.Faction || pawn.IsPrisonerOfColony;
				bool flag2 = pawn.HostileTo(this.parent.pawn);
				bool flag3 = pawn.Downed;
				if (!flag && !(flag2 && flag3))
				{
					if (throwMessages)
					{
						if(flag2 && !flag3)
                        {
							Messages.Message(pawn.Name + " is hostile, but not downed.", pawn, MessageTypeDefOf.RejectInput, false);
						}
						else if (!flag)
						{
							Messages.Message(pawn.Name + " is not a part of the colony or hostile.", pawn, MessageTypeDefOf.RejectInput, false);
						}
					}
					return false;
				}
				if (!Genital_Helper.has_penis_fertile(pawn))
				{
					if (throwMessages)
					{
						Messages.Message(pawn.Name + " has no penis", pawn, MessageTypeDefOf.RejectInput, false);
					}
					return false;
				}
			}
			return base.Valid(target, throwMessages);
		}
		public override bool GizmoDisabled(out string reason)
		{
			Pawn_GeneTracker genes = this.parent.pawn.genes;
			Gene_LifeForce gene_LifeForce = (genes != null) ? genes.GetFirstGeneOfType<Gene_LifeForce>() : null;
			if (gene_LifeForce == null)
			{
				reason = "AbilityDisabledNoFertilinGene".Translate(this.parent.pawn);
				return true;
			}
			reason = null;
			return false;
		}
	}
}
