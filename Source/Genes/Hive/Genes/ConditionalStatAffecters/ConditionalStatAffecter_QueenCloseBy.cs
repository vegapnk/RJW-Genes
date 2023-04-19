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
        public override string Label => (string)"StatsReport_QueenCloseBy".Translate();

        public override bool Applies(StatRequest req)
        {
            // => ModsConfig.BiotechActive && req.HasThing && req.Thing.Spawned && req.Thing.Position.InSunlight(req.Thing.Map);
            throw new NotImplementedException();
        } 
    }
}
