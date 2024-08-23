using Verse;
using RimWorld;
using rjw;

namespace RJW_Genes
{
    /// <summary>
    /// The CocoonWeaver Ability applies the RJW-Cocoon to a pawn.
    /// Friendly Pawns can always be cocooned, neutral and hostile pawns must be downed. 
    /// </summary>
    public class CompAbilityEffect_CocoonWeaver : CompAbilityEffect
    {
        private new CompProperties_AbilityCocoonWeaver Props
        {
            get
            {
                return (CompProperties_AbilityCocoonWeaver)this.props;
            }
        }


        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);

            Pawn CocooningPawn = this.parent.pawn;
            Pawn PawnToCocoon = target.Pawn;

            // Error Case - Null Pawn
            if (PawnToCocoon == null)
            {
                return;
            }

            PawnToCocoon.health.AddHediff(HediffDef.Named("RJW_Cocoon"));

        }

        /// <summary>
        /// For validity, there are a few checks:
        /// 0. Target is not already cocooned 
        /// 1. Target is either Colonist / Prisoner 
        /// 2. If the Target is an enemy or neutral, it must be downed. 
        /// </summary>
        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            Pawn cocoonTarget = target.Pawn;
            if (cocoonTarget != null)
            {
                bool CocoonTargetIsColonistOrPrisoner = cocoonTarget.Faction == this.parent.pawn.Faction || cocoonTarget.IsPrisonerOfColony;
                bool CocoonTargetIsHostile = cocoonTarget.HostileTo(this.parent.pawn);
                bool CocoonTargetIsDowned = cocoonTarget.Downed;

                if (cocoonTarget.health.hediffSet.hediffs.Any(t => t.def.defName == "RJW_Cocoon"))
                {
                    if (throwMessages)
                        Messages.Message(cocoonTarget.Name + " is already cocooned.", cocoonTarget, MessageTypeDefOf.RejectInput, false);
                    return false;
                }

                if (!CocoonTargetIsColonistOrPrisoner && !(CocoonTargetIsHostile && CocoonTargetIsDowned))
                {
                    if (throwMessages)
                    {
                        if (CocoonTargetIsHostile && !CocoonTargetIsDowned)
                        {
                            Messages.Message(cocoonTarget.Name + " is hostile, but not downed.", cocoonTarget, MessageTypeDefOf.RejectInput, false);
                        }
                        else if (!CocoonTargetIsColonistOrPrisoner)
                        {
                            Messages.Message(cocoonTarget.Name + " is not a part of the colony or hostile.", cocoonTarget, MessageTypeDefOf.RejectInput, false);
                        }
                    }
                    return false;
                }
            }
            return base.Valid(target, throwMessages);
        }

    }
}