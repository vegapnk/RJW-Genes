using HarmonyLib;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using static System.Net.Mime.MediaTypeNames;

namespace RJW_Genes
{

    [HarmonyPatch(typeof(SexUtility), nameof(SexUtility.SatisfyPersonal))]
    public class Patch_LivingCumbucket_StackHediff
    {

        /// <summary>
        /// This is the amount of fluid required if the pawn has a bodysize of 1, to reach a severity in the hediff of 1. 
        /// The hediff can still be increased over 1.0. 
        /// </summary>
        const float fluid_amount_required_for_hediff_severity_ = 100.0f;

        public static void Postfix(SexProps props)
        {
            if (!ModsConfig.IsActive("rjw.sexperience"))
                return;

            // ShortCuts: Exit Early if Pawn or Partner are null (can happen with Masturbation or other nieche-cases)
            if (props == null || props.pawn == null || !props.hasPartner())
                return;

            Pawn pawnA = props.pawn;
            Pawn pawnB = props.partner;

            if (pawnA.genes != null && pawnA.genes.HasActiveGene(GeneDefOf.rjw_genes_living_cumbucket) && CumUtility.GetTotalFluidAmount(pawnB) > 0)
            {
                ProcessLivingCumbucket(pawnA, CumUtility.GetTotalFluidAmount(pawnB));
            }

            if (pawnB.genes != null && pawnB.genes.HasActiveGene(GeneDefOf.rjw_genes_living_cumbucket) && CumUtility.GetTotalFluidAmount(pawnA) > 0)
            {
                ProcessLivingCumbucket(pawnB, CumUtility.GetTotalFluidAmount(pawnA));
            }
        }

        public static void ProcessLivingCumbucket(Pawn pawn, float cumamount)
        {
            float bodysize = pawn.BodySize;
            float result_severity_increase = cumamount / (fluid_amount_required_for_hediff_severity_ * bodysize);


            Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.rjw_genes_filled_living_cumbucket);
            if (hediff == null)
            {
                hediff = pawn.health.GetOrAddHediff(HediffDefOf.rjw_genes_filled_living_cumbucket);
                hediff.Severity = 0.01f;
            }

            hediff.Severity += result_severity_increase;
            ModLog.Debug($"Pumping the living cumbucket {pawn} (Bodysize {bodysize}) with {cumamount} cum, resulting in severity {hediff.Severity} (+{result_severity_increase})");
        }
    }
}
