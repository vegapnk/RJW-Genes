using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    public class ConditionalStatAffecter_VeryManyChildren : ConditionalStatAffecter
    {
        public override string Label => (string)"StatsReport_VeryManyChildren".Translate();

        public const int THRESHOLD_FOR_CHILDREN = 8;

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
                    return pawn.relations.ChildrenCount >= THRESHOLD_FOR_CHILDREN;
                }
            }

            return false;
        }
    }
}
