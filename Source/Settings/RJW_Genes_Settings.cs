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
            listing_Standard.CheckboxLabeled("detailed-debug", ref rjw_genes_detailed_debug, "Adds detailed information to the log about interactions and genes.", 0f, 1f);
            listing_Standard.End();
        }


        public static bool rjw_genes_detailed_debug = false;

    }
}
