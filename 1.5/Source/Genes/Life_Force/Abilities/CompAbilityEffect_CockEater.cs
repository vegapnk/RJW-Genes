using Verse;
using RimWorld;
using rjw;

namespace RJW_Genes
{
	/// <summary>
	/// The CockEater Ability bites off the first found non-artifical cock of an target pawn. 
	/// It will restore {MINIMUM_LIFEFORCE_GAIN} multiplied by up to 2-times the Cock-Size. 
	/// Consuming a "towering" cock will give 2*{MINIMUM_LIFEFORCE_GAIN}, resulting in default 0.5f LifeForce. 
	/// This number is reduced for consuming animals by Settings.
	/// 
	/// Balancing note: With the Cock-Eaters a drain of 0.08 is normal per day. This means 1 average cock should hold for 3-4 days of fertilin-fuel and half a day for an animal.
	/// </summary>
	public class CompAbilityEffect_CockEater : CompAbilityEffect
	{
		private new CompProperties_AbilityCockEater Props
		{
			get
			{
				return (CompProperties_AbilityCockEater)this.props;
			}
		}

		public const float MINIMUM_LIFEFORCE_GAIN = 0.25f;

		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{
			base.Apply(target, dest);
			Pawn CockBiter = this.parent.pawn;
			Pawn CockBittenPawn = target.Pawn;
			if (CockBittenPawn == null)
			{
				return;
			}
			var partBPR = Genital_Helper.get_genitalsBPR(CockBittenPawn);
			var parts = Genital_Helper.get_PartsHediffList(CockBittenPawn, partBPR);
			if (!parts.NullOrEmpty())
			{
				foreach (Hediff part in parts)
				{
					if (GenitaliaChanger.IsArtificial(part))
						continue;

					if (Genital_Helper.is_penis(part))
					{
						float gained_lifeforce = MINIMUM_LIFEFORCE_GAIN * (1 + part.Severity);
						if (CockBittenPawn.IsAnimal())
                        {
							gained_lifeforce *= RJW_Genes_Settings.rjw_genes_fertilin_from_animals_factor;
                        }
						// Increase LifeForce for Biter
						GeneUtility.OffsetLifeForce(GeneUtility.GetLifeForceGene(CockBiter), gained_lifeforce);
						// Handle Damage for Bitten 
						CockBittenPawn.TakeDamage(new DamageInfo(DamageDefOf.Bite, 99999f, 999f, hitPart: Genital_Helper.get_genitalsBPR(CockBittenPawn)));
						//CockBittenPawn.health.RemoveHediff(part);
						CockBittenPawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.rjw_genes_cock_eaten, CockBittenPawn, null);

						//Only one penis at the time
						break; 
					}
				}
			}
		}

		/// <summary>
		/// For validity, there are a few checks:
		/// 1. Target has Penis 
		/// 2. Target is either Colonist / Prisoner 
		/// 3. If the Target is an enemy, it must be downed. 
		/// </summary>
		public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
		{
			Pawn CockBiteTarget = target.Pawn;
			if (CockBiteTarget != null)
			{
				bool CockBiteTargetIsColonistOrPrisoner = CockBiteTarget.Faction == this.parent.pawn.Faction || CockBiteTarget.IsPrisonerOfColony;
				bool CockBiteTargetIsHostile = CockBiteTarget.HostileTo(this.parent.pawn);
				bool CockBiteTargetIsDowned = CockBiteTarget.Downed;

				if (!CockBiteTargetIsColonistOrPrisoner && !(CockBiteTargetIsHostile && CockBiteTargetIsDowned))
				{
					if (throwMessages)
					{
						if(CockBiteTargetIsHostile && !CockBiteTargetIsDowned)
                        {
							Messages.Message(CockBiteTarget.Name + " is hostile, but not downed.", CockBiteTarget, MessageTypeDefOf.RejectInput, false);
						}
						else if (!CockBiteTargetIsColonistOrPrisoner)
						{
							Messages.Message(CockBiteTarget.Name + " is not a part of the colony or hostile.", CockBiteTarget, MessageTypeDefOf.RejectInput, false);
						}
					}
					return false;
				}
				if (!Genital_Helper.has_penis_fertile(CockBiteTarget))
				{
					if (throwMessages)
					{
						Messages.Message(CockBiteTarget.Name + " has no penis", CockBiteTarget, MessageTypeDefOf.RejectInput, false);
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
