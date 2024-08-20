using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;
using HarmonyLib;
using rjw;

namespace RJW_BGS
{
    [HarmonyPatch(typeof(Hediff_BasePregnancy))]
    public class Patch_RJW_BasePregnancy_VanillaExpandedGenetics
    {
        public static List<string> supportedHybridRaces = new List<string>()
            {
            "GR_Manbear",
            "GR_Bearman",
            "GR_Manalope",
            "GR_Booman",
            "GR_Manchicken",
            "GR_Turkeyman",
            "GR_Manffalo",
            "GR_Muffaloman",
            "GR_Manwolf",
            "GR_Dogman",
            "GR_Mancat",
            "GR_Catman",
            "GR_Mansquirrel",
            "GR_Moleman",
            "GR_Thrumboman",
            "GR_Hurseman",
            "GR_Manscarab",
            "GR_Lizardman"
            };

        public static List<string> supportedInitialAnimalRaces = new List<string>()
            {
            "Bear_Grizzly",
            "Bear_Polar",
            "Boomalope",
            "Chicken",
            "Duck",
            "Turkey",
            "Goose",
            "Ostrich",
            "Emu",
            "Cassowary",
            "Cow",
            "Muffalo",
            "Bison",
            "Yak",
            "Warg",
            "Wolf_Timber",
            "Wolf_Arctic",
            "Fox_Fennec",
            "Fox_Red",
            "Fox_Arctic",
            "Husky",
            "LabradorRetriever",
            "YorkshireTerrier",
            "Cougar",
            "Panther",
            "Lynx",
            "Cat",
            "GuineaPig",
            "Hare",
            "Snowhare",
            "Squirrel",
            "Rat",
            "Raccoon",
            "Thrumbo",
            "Dromedary",
            "Elk",
            "Horse",
            "Caribou",
            "Donkey",
            "Megascarab",
            "Spelopede",
            "Megaspider",
            "Iguana",
            "Cobra",
            "Tortoise"
            };

        public static HediffDef controler = DefDatabase<HediffDef>.GetNamed("rjw_genes_animal_control_hediff", false);

        [HarmonyPostfix]
        [HarmonyPatch("GenerateBabies")]
        public static void addHedif (Hediff_BasePregnancy __instance)
        {
            if (controler == null) return;

            if (!RJW_BGSSettings.rjw_bgs_VE_genetics) return;

            foreach (Pawn baby in __instance.babies)
            {
                if(baby != null && supportedHybridRaces.Contains(baby.kindDef.race.defName))
                   baby.health.AddHediff(controler);
            }
        }

    }
}
