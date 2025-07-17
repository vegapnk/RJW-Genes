using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using rjw;
using RimWorld;
using Verse;
using RJW_Genes;

namespace CumpilationPatcher
{
    class Patch_Cumpilation_Inflatable
    {

        public static bool Prepare() => ModsConfig.IsActive("vegapnk.cumpilation");

        // This patch does not need the normal Harmony Targetting, 
        // as it needs to be added only on demand (See HarmonyInit.cs)
        public static void PostFix(SexProps props)
        {
            
            if (props == null || props.pawn == null || props.partner == null) return;


            if (props.pawn.genes != null && props.pawn.genes.HasActiveGene(RJW_Genes.GeneDefOf.rjw_genes_inflatable) )
            {
                AddOrIncreaseCumflationCounterHediffs(props.pawn);
            }

            if (props.partner.genes != null && props.partner.genes.HasActiveGene(RJW_Genes.GeneDefOf.rjw_genes_inflatable))
            {
                AddOrIncreaseCumflationCounterHediffs(props.partner);
            }
        }

        public static void AddOrIncreaseCumflationCounterHediffs(Pawn inflated)
        {

            List<(Hediff,HediffDef)> expectedCounterHediffs = inflated.health.hediffSet.hediffs
                .Where(hediff => FindMatchingCounterHediff(hediff.def) != null)
                .Select(hediff => (hediff,FindMatchingCounterHediff(hediff.def)))
                .ToList();

            ModLog.Debug($"Adding or Increasing {expectedCounterHediffs.Count()} expected CounterHediffs for Pawn {inflated}");

            foreach ((Hediff,HediffDef) matchedHediffs in expectedCounterHediffs) {
                if (inflated.health.hediffSet.HasHediff(matchedHediffs.Item2)) { 
                    Hediff counter = inflated.health.GetOrAddHediff(matchedHediffs.Item2);
                    counter.Severity = matchedHediffs.Item1.Severity;
                } else
                {
                    Hediff counter = HediffMaker.MakeHediff(matchedHediffs.Item2, inflated, matchedHediffs.Item1.Part);
                    counter.Severity = matchedHediffs.Item1.Severity;
                    inflated.health.AddHediff(counter);
                }
            }
        }

        public static HediffDef FindMatchingCounterHediff(HediffDef hediffDef)
        {
            if (hediffDef == null) return null;

            var result =  DefDatabase<HediffDef>.AllDefsListForReading
                .Where(def => {
                    if (def.comps == null || def.comps.Count == 0) return false;
                    var Matcher = def.comps.FirstOrFallback(comp => comp is HediffCompProperties_MatchSeverityOfHediff,null);
                    if (Matcher == null) return false;
                    HediffCompProperties_MatchSeverityOfHediff MatcherCasted = (HediffCompProperties_MatchSeverityOfHediff)Matcher;
                    return MatcherCasted.hediffToMatch == hediffDef;
                })
                .FirstOrFallback(null);
            if (result != null) ModLog.Debug($"Found CounterHediff {result} as counter for {hediffDef}");

            return result;
        }

        public static Hediff CreateOrGetCumflationCounterHediff(Pawn inflated, HediffDef counterCumflationDef, BodyPartRecord bodyPartRecord)
        {
            Hediff cumflationHediff = inflated.health.hediffSet.GetFirstHediffOfDef(counterCumflationDef);
            if (cumflationHediff == null)
            {
                cumflationHediff = HediffMaker.MakeHediff(counterCumflationDef, inflated, bodyPartRecord);
                cumflationHediff.Severity = 0;
                inflated.health.AddHediff(cumflationHediff, bodyPartRecord);
            }
            return cumflationHediff;

        }
    }
}