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

        public static int QueensOnMap()
        {
            List<Pawn> playersPawns = Find.CurrentMap.mapPawns.SpawnedPawnsInFaction(Faction.OfPlayer);
            return playersPawns.Count(pawn => pawn.Spawned && IsAdultQueen(pawn));
        }

        public static List<Pawn> GetQueensOnMap()
        {
            List<Pawn> playersPawns = Find.CurrentMap.mapPawns.SpawnedPawnsInFaction(Faction.OfPlayer);
            return playersPawns.FindAll(pawn => pawn.Spawned && IsAdultQueen(pawn));
        }
    }
}
