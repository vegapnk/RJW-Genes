using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;

namespace RJW_Genes.Animal_Inheritance
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
            listing_Standard.CheckboxLabeled("enabled", ref enabled, "no function yet", 0f, 1f);
            //listing_Standard.CheckboxLabeled("sexfrenzy", ref sexfrenzy, "disable the effects", 0f, 1f);
            listing_Standard.End();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<bool>(ref RJW_BGSSettings.enabled, "enabled", RJW_BGSSettings.enabled, true);
        }

        public static bool enabled;
    }
}
