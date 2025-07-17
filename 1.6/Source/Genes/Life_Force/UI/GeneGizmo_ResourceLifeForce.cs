using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using UnityEngine;
namespace RJW_Genes
{
	//Copied from GeneGizmo_ResourceHemogen, with small modifications 
	public class GeneGizmo_ResourceLifeForce : GeneGizmo_Resource
    {
        private static bool draggingBar;

        public GeneGizmo_ResourceLifeForce(Gene_Resource gene, List<IGeneResourceDrain> drainGenes, Color barColor, Color barhighlightColor) : base(gene, drainGenes, barColor, barhighlightColor)
        {
            
        }

        public override GizmoResult GizmoOnGUI(Vector2 topLeft, float maxWidth, GizmoRenderParms parms)
        {
            return base.GizmoOnGUI(topLeft, maxWidth, parms);
        }

		//Added for 1.6 Support.
        protected override bool DraggingBar
        {
            get
            {
                return GeneGizmo_ResourceLifeForce.draggingBar;
            }
            set
            {
                GeneGizmo_ResourceLifeForce.draggingBar = value;
            }
        }

        protected override string GetTooltip()
        {

			this.tmpDrainGenes.Clear();
			string text = string.Format("{0}: {1} / {2}\n", this.gene.ResourceLabel.CapitalizeFirst().Colorize(ColoredText.TipSectionTitleColor), this.gene.ValueForDisplay, this.gene.MaxForDisplay);
			if (this.gene.pawn.IsColonistPlayerControlled || this.gene.pawn.IsPrisonerOfColony)
			{
				if (this.gene.targetValue <= 0f)
				{
					text += "NeverSeekFertilin";
				}
				else
				{
					text = text + ("SeekFertilinBelow" + ": ") + this.gene.PostProcessValue(this.gene.targetValue);
				}
			}
			if (!this.drainGenes.NullOrEmpty<IGeneResourceDrain>())
			{
				float num = 0f;
				foreach (IGeneResourceDrain geneResourceDrain in this.drainGenes)
				{
					if (geneResourceDrain.CanOffset)
					{
						this.tmpDrainGenes.Add(new Pair<IGeneResourceDrain, float>(geneResourceDrain, geneResourceDrain.ResourceLossPerDay));
						num += geneResourceDrain.ResourceLossPerDay;
					}
				}
				if (num != 0f)
				{
					string text2 = (num < 0f) ? "RegenerationRate".Translate() : "DrainRate".Translate();
					text = string.Concat(new string[]
					{
				text,
				"\n\n",
				text2,
				": ",
				"PerDay".Translate(Mathf.Abs(this.gene.PostProcessValue(num))).Resolve()
					});
					foreach (Pair<IGeneResourceDrain, float> pair in this.tmpDrainGenes)
					{
						text = string.Concat(new string[]
						{
					text,
					"\n  - ",
					pair.First.DisplayLabel.CapitalizeFirst(),
					": ",
					"PerDay".Translate(this.gene.PostProcessValue(-pair.Second).ToStringWithSign()).Resolve()
						});
					}
				}
			}
			if (!this.gene.def.resourceDescription.NullOrEmpty())
			{
				text = text + "\n\n" + this.gene.def.resourceDescription.Formatted(this.gene.pawn.Named("PAWN")).Resolve();
			}
			return text;
		}
		private List<Pair<IGeneResourceDrain, float>> tmpDrainGenes = new List<Pair<IGeneResourceDrain, float>>();
	}
}
