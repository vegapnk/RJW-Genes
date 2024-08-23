using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Verse;

namespace RJW_Genes
{
    public class Thoughtworker_MultipleQueens_Mood : ThoughtWorker
    {

        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            // Error Handling and Check for Pawn being on Map
            if (p == null || !p.Spawned)
                return (ThoughtState) false;
            // Queens cannot have loyalty thoughts
            if (GeneUtility.HasGeneNullCheck(p, GeneDefOf.rjw_genes_queen))
                return (ThoughtState)false;
            // If the pawn is not on Map (e.g. caravan), no mali 
            if (!HiveUtility.PawnIsOnHomeMap(p))
                return (ThoughtState)false;

            if (GeneUtility.HasGeneNullCheck(p, GeneDefOf.rjw_genes_zealous_loyalty) && HiveUtility.QueensOnMap() >= 2)
            {
                return (ThoughtState)true;
            }

            return (ThoughtState) false;
        }

    }
}
