using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace RJW_Genes
{
    public class MapUtility
    {
        /// <summary>
        /// Checks if the pawn is on the players home map. 
        /// 
        /// Reason is that drones should only be punished for absence of queen if they are on the map and there is no queen.
        /// If they are on a mission, transport-pod etc. they should not get boni or mali. 
        /// </summary>
        /// <param name="pawn">The pawn for which to check map-presence.</param>
        /// <returns>True if the pawn is on the home-map, False otherwise.</returns>
        public static bool PawnIsOnHomeMap(Pawn pawn)
        {
            if (Find.Maps.NullOrEmpty() || !Find.Maps.Where(mapCandidate => mapCandidate.IsPlayerHome).Any())
            {
                return false;
            }
            Map homeMap = Find.Maps.Where(mapCandidate => mapCandidate.IsPlayerHome).First();
            return
                homeMap != null && pawn != null
                && pawn.Spawned
                && pawn.Map == homeMap;
        }

    }
}
