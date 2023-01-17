using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace RJW_Genes
{
	public class Gene_LifeForceDrain : Gene, IGeneResourceDrain
	{
		public Gene_Resource Resource
		{
			get
			{
				if (this.cachedLifeForceGene == null || !this.cachedLifeForceGene.Active)
				{
					this.cachedLifeForceGene = this.pawn.genes.GetFirstGeneOfType<Gene_LifeForce>();
				}
				return this.cachedLifeForceGene;
			}
		}

		public bool CanOffset
		{
			get
			{
				return this.Active && this.Resource != null && this.Resource.Active;
			}
		}

		public float ResourceLossPerDay
		{
			get
			{
				return this.def.resourceLossPerDay;
			}
		}

		public Pawn Pawn
		{
			get
			{
				return this.pawn;
			}
		}

		public string DisplayLabel
		{
			get
			{
				return this.Label + " (" + "Gene".Translate() + ")";
			}
		}

		public override void Tick()
		{
			base.Tick();
			if (this.CanOffset && this.Resource != null)
            {
				GeneUtility.OffsetLifeForce(this, -this.ResourceLossPerDay / 60000);
			}			
		}

		[Unsaved(false)]
		private Gene_LifeForce cachedLifeForceGene;

		private const float MinAgeForDrain = 3f;
	}
}
