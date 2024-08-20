using System.Collections.Generic;
using Verse;
using RimWorld;

namespace RJW_Genes
{
    public class Gene_LifeForce_Empath : Gene
    {

        const int EMPATH_DISTANCE_FALLBACK = 25;
        const int TICK_INTERVAL_FALLBACK = 60000 / 48;

        const float AHEAGO_FALLBACK = 0.02f, SATISFIED_FALLBACK = 0.01f, FRUSTRATED_FALLBACK = -0.01f;

        int empathDistance = 25;
        int tickInterval = 60000 / 48 ; // 60k = 1 day, we want 0.5h which is 1/48th of 1 day. 

        float aheagoIncrement = 0.02f;
        float satisfiedIncrement = 0.01f;
        float frustratedDecrement = -0.01f;


        public Gene_LifeForce_Empath() : base()
        {
            SetValuesFromExtension();
        }

        private void SetValuesFromExtension()
        {
            LifeForceEmpathExtension empathExt = GeneDefOf.rjw_genes_lifeforce_empath.GetModExtension<LifeForceEmpathExtension>();

            tickInterval =  ModExtensionHelper.GetTickIntervalFromModExtension(GeneDefOf.rjw_genes_lifeforce_empath, TICK_INTERVAL_FALLBACK);
            empathDistance = ModExtensionHelper.GetTickIntervalFromModExtension(GeneDefOf.rjw_genes_lifeforce_empath, EMPATH_DISTANCE_FALLBACK);

            aheagoIncrement = empathExt?.aheagoIncrement ?? AHEAGO_FALLBACK;
            satisfiedIncrement = empathExt?.satisfactionIncrement ?? SATISFIED_FALLBACK;
            frustratedDecrement = empathExt?.frustratedDecrement ?? FRUSTRATED_FALLBACK;
        }

        public override void Tick()
        {
            base.Tick();
            if (this.pawn.IsHashIntervalTick(tickInterval) && this.pawn.Map != null)
            {
                // Small check if LifeForce is present - likely minors were ticking this but not having Life-Force yet (#143)
                if (!GeneUtility.HasLifeForce(this.pawn)) return;

                // Check if the pawn is on *a* map. Maybe the reason for (#120)
                if (this.pawn.Map == null) return;

                foreach (Pawn pawn in this.AffectedPawns(this.pawn.Position, this.pawn.Map))
                {
                    this.FarmLifeForce(pawn);
                }
                        
            }
        }

        /// <summary>
        /// Creates an IEnumerable of all pawns which are closeby and in lineofsight, self and other pawns with lifeforce gene are skipped (to prevent loops).
        /// </summary>
        /// <param name="pos">The position of the empath on the map</param>
        /// <param name="map">The map the empath is on</param>
        /// <returns>A list of all pawns that are close enough for the empath to connect.</returns>
        private IEnumerable<Pawn> AffectedPawns(IntVec3 pos, Map map)
        {
            foreach (Pawn pawn in map.mapPawns.AllPawns)
            {
                // Return for trivial errors
                if (pawn == null || this.pawn == null || pawn == this.pawn)
                    continue;
                // Check for position-existance
                if (pawn.Position == null || pos == null || pawn.Map == null)
                    continue;
                // Do nothing if pawn is carried 
                if (pawn.CarriedBy != null)
                    continue;
                // Do nothing if Pawn is Baby or Child (#25)
                if (!pawn.ageTracker.Adult)
                    continue;
                // Do nothing for pawns that also have lifeforce
                if (GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_lifeforce))
                    continue;

                // Actual Logic: 
                // Pawn qualifies in right distance and needs line of sight. 
                if (pos.DistanceTo(pawn.Position) < empathDistance && GenSight.LineOfSight(pos, pawn.Position, pawn.Map))
                {
                    yield return pawn;
                }
            }

            yield break;
        }

        /// <summary>
        /// Adjust the empaths lifeforce depending on the farmed pawns sexneed. 
        /// </summary>
        /// <param name="farmedPawn">The pawn affecting the empath, increasing or decreasing his lifeforce. </param>
        private void FarmLifeForce(Pawn farmedPawn)
        {
            // Short rename to make rest more obvious.
            Pawn empath = pawn;

            if (farmedPawn == null)
                return;

            var sexneed = farmedPawn.needs.TryGetNeed<rjw.Need_Sex>();

            // Shortwire: do nothing on no sexneed.
            if (sexneed == null)
                return;

            if (sexneed.CurLevel >= sexneed.thresh_ahegao())
                GeneUtility.OffsetLifeForce(GeneUtility.GetLifeForceGene(empath), aheagoIncrement);
            else if (sexneed.CurLevel >= sexneed.thresh_satisfied())
                GeneUtility.OffsetLifeForce(GeneUtility.GetLifeForceGene(empath), satisfiedIncrement);
            else if (sexneed.CurLevel <= sexneed.thresh_frustrated())
                GeneUtility.OffsetLifeForce(GeneUtility.GetLifeForceGene(empath), frustratedDecrement);

        }

    }
}
