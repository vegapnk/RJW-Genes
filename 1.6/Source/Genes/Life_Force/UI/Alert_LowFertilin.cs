using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld.Planet;
using Verse;
using RimWorld;
namespace RJW_Genes
{
    public class Alert_CriticalFertilin : Alert
    {
		private List<GlobalTargetInfo> Targets
		{
			get
			{
				this.CalculateTargets();
				return this.targets;
			}
		}

		public override string GetLabel()
		{
			if (this.Targets.Count == 1)
			{
				return "AlertLowFertilin".Translate() + ": " + this.targetLabels[0];
			}
			return "AlertLowFertilin".Translate();
		}

		private void CalculateTargets()
		{
			this.targets.Clear();
			this.targetLabels.Clear();
			if (!ModsConfig.BiotechActive)
			{
				return;
			}
            //1.6 Patch
            //foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive)
            foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravellingTransporters_Alive_OfPlayerFaction)
            {
				if (pawn.RaceProps.Humanlike)
				{
					Pawn_GeneTracker genes = pawn.genes;
					Gene_LifeForce gene_Lifeforce = (genes != null) ? genes.GetFirstGeneOfType<Gene_LifeForce>() : null;
					if (gene_Lifeforce != null && gene_Lifeforce.Active && gene_Lifeforce.Value < gene_Lifeforce.MinLevelForAlert)
					{
						this.targets.Add(pawn);
						this.targetLabels.Add(pawn.NameShortColored.Resolve());
					}
				}
			}
		}

		public override TaggedString GetExplanation()
		{
			return "AlertLowFertilinDesc".Translate() + ":\n" + this.targetLabels.ToLineList("  - ");
		}

		public override AlertReport GetReport()
		{
			return AlertReport.CulpritsAre(this.Targets);
		}

		private List<GlobalTargetInfo> targets = new List<GlobalTargetInfo>();

		private List<string> targetLabels = new List<string>();
	}
}
