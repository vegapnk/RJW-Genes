using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using Verse.AI;
using rjw;

namespace RJW_Genes
{
	public class CompAbilityEffect_Seduce : CompAbilityEffect_WithDest
	{
		private new CompProperties_Seduce Props
		{
			get
			{
				return (CompProperties_Seduce)this.props;
			}
		}
		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{
			base.Apply(target, dest);
			Pawn pawn = target.Thing as Pawn;
			Pawn pawn2 = this.parent.pawn;
			if (pawn != null && pawn2 != null && !pawn.Downed)
			{
				Job job = JobMaker.MakeJob(JobDefOf.rjw_genes_lifeforce_seduced, pawn2);
				job.mote = MoteMaker.MakeThoughtBubble(pawn, this.parent.def.iconPath, true);
				pawn.jobs.StopAll(false, true);
				pawn.jobs.StartJob(job, JobCondition.InterruptForced, null, false, true, null, null, false, false, null, false, true);
			}
		}

		public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
		{

			Pawn pawn = target.Pawn;
			if (pawn != null)
			{
				if (!xxx.can_be_fucked(pawn))
				{
					if (throwMessages)
					{
						Messages.Message(pawn.Name + " is unable to have sex", pawn, MessageTypeDefOf.RejectInput, false);
					}
					return false;
				}
				else if (pawn.IsAnimal() && !RJWSettings.bestiality_enabled)
				{
					if (throwMessages)
					{
						Messages.Message("bestiality is disabled", pawn, MessageTypeDefOf.RejectInput, false);
					}
					return false;
				} 
				else if (GeneUtility.HasSeduce(pawn))
                {
					if (throwMessages)
					{
						Messages.Message(pawn.Name + " cannot be seduced, as they also have the Seduce-Ability", pawn, MessageTypeDefOf.RejectInput, false);
					}
					return false;
				}
				else if (pawn.Downed)
                {
					if (throwMessages)
					{
						Messages.Message(pawn.Name + " is unable to move", pawn, MessageTypeDefOf.RejectInput, false);
					}
					return false;
				}

			}
			return base.Valid(target, throwMessages);
		}

		public override bool GizmoDisabled(out string reason)
		{
			reason = null;
			if (!RJWSettings.rape_enabled)
			{
				reason = "Rape is disabled";
				return true;
			}
			return false;
		} 
    }
}
