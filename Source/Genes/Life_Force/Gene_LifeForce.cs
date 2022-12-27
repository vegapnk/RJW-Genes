using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using RimWorld;


namespace RJW_Genes
{
    public class Gene_LifeForce : Gene_Resource, IGeneResourceDrain
	{
		public override void ExposeData()
		{
			base.ExposeData();
		}

		public bool ShouldConsumeLifeForceNow()
		{
			return this.Value < this.targetValue;
		}

		//Same as Gene_Hemogen
		public override IEnumerable<Gizmo> GetGizmos()
		{
			foreach (Gizmo gizmo in base.GetGizmos())
			{
				yield return gizmo;
			}
			IEnumerator<Gizmo> enumerator = null;
			foreach (Gizmo gizmo2 in GeneResourceDrainUtility.GetResourceDrainGizmos(this))
			{
				yield return gizmo2;
			}
			enumerator = null;
			yield break;
			yield break;
		}

		//Depending on how low the value is it will increase sexdrive and if it reaches zero it will create a random rape mental break.
		//Not using base.Tick() as it is used to start mental breaks, but we have another way to do it.
		public override void Tick()
		{
			//base.Tick();
			if (this.CanOffset && this.Resource != null)
            {
				this.Resource.Value -= this.ResourceLossPerDay / 60000;
				if (this.Resource.Value <= 0 && this.pawn.IsHashIntervalTick(300))
				{
					if (ModsConfig.BiotechActive && this.def.mentalBreakDef != null && 
						this.pawn.Spawned && !this.pawn.InMentalState && !this.pawn.Downed && 
						this.def.mentalBreakDef.Worker.BreakCanOccur(this.pawn))
                    {
						this.def.mentalBreakDef.Worker.TryStart(this.pawn, "MentalStateReason_Gene".Translate() + ": " + this.LabelCap, false);
					}
				}
            }
			//GeneResourceDrainUtility.TickResourceDrain(this);
		}

		public Gene_Resource Resource
		{
			get
			{
				return this;
			}
		}
		public Pawn Pawn
		{
			get
			{
				return this.pawn;
			}
		}
		public bool CanOffset
		{
			get
			{
				return this.pawn.Spawned && this.Active;
			}
		}

		public float ResourceLossPerDay
		{
			get
			{
				return this.def.resourceLossPerDay;
			}
		}

		public string DisplayLabel
		{
			get
			{
				return this.def.resourceLabel;
			}
		}

		public override float InitialResourceMax
		{
			get
			{
				return 1f;
			}
		}

		public override float MinLevelForAlert
		{
			get
			{
				return 0.15f;
			}
		}
		public override float MaxLevelOffset
		{
			get
			{
				return base.MaxLevelOffset;
			}
		}
		protected override Color BarColor
		{
			get
			{
				return Color.grey;
			}
		}
		protected override Color BarHighlightColor
		{
			get
			{
				return Color.white;
			}
		}
	}
}
