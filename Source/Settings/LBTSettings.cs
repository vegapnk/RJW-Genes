using UnityEngine;
using Verse;

// If it isn't blatantly obvious, I unabashedly ripped this settings template from RJW, minus the fact that
// I won't be (personally) supporting multiplayer, and it's all in one file - but I digress ...
namespace LewdBiotech
{
    class LBTSettingsController : Mod
    {
        public LBTSettingsController(ModContentPack content) : base(content)
        {
            GetSettings<LBTSettings>();
        }

        public override string SettingsCategory()
        {
            return "LBTSettings".Translate();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            LBTSettings.DoWindowContents(inRect);
        }
    }

    public class LBTSettings : ModSettings
    {
        // For my own sanity, all now and future settings will have a default disabled/false state (at least, that's the plan), and
        // the settings name and description should reflect that (not that I'm going to add that many settings, mind you)
		public static bool devMode = false;
        public static bool regretStealingLovinThoughtDisabled = false;

        public static void DoWindowContents(Rect inRect)
        {
            // Shrink the settings window a bit - don't need to be that w i d e
            inRect.width = inRect.width - 400;
            inRect.x = inRect.x + 200;
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.Gap(4f);
            listingStandard.CheckboxLabeled("EnableLBTDevLogging".Translate(), ref devMode, "EnableLBTDevLoggingDesc".Translate());
            listingStandard.Gap(4f);
            listingStandard.CheckboxLabeled("RegretStealingLovinThoughtDisabled".Translate(), ref regretStealingLovinThoughtDisabled, "RegretStealingLovinThoughtDisabledDesc".Translate());
            listingStandard.End();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref devMode, "EnableLBTDevLogging", devMode, true);
            Scribe_Values.Look(ref regretStealingLovinThoughtDisabled, "regretStealingLovinThoughtDisabled", regretStealingLovinThoughtDisabled, true);
        }
    }
}
