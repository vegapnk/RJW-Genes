using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace RJW_Genes
{
    public class ThoughtWorker_Aphrodisiac_Pheromones_Social : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn pawn, Pawn other)
        {
            // Return for trivial errors
            if (pawn == null || other == null || pawn == other)
                return (ThoughtState)false;
            // Check for position-existance
            if (pawn.Position == null || other.Position == null || pawn.Map == null || other.Map == null)
                return (ThoughtState)false;
            // Do nothing if pawn is carried 
            if (pawn.CarriedBy != null)
                return (ThoughtState)false;
            // Do nothing if Pawn is Baby or Child (#25)
            if (!pawn.ageTracker.Adult)
                return (ThoughtState)false;
            // Only check if they are spawned humans
            if (!pawn.Spawned || !other.Spawned)
                return (ThoughtState)false;
            if (!pawn.RaceProps.Humanlike)
                return (ThoughtState)false;
            if (!other.RaceProps.Humanlike)
                return (ThoughtState)false;

            if (!RelationsUtility.PawnsKnowEachOther(pawn, other))
                return (ThoughtState)false;
            // If the pawn is not on Map (e.g. caravan), no mali 



            // Do nothing for pawns that also have pheromones
            if (GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_aphrodisiac_pheromones))

                return (ThoughtState)false;

            // Actual Logic: 
            // Pawn qualifies in right distance and needs line of sight.
            var pos = other.Position; 
            int effectDistance = ModExtensionHelper.GetDistanceFromModExtension(GeneDefOf.rjw_genes_aphrodisiac_pheromones, Gene_Aphrodisiac_Pheromones.APHRODISIAC_DISTANCE_FALLBACK);
            if (pos.DistanceTo(pawn.Position) < effectDistance && GenSight.LineOfSight(pos, pawn.Position, pawn.Map))
            {
               return (ThoughtState)true;
            }

            return (ThoughtState)false;
        }
    }
}
