using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    public class ThoughtWorker_RivalQueen_Social : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn other)
        {
            if (!p.RaceProps.Humanlike)
                return (ThoughtState)false;
            if (!other.RaceProps.Humanlike)
                return (ThoughtState)false;

            if (!RelationsUtility.PawnsKnowEachOther(p, other))
                return (ThoughtState)false;
            // If the pawn is not on Map (e.g. caravan), no mali 
            if (!HiveUtility.PawnIsOnHomeMap(p))
                return (ThoughtState)false;

            // Only check if they are spawned 
            if (!p.Spawned || !other.Spawned)
            {
                return (ThoughtState)false;
            }

            if(HiveUtility.IsAdultQueen(p) && HiveUtility.IsAdultQueen(other))
            {
                return (ThoughtState)true;
            }
            return (ThoughtState)false;
        }
    }
}
