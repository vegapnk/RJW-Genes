using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;
namespace RJW_Genes
{
    public class RJW_GenesSettings : ModSettings
    {
        public static void DoWindowContents(Rect inRect)
        {   
            Rect outRect = new Rect(0f, 30f, inRect.width, inRect.height - 30f);
            Rect rect = new Rect(0f, 0f, inRect.width - 16f, inRect.height + 300f);
            //Widgets.BeginScrollView(outRect, ref RJWSettings.scrollPosition, rect, true);
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.maxOneColumn = true;
            listing_Standard.ColumnWidth = rect.width / 2.05f;
            listing_Standard.Begin(rect);
            listing_Standard.Gap(30);
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
            //listing_Standard.CheckboxLabeled("sexfrenzy", ref sexfrenzy, "disable the effects", 0f, 1f);
            listing_Standard.Gap(10f);
            listing_Standard.End();
        }
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<bool>(ref RJW_GenesSettings.rjw_genes_sexdemon_visit, "rjw_genes_sexdemon_visit", RJW_GenesSettings.rjw_genes_sexdemon_visit, true);
            Scribe_Values.Look<bool>(ref RJW_GenesSettings.rjw_genes_sexdemon_join_size_matters, "rjw_genes_sexdemon_join_size_matters", RJW_GenesSettings.rjw_genes_sexdemon_join_size_matters, true);
            Scribe_Values.Look<bool>(ref RJW_GenesSettings.rjw_genes_sexdemon_visit_groups, "rjw_genes_sexdemon_groups", RJW_GenesSettings.rjw_genes_sexdemon_visit_groups, true);
            Scribe_Values.Look<bool>(ref RJW_GenesSettings.rjw_genes_sexdemon_visit_succubi, "rjw_genes_sexdemon_succubi", RJW_GenesSettings.rjw_genes_sexdemon_visit_succubi, true);
            Scribe_Values.Look<bool>(ref RJW_GenesSettings.rjw_genes_sexdemon_visit_incubi, "rjw_genes_sexdemon_incubi", RJW_GenesSettings.rjw_genes_sexdemon_visit_incubi, true);
        }

        public static bool rjw_genes_sexdemon_visit = true;
        public static bool rjw_genes_sexdemon_join_size_matters = true;
        public static bool rjw_genes_sexdemon_visit_groups = true;
        public static bool rjw_genes_sexdemon_visit_succubi = true;
        public static bool rjw_genes_sexdemon_visit_incubi = true;

    }
}
