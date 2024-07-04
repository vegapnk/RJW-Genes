using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    /// <summary>
    /// This conditional stat affecter "fires" if the pawn has no children. 
    /// 
    /// DevNote: I salvaged this from 1.3.3 Halamyr Conditional Stat Affecters. 
    /// It seems that with RW 1.5 there was a change how these work, as the req.Pawn seems to be null. 
    /// Now, the pawn is in req.Thing. 
    /// </summary>
    public class ConditionalStatAffecter_NoChildren : ConditionalStatAffecter
    {
        public override string Label => (string)"StatsReport_NoChildren".Translate();

        public override bool Applies(StatRequest req)
        {
            if (req == null || req.Thing == null || !req.Thing.Spawned) return false;

            if (req.Thing is Pawn pawn)
            {
                // Do nothing if Pawn is Baby or Child (#25)
                if (!pawn.ageTracker.Adult)
                    return false;

                if (GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_hardwired_progenity))
                {
                    return pawn.relations.ChildrenCount == 0;
                }
            }

            return false;
        }

    }

}
