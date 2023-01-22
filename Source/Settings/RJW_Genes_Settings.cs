using System;
using Verse;
using UnityEngine;

namespace RJW_Genes
{
    public class RJW_Genes_Settings : ModSettings
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
            listing_Standard.Label("Fertlin-Gain from Animals" + ": " +
                Math.Round((double)(RJW_Genes_Settings.rjw_genes_fertilin_from_animals_factor * 100f), 0).ToString() + "%", -1f, "of fertilin gained (compared to human-baseline).");
            RJW_Genes_Settings.rjw_genes_fertilin_from_animals_factor = listing_Standard.Slider(RJW_Genes_Settings.rjw_genes_fertilin_from_animals_factor, 0f, 3f);
            listing_Standard.Gap(5f);
            listing_Standard.CheckboxLabeled("detailed-debug", ref rjw_genes_detailed_debug, "Adds detailed information to the log about interactions and genes.", 0f, 1f);
            listing_Standard.End();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<float>(ref RJW_Genes_Settings.rjw_genes_fertilin_from_animals_factor, "rjw_genes_fertilin_from_animals_factor", RJW_Genes_Settings.rjw_genes_fertilin_from_animals_factor, true);
            Scribe_Values.Look<bool>(ref RJW_Genes_Settings.rjw_genes_detailed_debug, "rjw_genes_detailed_debug", RJW_Genes_Settings.rjw_genes_detailed_debug, true);
        }

        public static bool rjw_genes_detailed_debug = false;
        public static float rjw_genes_fertilin_from_animals_factor = 0.1f;
    }
}
