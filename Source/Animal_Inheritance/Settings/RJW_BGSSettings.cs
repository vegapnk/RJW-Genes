using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;

namespace RJW_BGS
{
    public class RJW_BGSSettings : ModSettings
    {
        public static void DoWindowContents(Rect inRect)
        {
            //Copied from RJW settings mostly
            Rect outRect = new Rect(0f, 30f, inRect.width, inRect.height - 30f);
            Rect rect = new Rect(0f, 0f, inRect.width - 16f, inRect.height + 300f);
            //Widgets.BeginScrollView(outRect, ref RJWSettings.scrollPosition, rect, true);
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.maxOneColumn = true;
            listing_Standard.ColumnWidth = rect.width / 2.05f;
            listing_Standard.Begin(rect);
            listing_Standard.Gap(24f);
            listing_Standard.CheckboxLabeled("enabled", ref rjw_bgs_enabled, "If toggled, Animal Pregnancies will try inherit genes.", 0f, 1f);
            //listing_Standard.CheckboxLabeled("sexfrenzy", ref sexfrenzy, "disable the effects", 0f, 1f);
            listing_Standard.Gap(5f);
            listing_Standard.Label("gene inheritance chance"+ ": " + 
                Math.Round((double)(RJW_BGSSettings.rjw_bgs_global_gene_chance * 100f), 0).ToString() + "%", -1f, "modify chance for a gene to be inherited.");
            RJW_BGSSettings.rjw_bgs_global_gene_chance = listing_Standard.Slider(RJW_BGSSettings.rjw_bgs_global_gene_chance, 0f, 5f);
            listing_Standard.Gap(5f);
            listing_Standard.CheckboxLabeled("genes as xenogenes", ref rjw_bgs_animal_genes_as_xenogenes, "If toggled, animal genes will be added as xenogenes.", 0f, 1f);
            listing_Standard.Gap(5f);
            listing_Standard.CheckboxLabeled("detailed-debug", ref rjw_bgs_detailed_debug, "Adds detailed information to the log about pregnancies and genes.", 0f, 1f);
            listing_Standard.End();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<bool>(ref RJW_BGSSettings.rjw_bgs_enabled, "rjw_bgs_enabled", RJW_BGSSettings.rjw_bgs_enabled, true);
            Scribe_Values.Look<float>(ref RJW_BGSSettings.rjw_bgs_global_gene_chance, "rjw_bgs_global_gene_chance", RJW_BGSSettings.rjw_bgs_global_gene_chance, true);
            Scribe_Values.Look<bool>(ref RJW_BGSSettings.rjw_bgs_animal_genes_as_xenogenes, "rjw_bgs_animal_genes_as_xenogenes", RJW_BGSSettings.rjw_bgs_animal_genes_as_xenogenes, true);
            Scribe_Values.Look<bool>(ref RJW_BGSSettings.rjw_bgs_detailed_debug, "rjw_bgs_detailed_debug", RJW_BGSSettings.rjw_bgs_detailed_debug, true);
        }

        public static float rjw_bgs_global_gene_chance = 1f;
        public static bool rjw_bgs_enabled = true;
        public static bool rjw_bgs_animal_genes_as_xenogenes = false;
        public static bool rjw_bgs_detailed_debug = false;
    }
}
