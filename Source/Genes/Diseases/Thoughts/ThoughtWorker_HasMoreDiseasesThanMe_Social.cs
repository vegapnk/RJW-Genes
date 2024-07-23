using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    public class ThoughtWorker_HasMoreDiseasesThanMe_Social : ThoughtWorker
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

            int pawn_diseases = DiseaseHelper.GetGeneticDiseaseGenes(pawn).Count();
            int other_diseases = DiseaseHelper.GetGeneticDiseaseGenes(other).Count();
            int disease_diff = other_diseases - pawn_diseases;

            if (disease_diff >= 5)
                return ThoughtState.ActiveAtStage(2);
            else if (disease_diff >= 2)
                return ThoughtState.ActiveAtStage(1);
            else if (disease_diff >= 1)
                return ThoughtState.ActiveAtStage(0);
            else
                return (ThoughtState)false;
        }
    }
}
