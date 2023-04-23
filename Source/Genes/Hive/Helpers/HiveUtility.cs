using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    internal class HiveUtility
    {

        /// <summary>
        /// Checks for existance of the RJW-Gene `queen`, if the pawn is spawned and if the pawn has reached adulthood.
        /// Despite the naming, a Queen can also be male.
        /// </summary>
        /// <param name="pawn">The pawn that could be an Adult Queen</param>
        /// <returns>Whether the pawn is an adult queen.</returns>
        public static bool IsAdultQueen(Pawn pawn)
        {

            if (pawn == null || !pawn.Spawned)
                return false;

            if (GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_queen)) 
            { 
                return pawn.ageTracker.Adult;
            }

            return false;
        }

        public static int QueensOnMap() => GetQueensOnMap().Count;

        /// <summary>
        /// Checks for all pawns on the Players Home Map if they are an adult queen.
        /// Adultness is determined by Base-Game Logic, Queen is determined by the rjw_genes_queen GeneDefOf (Not Xenotype). 
        /// </summary>
        /// <returns>A list of queens on the players HomeMap</returns>
        public static List<Pawn> GetQueensOnMap()
        {
            Map map = Find.Maps.Where(mapCandidate => mapCandidate.IsPlayerHome).First();

            if (map != null)
            {
                List<Pawn> playersPawns = map.mapPawns.SpawnedPawnsInFaction(Faction.OfPlayer);
                return playersPawns.FindAll(pawn => pawn.Spawned && IsAdultQueen(pawn));
            }
            // Fallback: Something is wrong with Map 
            return new List<Pawn>();
        }

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
            Map homeMap = Find.Maps.Where(mapCandidate => mapCandidate.IsPlayerHome).First();
            return
                homeMap != null && pawn != null
                && pawn.Spawned
                && pawn.Map == homeMap;
        }
    }
}
