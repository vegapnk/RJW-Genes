using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Verse;

namespace RJW_Genes
{
    public class Thoughtworker_RivalQueen_Mood : ThoughtWorker
    {

        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (p == null || !p.Spawned)
                return (ThoughtState) false;
            // If the pawn is not on Map (e.g. caravan), no mali 
            if (!HiveUtility.PawnIsOnHomeMap(p))
                return (ThoughtState)false;

            if (HiveUtility.IsAdultQueen(p) && HiveUtility.QueensOnMap() >= 2)
            {
                return (ThoughtState) true;
            }

            return (ThoughtState) false;
        }

    }
}
