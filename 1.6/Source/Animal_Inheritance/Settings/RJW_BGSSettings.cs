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
            listing_Standard.Gap(5f);
            listing_Standard.Gap(24f);
            listing_Standard.CheckboxLabeled("rjw_genes_animal_inheritance_settings_enabled_key".Translate(), ref rjw_bgs_enabled, "rjw_genes_animal_inheritance_settings_enabled_explanation".Translate(), 0f, 1f);
            //listing_Standard.CheckboxLabeled("sexfrenzy", ref sexfrenzy, "disable the effects", 0f, 1f);
            listing_Standard.Gap(5f);
            //1.6 Fix added (TipSignal?)(TipSignal)
            listing_Standard.Label("gene inheritance chance: " + Math.Round((double)(RJW_BGSSettings.rjw_bgs_global_gene_chance * 100f), 0).ToString() + "%", -1f, (TipSignal?)(TipSignal)"modify chance for a gene to be inherited.");
            RJW_BGSSettings.rjw_bgs_global_gene_chance = listing_Standard.Slider(RJW_BGSSettings.rjw_bgs_global_gene_chance, 0f, 5f);
            listing_Standard.Gap(5f);
            listing_Standard.CheckboxLabeled("rjw_genes_animal_inheritance_settings_added_as_xenogene_key".Translate(), ref rjw_bgs_animal_genes_as_xenogenes, "rjw_genes_animal_inheritance_settings_added_as_xenogene_explanation".Translate(), 0f, 1f);
            listing_Standard.Gap(5f);
            
            listing_Standard.CheckboxLabeled("rjw_genes_animal_inheritance_settings_ve_genetics_hybridization_key".Translate(), ref rjw_bgs_VE_genetics, "rjw_genes_animal_inheritance_settings_ve_genetics_hybridization_explanation".Translate(), 0f, 1f);
            listing_Standard.Gap(5f);
            //1.6 Fix added (TipSignal?)(TipSignal)
            listing_Standard.Label("VE Hybrid Chance: " + Math.Round((double)(RJW_BGSSettings.rjw_bgs_ve_genetics_chance * 100f), 0).ToString() + "%", -1f, (TipSignal?)(TipSignal)"modify chance for a bestiality child to be hybrid.");
            RJW_BGSSettings.rjw_bgs_ve_genetics_chance = listing_Standard.Slider(RJW_BGSSettings.rjw_bgs_ve_genetics_chance, 0f, 1f);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<bool>(ref RJW_BGSSettings.rjw_bgs_enabled, "rjw_bgs_enabled", RJW_BGSSettings.rjw_bgs_enabled, true);
            Scribe_Values.Look<float>(ref RJW_BGSSettings.rjw_bgs_global_gene_chance, "rjw_bgs_global_gene_chance", RJW_BGSSettings.rjw_bgs_global_gene_chance, true);
            Scribe_Values.Look<bool>(ref RJW_BGSSettings.rjw_bgs_animal_genes_as_xenogenes, "rjw_bgs_animal_genes_as_xenogenes", RJW_BGSSettings.rjw_bgs_animal_genes_as_xenogenes, true);
            Scribe_Values.Look<bool>(ref RJW_BGSSettings.rjw_bgs_VE_genetics, "rjw_bgs_VE_genetics", RJW_BGSSettings.rjw_bgs_VE_genetics, true);
            Scribe_Values.Look<float>(ref RJW_BGSSettings.rjw_bgs_ve_genetics_chance, "rjw_bgs_ve_genetics_chance", RJW_BGSSettings.rjw_bgs_ve_genetics_chance, true);
        }

        public static float rjw_bgs_global_gene_chance = 1f;
        public static bool rjw_bgs_enabled = true;
        public static bool rjw_bgs_animal_genes_as_xenogenes = false;
        public static bool rjw_bgs_VE_genetics = true;
        public static float rjw_bgs_ve_genetics_chance = 0.25f;
    }
}
