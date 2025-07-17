using RimWorld;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace RJW_Genes
{
    public class ThoughtWorker_SizeBlinded_Social : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn pawn, Pawn other)
        {
            // Return for trivial errors
            if (pawn == null || other == null || pawn == other)
                return (ThoughtState)false;
            // Check for position-existance
            //1.6 Fix
            //if (pawn.Position == null || other.Position == null || pawn.Map == null || other.Map == null)
            if (pawn.Map == null || other.Map == null || !pawn.Spawned || !other.Spawned)
                return (ThoughtState)false;
            // Do nothing if pawn is carried 
            if (pawn.CarriedBy != null)
                return (ThoughtState)false;
            // Do nothing if Pawn is Baby or Child (#25)
            if (!pawn.ageTracker.Adult)
                return (ThoughtState)false;
            if (!other.ageTracker.Adult)
                return (ThoughtState)false;
            // Only check if they are spawned humans
            if (!pawn.Spawned || !other.Spawned)
                return (ThoughtState)false;
            if (!pawn.RaceProps.Humanlike)
                return (ThoughtState)false;
            if (!other.RaceProps.Humanlike)
                return (ThoughtState)false;

            // Pawns that have not "met" wont give each other Mali
            // Known-Each-Other is a key-word for Rimworld that shows they have had any interaction and stored each other in relations. 
            if (!RelationsUtility.PawnsKnowEachOther(pawn, other))
                return (ThoughtState)false;
            // If the pawn is not on Map (e.g. caravan), no mali 
            if (!MapUtility.PawnIsOnHomeMap(pawn))
                return (ThoughtState)false;


            // Do nothing if there is no size-blinded involved 
            if (!GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_size_blinded))
                return (ThoughtState)false;
            else
                ModLog.Debug($"{pawn} has the size blinded gene");

            // Iff the pawn has a penis, retrieve it's size. 
            var penis = GenitaliaUtility.GetBiggestPenis(other);
            // Do Nothing if the other pawn has no penis 
            if (penis == null) return (ThoughtState)false;
            var bodysize = GenitaliaUtility.GetBodySizeOfSexPart(penis);

            if (penis.Severity + (bodysize) - 1.0 > 1.0)
                return ThoughtState.ActiveAtStage(2);
            else if (penis.Severity >= 0.8f)
                return ThoughtState.ActiveAtStage(1);
            else
                return ThoughtState.ActiveAtStage(0);

        }
    }
}
