using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace RJW_Genes
{
    [DefOf]
    public static class HediffDefOf
    {
        public static readonly HediffDef rjw_genes_aphrodisiac_pheromone;
        public static readonly HediffDef rjw_genes_fertilin_lost;
        public static readonly HediffDef rjw_genes_succubus_drained;
        public static readonly HediffDef rjw_genes_orgasm_rush_hediff;
        public static readonly HediffDef rjw_genes_fertilin_craving;
        public static readonly HediffDef rjw_genes_evergrowth_sideeffect;
        public static readonly HediffDef rjw_genes_orgasmic_mytosis_hediff;
        public static readonly HediffDef rjw_genes_mytosis_shock_hediff;

        public static readonly HediffDef rjw_genes_twinkification_progress;
        public static readonly HediffDef rjw_genes_feminization_progress;

        public static readonly HediffDef rjw_genes_genetic_rut;

        public static readonly HediffDef rjw_genes_disease_carrier_storage;

        // Note: Counter does meant it counters it, not it's counting
        [MayRequire("LustLicentia.RJWLabs")] public static readonly HediffDef rjw_genes_cumstuffed_counter;
        [MayRequire("LustLicentia.RJWLabs")] public static readonly HediffDef rjw_genes_cumflation_counter;

        public static readonly HediffDef OvaryAgitator;
        public static readonly HediffDef Bioscaffold;

        [MayRequire("rjw.sexperience")] public static readonly HediffDef rjw_genes_filled_living_cumbucket;
    }
}
