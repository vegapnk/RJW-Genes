using System.Collections.Generic;
using Verse;
using RimWorld;

namespace RJW_Genes
{
    public class Gene_Aphrodisiac_Pheromones : Gene
    {   

        // Summary: once every one hour check for all pawns nearby and in line of sight (same room) and add/renew a hediff which lasts for 1 hour.
        public override void Tick()
        {
            base.Tick();
            if (this.pawn.IsHashIntervalTick(2500) && this.pawn.Map != null)
            {
                // Only spread pheromones if sexdrive above 1
                float sexfrequency = this.pawn.GetStatValue(StatDef.Named("SexFrequency"));
                if(sexfrequency > 1f)
                {
                    foreach (Pawn pawn in this.AffectedPawns(this.pawn.Position, this.pawn.Map))
                    {
                        this.InduceAphrodisiac(pawn, sexfrequency);
                    }
                }               
            }
        }

        // Creates an IEnumerable of all pawns which are closeby and in lineofsight, self and other pawns with aphrodisiac pheromones gene are skipped (to prevent loops).
        private IEnumerable<Pawn> AffectedPawns(IntVec3 pos, Map map)
        {
            foreach (Pawn pawn in map.mapPawns.AllPawns)
            {
                if (pawn != null && this.pawn != null && pawn != this.pawn 
                    && pos.DistanceTo(pawn.Position) < 5 && GenSight.LineOfSight(pos, pawn.Position, pawn.Map) 
                    && !GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_aphrodisiac_pheromones))
                {
                    yield return pawn;
                }
            }

            yield break;
        }

        // Applies or renews a hediff which increases sexdrive for 6 hours
        private void InduceAphrodisiac(Pawn pawn, float sexfrequency)
        {
            Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.rjw_genes_aphrodisiac_pheromone);
            
            if (hediff != null)
            {
                hediff.Severity = 1f;
            }
            else
            {
                Hediff aphrodisiac = HediffMaker.MakeHediff(HediffDefOf.rjw_genes_aphrodisiac_pheromone, pawn);
                foreach (StatModifier stat in aphrodisiac.CurStage.statFactors)
                {
                    if (stat.stat.defName == "SexFrequency")
                    {
                        stat.value = ModifySexfrequency(pawn, sexfrequency);
                        pawn.health.AddHediff(aphrodisiac);
                    }                    
                }
            }
        }

        // Function to modify aphrodisiac strength, currently has no effect, but provides an easy hook for other modders and patches.
        public float ModifySexfrequency(Pawn pawn, float sexfrequency)
        {
            return sexfrequency;
        }
    }
}
