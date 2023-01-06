using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using rjw;
namespace RJW_Genes
{
    public class HediffComp_RemoveSubmit : HediffComp
    {
		public HediffCompProperties_RemoveSubmit Props
		{
			get
			{
				return (HediffCompProperties_RemoveSubmit)this.props;
			}
		}

        public override void CompPostPostRemoved()
        {
            base.CompPostPostRemoved();
			HediffWithComps submitting = this.Pawn.health.hediffSet.GetFirstHediffOfDef(xxx.submitting) as HediffWithComps;
			submitting.CurStage.becomeVisible = false;
			if (submitting != null)
			{
				foreach (HediffComp comp in submitting.comps)
				{
					HediffComp_Disappears hediffComp = comp as HediffComp_Disappears;
					if (hediffComp != null)
					{
						hediffComp.ticksToDisappear = 1;
						//pawn.health.RemoveHediff(submitting);
						//removing the hediff directly gives an error, ArgementOutOrRange, making the remaining time 1 ticks should have the same effect without the error
					}
				}
			}

		}
	}
}
