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
            listing_Standard.Label("rjw_genes_settings_genitalia_resizing_age".Translate() + " " +
                Math.Round((double)(RJW_Genes_Settings.rjw_genes_resizing_age), 0).ToString() + "rjw_genes_settings_genitalia_resizing_postfix".Translate(), -1f, "rjw_genes_settings_genitalia_resizing_age_explanation".Translate());
            RJW_Genes_Settings.rjw_genes_resizing_age = listing_Standard.Slider(RJW_Genes_Settings.rjw_genes_resizing_age, 18f, 100f);
            listing_Standard.Gap(4f);
            // Fertilin Gain From Animals
            listing_Standard.Label("rjw_genes_settings_fertilin_gain_from_animals".Translate() + " " +
               Math.Round((double)(RJW_Genes_Settings.rjw_genes_fertilin_from_animals_factor * 100f), 0).ToString() + "%", -1f, "rjw_genes_settings_fertilin_gain_from_animals_explanation".Translate());
            RJW_Genes_Settings.rjw_genes_fertilin_from_animals_factor = listing_Standard.Slider(RJW_Genes_Settings.rjw_genes_fertilin_from_animals_factor, 0f, 3f);

            listing_Standard.Gap(5f);
            listing_Standard.CheckboxLabeled("rjw_genes_settings_sexdemon_spawn_key".Translate(), ref rjw_genes_sexdemon_visit, "rjw_genes_settings_sexdemon_spawn_explanation".Translate(), 0f, 1f);
            if (rjw_genes_sexdemon_visit)
            {
                listing_Standard.Gap(3f);
                listing_Standard.CheckboxLabeled("  " + "rjw_genes_settings_sexdemon_size_matters_key".Translate(), ref rjw_genes_sexdemon_join_size_matters, "rjw_genes_settings_sexdemon_size_matters_explanation".Translate(), 0f, 1f);
                listing_Standard.Gap(3f);
                listing_Standard.CheckboxLabeled("  " + "rjw_genes_settings_sexdemon_group_spawn_key".Translate(), ref rjw_genes_sexdemon_visit_groups, "rjw_genes_settings_sexdemon_group_spawn_explanation".Translate(), 0f, 1f);
                listing_Standard.Gap(3f);
                listing_Standard.CheckboxLabeled("  " + "rjw_genes_settings_sexdemon_succubi_spawn_key".Translate(), ref rjw_genes_sexdemon_visit_succubi, "rjw_genes_settings_sexdemon_succubi_spawn_explanation".Translate(), 0f, 1f);
                listing_Standard.Gap(3f);
                listing_Standard.CheckboxLabeled("  " + "rjw_genes_settings_sexdemon_incubi_spawn_key".Translate(), ref rjw_genes_sexdemon_visit_incubi, "rjw_genes_settings_sexdemon_incubi_spawn_explanation".Translate(), 0f, 1f);
            }

            listing_Standard.Gap(4f);


            listing_Standard.Gap(4f);
            listing_Standard.CheckboxLabeled("rjw_genes_settings_regret_stealing_love_key".Translate(), ref regretStealingLovinThoughtDisabled, "rjw_genes_settings_regret_stealing_love_explanation".Translate());

            listing_Standard.Gap(4f);
            listing_Standard.CheckboxLabeled("rjw_genes_settings_animal_mating_needs_penis_key".Translate(), ref animalMatingPulseCheckForGenitals, "rjw_genes_settings_animal_mating_needs_penis_explanation".Translate());


            listing_Standard.Gap(5f);
            listing_Standard.Label("rjw_genes_genetic_disease_header_key".Translate());
            listing_Standard.Gap(4f);
            listing_Standard.CheckboxLabeled("\t" + "rjw_genes_settings_genetic_disease_spread_key".Translate(), ref rjw_genes_genetic_disease_spread, "rjw_genes_settings_genetic_disease_spread_explanation".Translate(), 0f, 1f);
            listing_Standard.Gap(4f);
            listing_Standard.CheckboxLabeled("\t" + "rjw_genes_genetic_disease_as_endogenes_key".Translate(), ref rjw_genes_genetic_disease_as_endogenes, "rjw_genes_genetic_disease_as_endogenes_explanation".Translate(), 0f, 1f);
            listing_Standard.Gap(4f);
            listing_Standard.CheckboxLabeled("\t" + "rjw_genes_genetic_disease_spread_only_on_penetrative_sex_key".Translate(), ref rjw_genes_genetic_disease_spread_only_on_penetrative_sex, "rjw_genes_genetic_disease_spread_only_on_penetrative_sex_explanation".Translate(), 0f, 1f);


            listing_Standard.Gap(10f);
            listing_Standard.CheckboxLabeled("rjw_genes_settings_generous_donor_cheatmode_key".Translate(), ref rjw_genes_generous_donor_cheatmode, "rjw_genes_settings_generous_donor_cheatmode_explanation".Translate(), 0f, 1f);
            listing_Standard.Gap(5f);
            listing_Standard.CheckboxLabeled("rjw_genes_settings_detailed_debug_key".Translate(), ref rjw_genes_detailed_debug, "rjw_genes_settings_detailed_debug_explanation".Translate(), 0f, 1f);
            listing_Standard.End();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<float>(ref RJW_Genes_Settings.rjw_genes_resizing_age, "rjw_genes_resizing_age", RJW_Genes_Settings.rjw_genes_resizing_age, true);
            Scribe_Values.Look<float>(ref RJW_Genes_Settings.rjw_genes_fertilin_from_animals_factor, "rjw_genes_fertilin_from_animals_factor", RJW_Genes_Settings.rjw_genes_fertilin_from_animals_factor, true);
            Scribe_Values.Look<bool>(ref RJW_Genes_Settings.rjw_genes_detailed_debug, "rjw_genes_detailed_debug", RJW_Genes_Settings.rjw_genes_detailed_debug, true);
            Scribe_Values.Look(ref regretStealingLovinThoughtDisabled, "regretStealingLovinThoughtDisabled", regretStealingLovinThoughtDisabled, true);

            Scribe_Values.Look(ref animalMatingPulseCheckForGenitals, "animalMatingPulseCheckForGenitals", animalMatingPulseCheckForGenitals, true);
            Scribe_Values.Look<bool>(ref RJW_Genes_Settings.rjw_genes_generous_donor_cheatmode, "rjw_genes_generous_donor_cheatmode", RJW_Genes_Settings.rjw_genes_generous_donor_cheatmode, true); 
            Scribe_Values.Look<bool>(ref RJW_Genes_Settings.rjw_genes_sexdemon_visit, "rjw_genes_sexdemon_visit", RJW_Genes_Settings.rjw_genes_sexdemon_visit, true);
            Scribe_Values.Look<bool>(ref RJW_Genes_Settings.rjw_genes_sexdemon_join_size_matters, "rjw_genes_sexdemon_join_size_matters", RJW_Genes_Settings.rjw_genes_sexdemon_join_size_matters, true);
            Scribe_Values.Look<bool>(ref RJW_Genes_Settings.rjw_genes_sexdemon_visit_groups, "rjw_genes_sexdemon_groups", RJW_Genes_Settings.rjw_genes_sexdemon_visit_groups, true);
            Scribe_Values.Look<bool>(ref RJW_Genes_Settings.rjw_genes_sexdemon_visit_succubi, "rjw_genes_sexdemon_succubi", RJW_Genes_Settings.rjw_genes_sexdemon_visit_succubi, true);
            Scribe_Values.Look<bool>(ref RJW_Genes_Settings.rjw_genes_sexdemon_visit_incubi, "rjw_genes_sexdemon_incubi", RJW_Genes_Settings.rjw_genes_sexdemon_visit_incubi, true);

            Scribe_Values.Look<bool>(ref RJW_Genes_Settings.rjw_genes_genetic_disease_spread, "rjw_genes_genetic_disease_spread", RJW_Genes_Settings.rjw_genes_genetic_disease_spread, true);
            Scribe_Values.Look<bool>(ref RJW_Genes_Settings.rjw_genes_genetic_disease_as_endogenes, "rjw_genes_genetic_disease_as_endogenes", RJW_Genes_Settings.rjw_genes_genetic_disease_as_endogenes, true);
            Scribe_Values.Look<bool>(ref RJW_Genes_Settings.rjw_genes_genetic_disease_spread_only_on_penetrative_sex, "rjw_genes_genetic_disease_spread_only_on_penetrative", RJW_Genes_Settings.rjw_genes_genetic_disease_spread_only_on_penetrative_sex, true);

        }

        public static bool rjw_genes_detailed_debug = false;
        public static float rjw_genes_fertilin_from_animals_factor = 0.1f;
        public static float rjw_genes_resizing_age = 20;
        public static bool regretStealingLovinThoughtDisabled = false;
        public static bool animalMatingPulseCheckForGenitals = true;

        public static bool rjw_genes_sexdemon_visit = true;
        public static bool rjw_genes_sexdemon_join_size_matters = true;
        public static bool rjw_genes_sexdemon_visit_groups = true;
        public static bool rjw_genes_sexdemon_visit_succubi = true;
        public static bool rjw_genes_sexdemon_visit_incubi = true;

        public static bool rjw_genes_genetic_disease_spread = true;
        public static bool rjw_genes_genetic_disease_as_endogenes = true;
        public static bool rjw_genes_genetic_disease_spread_only_on_penetrative_sex = false;

        public static bool rjw_genes_generous_donor_cheatmode = false;
    }
}
