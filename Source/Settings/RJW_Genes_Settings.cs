using System;
using Verse;
using UnityEngine;
using RimWorld;

namespace RJW_Genes
{
    public class RJW_Genes_Settings : ModSettings
    {
        public static void DoWindowContents(Rect inRect)
        {
            //Copied from RJW settings mostly
            Rect outRect = new Rect(0f, 30f, inRect.width, inRect.height - 30f);
            Rect rect = new Rect(0f, 0f, inRect.width - 16f, inRect.height + 300f);

            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.maxOneColumn = true;
            listing_Standard.ColumnWidth = rect.width / 2.05f;
            listing_Standard.Begin(rect);
            listing_Standard.Gap(24f); 
            // Genitalia Resizing Age
            listing_Standard.Label("Genitalia resizing age" + ": " +
                Math.Round((double)(RJW_Genes_Settings.rjw_genes_resizing_age), 0).ToString() , -1f, "years.");
            RJW_Genes_Settings.rjw_genes_resizing_age = listing_Standard.Slider(RJW_Genes_Settings.rjw_genes_resizing_age, 18f, 100f);
            listing_Standard.Gap(4f);
            // Evergrowth Speed 
            listing_Standard.Label("number of ticks between genitalia evergrowth updates (600 tick for ~2cm/day)" + ": " +
               Math.Round((double)(RJW_Genes_Settings.rjw_genes_evergrowth_ticks), 0).ToString() , -1f, "ticks.");
            RJW_Genes_Settings.rjw_genes_evergrowth_ticks = (int) listing_Standard.Slider(RJW_Genes_Settings.rjw_genes_evergrowth_ticks, 600, 60000);
            listing_Standard.Gap(4f);
            // Fertilin Gain From Animals
            listing_Standard.Label("Fertilin-Gain from Animals" + ": " +
               Math.Round((double)(RJW_Genes_Settings.rjw_genes_fertilin_from_animals_factor * 100f), 0).ToString() + "", -1f, "of fertilin gained (compared to human-baseline).");
            RJW_Genes_Settings.rjw_genes_fertilin_from_animals_factor = listing_Standard.Slider(RJW_Genes_Settings.rjw_genes_fertilin_from_animals_factor, 0f, 3f);

            listing_Standard.Gap(5f);
            listing_Standard.CheckboxLabeled("Sexdemon Visits", ref rjw_genes_sexdemon_visit, "If enabled, incubi and succubi can spawn in through an event.", 0f, 1f);
            if (rjw_genes_sexdemon_visit)
            {
                listing_Standard.Gap(3f);
                listing_Standard.CheckboxLabeled("  Size matters", ref rjw_genes_sexdemon_join_size_matters, "Incubi and succubi will consider size/tightness of partners genital for deciding if they want to join", 0f, 1f);
                listing_Standard.Gap(3f);
                listing_Standard.CheckboxLabeled("  Sexdemon groups", ref rjw_genes_sexdemon_visit_groups, "Multiple sexdemons can spawn during a event", 0f, 1f);
                listing_Standard.Gap(3f);
                listing_Standard.CheckboxLabeled("  Succubi", ref rjw_genes_sexdemon_visit_succubi, "Allow incubi to spawn through this even", 0f, 1f);
                listing_Standard.Gap(3f);
                listing_Standard.CheckboxLabeled("  Incubi", ref rjw_genes_sexdemon_visit_incubi, "Allow incubi to spawn through this even", 0f, 1f);
            }

            listing_Standard.Gap(4f);


            listing_Standard.Gap(4f);
            listing_Standard.CheckboxLabeled("Regret Stealing Love", ref regretStealingLovinThoughtDisabled, "If off, pawns will not get bad thoughts for seduction.");

            listing_Standard.Gap(5f);
            listing_Standard.CheckboxLabeled("generous-donor cheatmode", ref rjw_genes_generous_donor_cheatmode, "When enabled, pawns with the 'generous donor' are not drained and not fertilin exhausted. Hence they can fuel succubi and incubi non-stop. This makes them drastically easier to keep, and you should not do it.", 0f, 1f);

            listing_Standard.Gap(5f);
            listing_Standard.CheckboxLabeled("detailed-debug", ref rjw_genes_detailed_debug, "Adds detailed information to the log about interactions and genes.", 0f, 1f);
            listing_Standard.End();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref RJW_Genes_Settings.rjw_genes_evergrowth_ticks, "rjw_genes_evergrowth_ticks", RJW_Genes_Settings.rjw_genes_evergrowth_ticks, true);
            Scribe_Values.Look<float>(ref RJW_Genes_Settings.rjw_genes_resizing_age, "rjw_genes_resizing_age", RJW_Genes_Settings.rjw_genes_resizing_age, true);
            Scribe_Values.Look<float>(ref RJW_Genes_Settings.rjw_genes_fertilin_from_animals_factor, "rjw_genes_fertilin_from_animals_factor", RJW_Genes_Settings.rjw_genes_fertilin_from_animals_factor, true);
            Scribe_Values.Look<bool>(ref RJW_Genes_Settings.rjw_genes_detailed_debug, "rjw_genes_detailed_debug", RJW_Genes_Settings.rjw_genes_detailed_debug, true);
            Scribe_Values.Look(ref regretStealingLovinThoughtDisabled, "regretStealingLovinThoughtDisabled", regretStealingLovinThoughtDisabled, true);
            Scribe_Values.Look<bool>(ref RJW_Genes_Settings.rjw_genes_generous_donor_cheatmode, "rjw_genes_generous_donor_cheatmode", RJW_Genes_Settings.rjw_genes_generous_donor_cheatmode, true); 
            Scribe_Values.Look<bool>(ref RJW_Genes_Settings.rjw_genes_sexdemon_visit, "rjw_genes_sexdemon_visit", RJW_Genes_Settings.rjw_genes_sexdemon_visit, true);
            Scribe_Values.Look<bool>(ref RJW_Genes_Settings.rjw_genes_sexdemon_join_size_matters, "rjw_genes_sexdemon_join_size_matters", RJW_Genes_Settings.rjw_genes_sexdemon_join_size_matters, true);
            Scribe_Values.Look<bool>(ref RJW_Genes_Settings.rjw_genes_sexdemon_visit_groups, "rjw_genes_sexdemon_groups", RJW_Genes_Settings.rjw_genes_sexdemon_visit_groups, true);
            Scribe_Values.Look<bool>(ref RJW_Genes_Settings.rjw_genes_sexdemon_visit_succubi, "rjw_genes_sexdemon_succubi", RJW_Genes_Settings.rjw_genes_sexdemon_visit_succubi, true);
            Scribe_Values.Look<bool>(ref RJW_Genes_Settings.rjw_genes_sexdemon_visit_incubi, "rjw_genes_sexdemon_incubi", RJW_Genes_Settings.rjw_genes_sexdemon_visit_incubi, true);
        }

        public static bool rjw_genes_detailed_debug = false;
        public static float rjw_genes_fertilin_from_animals_factor = 0.1f;
        public static float rjw_genes_resizing_age = 20;
        public static int rjw_genes_evergrowth_ticks = 60000;
        public static bool regretStealingLovinThoughtDisabled = false;

        public static bool rjw_genes_sexdemon_visit = true;
        public static bool rjw_genes_sexdemon_join_size_matters = true;
        public static bool rjw_genes_sexdemon_visit_groups = true;
        public static bool rjw_genes_sexdemon_visit_succubi = true;
        public static bool rjw_genes_sexdemon_visit_incubi = true;




        public static bool rjw_genes_generous_donor_cheatmode = false;
    }
}
