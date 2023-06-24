using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;

namespace RJW_Genes
{

    /// <summary>
    /// Checks if there is (exactly) one queen nearby. 
    /// If the pawn is a queen itself, it's checked if there are OTHER queens nearby.
    /// While this is used for mostly positive things for workers and drones, for queens it checks if there is a rival nearby.
    /// </summary>
    public class ConditionalStatAffecter_QueenCloseBy : ConditionalStatAffecter
    {

        const float EFFECT_DISTANCE = 10.0f;

        public override string Label => (string)"StatsReport_QueenCloseBy".Translate();

        public override bool Applies(StatRequest req)
        {
            if (req.Pawn == null || !req.Pawn.Spawned)
                return false;
            // If the pawn is not on Map (e.g. caravan), no mali 
            if (!HiveUtility.PawnIsOnHomeMap(req.Pawn))
                return false;

            // Case A: Check for Loyal Pawns if their One Queen is nearby
            if (GeneUtility.HasGeneNullCheck(req.Pawn, GeneDefOf.rjw_genes_zealous_loyalty) && HiveUtility.QueensOnMap() == 1)
            {
                Pawn queen = HiveUtility.GetQueensOnMap()[0];

                return req.Pawn.Position.DistanceTo(queen.Position) <= EFFECT_DISTANCE;
            }

            // Case A: Check for Queen if another Queen is nearby
            if (GeneUtility.HasGeneNullCheck(req.Pawn, GeneDefOf.rjw_genes_zealous_loyalty) && HiveUtility.QueensOnMap() >= 2)
            {
                foreach (Pawn queen in HiveUtility.GetQueensOnMap())
                {
                    if (queen != req.Pawn && req.Pawn.Position.DistanceTo(queen.Position) <= EFFECT_DISTANCE)
                        return true;
                }
            }


            return false;
        }
    }
}
