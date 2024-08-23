using Verse;
using Verse.AI;
using RimWorld;
namespace RJW_Genes
{
	public class CompAbilityEffect_LifeForceCost : CompAbilityEffect
	{

		public new CompProperties_AbilityLifeForceCost Props
		{
			get
			{
				return (CompProperties_AbilityLifeForceCost)this.props;
			}
		}

		private bool HasEnoughFertilin
		{
			get
			{
				Pawn_GeneTracker genes = this.parent.pawn.genes;
				Gene_LifeForce gene_lifeforce = (genes != null) ? genes.GetFirstGeneOfType < Gene_LifeForce>() : null;
				return gene_lifeforce != null && gene_lifeforce.Value >= this.Props.fertilinCost;
			}
		}

		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{
			base.Apply(target, dest);
			GeneUtility.OffsetLifeForce(GeneUtility.GetLifeForceGene(this.parent.pawn), -this.Props.fertilinCost);
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

		
	}
}
