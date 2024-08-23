using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
namespace RJW_Genes
{
    public class HediffComp_SeverityFromFertilin : HediffComp
	{
		public HediffCompProperties_SeverityFromFertilin Props
		{
			get
			{
				return (HediffCompProperties_SeverityFromFertilin)this.props;
			}
		}
		public override bool CompShouldRemove
		{
			get
			{
				Pawn_GeneTracker genes = base.Pawn.genes;
				return ((genes != null) ? genes.GetFirstGeneOfType<Gene_LifeForce>() : null) == null;
			}
		}
		private Gene_LifeForce LifeForce
		{
			get
			{
				if (this.cachedLifeForceGene == null)
				{
					this.cachedLifeForceGene = base.Pawn.genes.GetFirstGeneOfType<Gene_LifeForce>();
				}
				return this.cachedLifeForceGene;
			}
		}
		public override void CompPostTick(ref float severityAdjustment)
		{
			base.CompPostTick(ref severityAdjustment);
			severityAdjustment += ((this.LifeForce.Value > 0f) ? this.Props.severityPerHourHemogen : this.Props.severityPerHourEmpty) / 2500f;
			this.MentalBreak();
		}

		public void MentalBreak()
        {
			if (cachedLifeForceGene.Resource.Value <= cachedLifeForceGene.Resource.MinLevelForAlert && this.Pawn.IsHashIntervalTick(2500) && Rand.Chance(0.03f)) //~50% chance each day for mental break
			{
				if (this.Pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_cum_eater)
				|| this.Pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_fertilin_absorber) || this.Pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_drainer))
				{
					//TODO: use mentalstatedef instead of mentalbreakdef
					MentalBreakDef randomrape = GeneDefOf.rjw_genes_lifeforce_randomrape;
					if (ModsConfig.BiotechActive &&
						this.Pawn.Spawned && !this.Pawn.InMentalState && !this.Pawn.Downed &&
						randomrape.Worker.BreakCanOccur(this.Pawn))
					{
						randomrape.Worker.TryStart(this.Pawn, "MentalBreakNoFertilin".Translate(), false);
					}
				}
			}
		}

		private Gene_LifeForce cachedLifeForceGene;
	}
}
