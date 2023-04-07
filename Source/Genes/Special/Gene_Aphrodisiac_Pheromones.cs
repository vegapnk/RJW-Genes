﻿using System.Collections.Generic;
using Verse;
using RimWorld;

namespace RJW_Genes
{
    public class Gene_Aphrodisiac_Pheromones : Gene
    {

        // Default XML Setting is that it looses 4 Severity per day - so a "fully libido" gives 6h boost. 
        // This means that adding +.25 equals 1.5h of Libido. 
        // Tick Speed is hence set to 0.5h 

        const int APHRODISIAC_DISTANCE = 25;
        const int TICK_INTERVAL = 60000 / 48 ; // 60k = 1 day, we want 0.5h which is 1/48th of 1 day. 

        const float SEXFREQ_THRESHOLD = 0.5f;

        // Summary: once every one hour check for all pawns nearby and in line of sight (same room) and add/renew a hediff which lasts for 1 hour.
        public override void Tick()
        {
            base.Tick();
            if (this.pawn.IsHashIntervalTick(TICK_INTERVAL) && this.pawn.Map != null)
            {
                // Only spread pheromones if sexdrive above 1
                float sexfrequency = this.pawn.GetStatValue(StatDef.Named("SexFrequency"));
                if(sexfrequency > SEXFREQ_THRESHOLD)
                {
                    foreach (Pawn pawn in this.AffectedPawns(this.pawn.Position, this.pawn.Map))
                    {
                        this.InduceAphrodisiac(pawn);
                    }
                }               
            }
        }

        // Creates an IEnumerable of all pawns which are closeby and in lineofsight, self and other pawns with aphrodisiac pheromones gene are skipped (to prevent loops).
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
                // Do nothing for pawns that also have pheromones
                if (GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_aphrodisiac_pheromones))
                    continue;

                // Actual Logic: 
                // Pawn qualifies in right distance and needs line of sight. 
                if (pos.DistanceTo(pawn.Position) < APHRODISIAC_DISTANCE && GenSight.LineOfSight(pos, pawn.Position, pawn.Map))
                {
                    yield return pawn;
                }
            }

            yield break;
        }

        private void InduceAphrodisiac(Pawn pawn)
        {
            Hediff aphrodisiac = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.rjw_genes_aphrodisiac_pheromone);
            
            if (aphrodisiac != null)
            {
                aphrodisiac.Severity += 0.25f;
            }
            else
            {
                aphrodisiac = HediffMaker.MakeHediff(HediffDefOf.rjw_genes_aphrodisiac_pheromone, pawn);
                aphrodisiac.Severity = 0.5f;
                pawn.health.AddHediff(aphrodisiac);
            }
        }

    }
}
