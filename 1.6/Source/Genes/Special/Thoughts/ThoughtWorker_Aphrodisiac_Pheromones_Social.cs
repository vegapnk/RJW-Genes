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
            //1.6 fix
            //if (pawn.Position == null || other.Position == null || pawn.Map == null || other.Map == null)
            if (pawn.Map == null || other.Map == null || !pawn.Spawned || !other.Spawned)
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

            // Pawns that have not "met" wont give each other Mali
            // Known-Each-Other is a key-word for Rimworld that shows they have had any interaction and stored each other in relations. 
            if (!RelationsUtility.PawnsKnowEachOther(pawn, other))
                return (ThoughtState)false;
            // If the pawn is not on Map (e.g. caravan), no mali 
            if (!MapUtility.PawnIsOnHomeMap(pawn))
                return (ThoughtState)false;

            // Do nothing if the pawn does not have the pheromones
            if (!GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_aphrodisiac_pheromones))
                return (ThoughtState)false;

            // Do nothing for others that also have pheromones
            if (GeneUtility.HasGeneNullCheck(other, GeneDefOf.rjw_genes_aphrodisiac_pheromones))
                return (ThoughtState)false;

            // Do nothing for pawns that wear Gas-Masks
            if (other.apparel != null && other.apparel.AnyApparel)
                if (other.apparel.WornApparel.Any(apparel => apparel.def == RimWorld.ThingDefOf.Apparel_GasMask))
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
