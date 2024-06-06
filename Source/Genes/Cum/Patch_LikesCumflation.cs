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

namespace RJW_Genes
{
    /// <summary>
    /// Changes LicentiaLabs (if Present) to add a cumflation-counter hediff, when the pawn is cumflated. 
    /// The counter hediff takes away the negative stats of the original hediff.
    /// This code is exercised / loaded in the HarmonyInit.
    /// Patched File: https://gitgud.io/John-the-Anabaptist/licentia-labs/-/blob/master/Source/LicentiaLabs/LicentiaLabs/Cumflation.cs
    /// </summary>
    /// 
    class Patch_LikesCumflation
    {
        // This patch does not need the normal Harmony Targetting, 
        // as it needs to be added only on demand (See HarmonyInit.cs)
        public static void PostFix(SexProps props)
        {
            
            if (props == null || props.pawn == null || props.partner == null) return;

            if (props.pawn.genes != null && props.pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_likes_cumflation) )
            {
                AddOrIncreaseCumflationCounterHediff(props.pawn);
            }

            if (props.partner.genes != null && props.partner.genes.HasActiveGene(GeneDefOf.rjw_genes_likes_cumflation))
            {
                AddOrIncreaseCumflationCounterHediff(props.partner);
            }
        }

        public static void AddOrIncreaseCumflationCounterHediff(Pawn inflated)
        {
            Hediff cumstuffed_hediff = inflated.health.hediffSet.GetFirstHediffOfDef(LicentiaLabs.Licentia.HediffDefs.Cumstuffed);
            //Hediff cumstuffed_hediff = LicentiaLabs.CumflationHelper.GetCumflationHediff(inflated, LicentiaLabs.Licentia.HediffDefs.Cumstuffed, "stomach");
            if (cumstuffed_hediff != null && cumstuffed_hediff.Severity >= 0.01) {
                ModLog.Message($"{inflated} got cumstuffed and gets the counter-part");
                var bodyPartRecord = inflated.RaceProps.body.AllParts.Find(bpr => bpr.def.defName.Contains("stomach") || bpr.def.defName.Contains("stomach".ToLower()));
                var counter_hediff = CreateOrGetCumflationCounterHediff(inflated, HediffDefOf.rjw_genes_cumstuffed_counter, bodyPartRecord);
                counter_hediff.Severity = cumstuffed_hediff.Severity;
            }

            Hediff cumflation_hediff = inflated.health.hediffSet.GetFirstHediffOfDef(LicentiaLabs.Licentia.HediffDefs.Cumflation);
            if (cumflation_hediff != null && cumflation_hediff.Severity >= 0.01)
            {
                ModLog.Message($"{inflated} got cumflated and gets the counter-part");
                var bodyPartRecord = Genital_Helper.get_genitalsBPR(inflated);
                var counter_hediff = CreateOrGetCumflationCounterHediff(inflated, HediffDefOf.rjw_genes_cumflation_counter, bodyPartRecord);
                counter_hediff.Severity = cumflation_hediff.Severity;
            }
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