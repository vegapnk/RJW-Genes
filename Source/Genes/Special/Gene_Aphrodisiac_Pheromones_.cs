using HarmonyLib;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace RJW_Genes
{
    public class Gene_Aphrodisiac_Pheromones : Gene
    {   

        //Summary one every one check for all pawns nearby and in line of sight and add/renew a hediff which increases sexdrive for six hours.
        public override void Tick()
        {
            base.Tick();
            if (this.pawn.IsHashIntervalTick(2500))
            {
                //Only spread pheromones if sexdrive above 1
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

        //Creatus an IEnumerable of all pawns which are closeby and in lineofsight, self and other pawns with aphrodisiac pheromones gene are skipped.
        private IEnumerable<Pawn> AffectedPawns(IntVec3 pos, Map map)
        {
            foreach (Pawn pawn in map.mapPawns.AllPawns)
            {
                if (this.pawn != null && pawn != this.pawn && pos.DistanceTo(pawn.Position) < 5 && GenSight.LineOfSight(pos, pawn.Position, pawn.Map) && !GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_aphrodisiac_pheromones))
                {
                    yield return pawn;
                }
            }
            //IEnumerator<Pawn> enumerator = null;
            yield break;
        }

        //Applies er renews a hediff which increases sexdrive for 6 hours
        private void InduceAphrodisiac(Pawn pawn, float sexfrequency)
        {
            Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Aphrodisiac_Pheromone);
            
            if (hediff != null)
            {
                hediff.Severity = 1f;
            }
            else
            {
                Hediff aphrodisiac = HediffMaker.MakeHediff(HediffDefOf.Aphrodisiac_Pheromone, pawn);
                foreach (StatModifier stat in aphrodisiac.CurStage.statFactors)
                {
                    //Log.Message(pawn.Name.ToString());
                    //Log.Message(stat.stat.defName);
                    //Log.Message(stat.value.ToString());
                    if (stat.stat.defName == "SexFrequency")
                    {
                        stat.value = ModifySexfrequency(pawn, sexfrequency);
                        pawn.health.AddHediff(aphrodisiac);
                    }                    
                }
            }
        }

        //Function to modify aphrodisiac strength, currently has no effect, but it's an easy hook for other modders.
        public float ModifySexfrequency(Pawn pawn, float sexfrequency)
        {
            return sexfrequency;
        }
    }
}
