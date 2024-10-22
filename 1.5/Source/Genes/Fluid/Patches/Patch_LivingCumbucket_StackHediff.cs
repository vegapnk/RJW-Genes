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

    public class Patch_LivingCumbucket_StackHediff
    {

        /// <summary>
        /// This is the amount of fluid required if the pawn has a bodysize of 1, to reach a severity in the hediff of 1. 
        /// The hediff can still be increased over 1.0. 
        /// </summary>
        const float fluid_amount_required_for_hediff_severity_ = 100.0f;
        public static bool Prepare() => ModsConfig.IsActive("vegapnk.cumpilation");

        public static void PostFix(SexProps props)
        {
            // ShortCuts: Exit Early if Pawn or Partner are null (can happen with Masturbation or other nieche-cases)
            if (props == null || props.pawn == null || !props.hasPartner())
                return;

            // Is not internal Sex
            if (!Cumpilation.Cumflation.StuffingUtility.IsSexTypeThatCanCumstuff(props) && !Cumpilation.Cumflation.CumflationUtility.IsSexTypeThatCanCumflate(props))
                return;

            Pawn pawnA = props.pawn;
            Pawn pawnB = props.partner;

            if (pawnA.genes != null && pawnA.genes.HasActiveGene(GeneDefOf.rjw_genes_living_cumbucket) && FluidUtility.GetTotalFluidAmount(pawnB) > 0)
            {
                ISexPartHediff genital = Cumpilation.Common.FluidUtility.GetGenitalsWithFluids(pawnB,filterForShootsOnOrgasm:true).RandomElement();
                if (genital != null)
                {
                    var comp = genital.GetPartComp();
                    StackUpLivingCumbucket(pawnA, comp.FluidAmount, comp.Fluid, pawnB);
                }
            }

            if (pawnB.genes != null && pawnB.genes.HasActiveGene(GeneDefOf.rjw_genes_living_cumbucket) && FluidUtility.GetTotalFluidAmount(pawnA) > 0)
            {
                ISexPartHediff genital = Cumpilation.Common.FluidUtility.GetGenitalsWithFluids(pawnA, filterForShootsOnOrgasm: true).RandomElement();
                if (genital != null)
                {
                    var comp = genital.GetPartComp();
                    StackUpLivingCumbucket(pawnB, comp.FluidAmount, comp.Fluid, pawnA);
                }
            }
        }

        public static void StackUpLivingCumbucket(Pawn pawn, float cumamount, SexFluidDef fluid, Pawn source)
        {
            float bodysize = pawn.BodySize;
            float result_severity_increase = cumamount / (fluid_amount_required_for_hediff_severity_ * bodysize);

            Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.rjw_genes_filled_living_cumbucket);
            if (hediff == null)
            {
                hediff = pawn.health.GetOrAddHediff(HediffDefOf.rjw_genes_filled_living_cumbucket);
                hediff.Severity = 0.01f;
            }

            var storage = hediff.TryGetComp<Cumpilation.Cumflation.HediffComp_SourceStorage>();
            if (storage != null)
            {
                Cumpilation.Cumflation.FluidSource entry = new Cumpilation.Cumflation.FluidSource() { amount = cumamount, fluid=fluid, pawn = source };
                storage.AddOrMerge(entry);
            }

            hediff.Severity += result_severity_increase;
            ModLog.Debug($"Pumping the living cumbucket {pawn} (Bodysize {bodysize}) with {cumamount} cum, resulting in severity {hediff.Severity} (+{result_severity_increase})");
        }
    }
}
