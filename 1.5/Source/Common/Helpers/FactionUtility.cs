using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    public class FactionUtility
    {

        /// <summary>
        /// Tries to change the goodwill between the factions of two pawns. 
        /// Exceptions when nothing happens:
        /// - Pawns, or Pawns Factions, are null 
        /// - The `actors` Faction is not the players faction
        /// - Both pawns have the same faction
        /// - The Event is not found
        /// </summary>
        /// <param name="actor">The pawn that initiated a faction-goodwill change by his actions</param>
        /// <param name="target">The pawn that was harmed/affected by the action</param>
        /// <param name="HistoryEventDefname">The event defname, for proper reporting</param>
        /// <param name="goodWillChange">How much (positive or negative) the goodwill will change</param>
        public static void HandleFactionGoodWillPenalties(Pawn actor, Pawn target, string HistoryEventDefname, int goodWillChange, bool canSendHostileLetter=true)
        {
            if (actor == null) return;
            if (target == null) return;
            if (
                target.Faction != null && actor.Faction != null
                && target.Faction != actor.Faction
                && target.Faction != Faction.OfPlayer)
            {
                HistoryEventDef reason = DefDatabase<HistoryEventDef>.GetNamedSilentFail(HistoryEventDefname);
                if (reason == null) return;

                target.Faction.TryAffectGoodwillWith(actor.Faction, goodWillChange, true, canSendHostileLetter, reason, target);
            }
        }
    }
}
