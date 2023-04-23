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
    /// Checks if there are multiple queens on the map.
    /// </summary>
    public class ConditionalStatAffecter_MultipleQueens : ConditionalStatAffecter
    {
        public override string Label => (string)"StatsReport_MultipleQueens".Translate();

        public override bool Applies(StatRequest req)
        {
            if (req.Pawn == null || !req.Pawn.Spawned)
                return false;
            // If the pawn is not on Map (e.g. caravan), no mali 
            if (!HiveUtility.PawnIsOnHomeMap(req.Pawn))
                return false;

            if (GeneUtility.HasGeneNullCheck(req.Pawn, GeneDefOf.rjw_genes_zealous_loyalty))
            {
                return HiveUtility.QueensOnMap() >= 2;
            }

            return false;
        } 
    }
}
