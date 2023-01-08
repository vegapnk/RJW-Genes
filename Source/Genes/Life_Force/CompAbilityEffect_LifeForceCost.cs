using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Verse;
using Verse.AI;
using RimWorld;
namespace RJW_Genes
{
	// Token: 0x02000F66 RID: 3942
	public class CompAbilityEffect_LifeForceCost : CompAbilityEffect
	{
		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06005D18 RID: 23832 RVA: 0x001FA767 File Offset: 0x001F8967
		public new CompProperties_AbilityLifeForceCost Props
		{
			get
			{
				return (CompProperties_AbilityLifeForceCost)this.props;
			}
		}

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06005D19 RID: 23833 RVA: 0x001FA774 File Offset: 0x001F8974
		private bool HasEnoughFertilin
		{
			get
			{
				Pawn_GeneTracker genes = this.parent.pawn.genes;
				Gene_LifeForce gene_lifeforce = (genes != null) ? genes.GetFirstGeneOfType < Gene_LifeForce>() : null;
				return gene_lifeforce != null && gene_lifeforce.Value >= this.Props.fertilinCost;
			}
		}

		// Token: 0x06005D1A RID: 23834 RVA: 0x001FA7B7 File Offset: 0x001F89B7
		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{
			base.Apply(target, dest);
			GeneUtility.OffsetLifeForce(GeneUtility.GetLifeForceGene(this.parent.pawn), -this.Props.fertilinCost, true);
		}

		// Token: 0x06005D1B RID: 23835 RVA: 0x001FA7E0 File Offset: 0x001F89E0
		public override bool GizmoDisabled(out string reason)
		{
			Pawn_GeneTracker genes = this.parent.pawn.genes;
			Gene_LifeForce gene_LifeForce = (genes != null) ? genes.GetFirstGeneOfType<Gene_LifeForce>() : null;
			if (gene_LifeForce == null)
			{
				reason = "AbilityDisabledNoFertilinGene".Translate(this.parent.pawn);
				return true;
			}
			if (gene_LifeForce.Value < this.Props.fertilinCost)
			{
				reason = "AbilityDisabledNoFertilin".Translate(this.parent.pawn);
				return true;
			}
			float num = this.TotalLifeForceCostOfQueuedAbilities();
			float num2 = this.Props.fertilinCost + num;
			if (this.Props.fertilinCost > 1E-45f && num2 > gene_LifeForce.Value)
			{
				reason = "AbilityDisabledNoFertilin".Translate(this.parent.pawn);
				return true;
			}
			reason = null;
			return false;
		}

		public override bool AICanTargetNow(LocalTargetInfo target)
		{
			return this.HasEnoughFertilin;
		}

		private float TotalLifeForceCostOfQueuedAbilities()
		{
			Pawn_JobTracker jobs = this.parent.pawn.jobs;
			object obj;
			if (jobs == null)
			{
				obj = null;
			}
			else
			{
				Job curJob = jobs.curJob;
				obj = ((curJob != null) ? curJob.verbToUse : null);
			}
			Verb_CastAbility verb_CastAbility = obj as Verb_CastAbility;
			float num;
			if (verb_CastAbility == null)
			{
				num = 0f;
			}
			else
			{
				Ability ability = verb_CastAbility.ability;
				num = ((ability != null) ? AbilityUtility.LifeForceCost(ability) : 0f);
			}
			float num2 = num;
			if (this.parent.pawn.jobs != null)
			{
				for (int i = 0; i < this.parent.pawn.jobs.jobQueue.Count; i++)
				{
					Verb_CastAbility verb_CastAbility2;
					if ((verb_CastAbility2 = (this.parent.pawn.jobs.jobQueue[i].job.verbToUse as Verb_CastAbility)) != null)
					{
						float num3 = num2;
						Ability ability2 = verb_CastAbility2.ability;
						num2 = num3 + ((ability2 != null) ? AbilityUtility.LifeForceCost(ability2) : 0f);
					}
				}
			}
			return num2;
		}
		
		//Modified version of HemogenCost in Ability
		
	}
}
