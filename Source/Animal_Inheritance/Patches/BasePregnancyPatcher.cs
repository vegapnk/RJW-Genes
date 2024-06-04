using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;
using HarmonyLib;
using rjw;
using RimWorld;

namespace RJW_BGS
{
    [HarmonyPatch(typeof(Hediff_BasePregnancy))]
    public class BasePregnancyPatcher
    {
        /// <summary>
        /// The supported races that are produced by Vanilla Genetics Expanded, that can lead to different offsprings when in Human - Animal Sex
        /// </summary>
        public static List<string> firstGenerationOffspringRaces = new List<string>()
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

        /// <summary>
        /// The supported races that can produce Vanilla Genetics hybrids as Human - Animal Sex results. 
        /// </summary>
        public static List<string> parentGenerationOffspringRaces = new List<string>()
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

        [HarmonyPostfix]
        [HarmonyPatch("GenerateBabies")]
        public static void AddComfortableWithHumansHediff (Hediff_BasePregnancy __instance)
        {
            foreach (Pawn baby in __instance.babies)
            {
                if (baby != null && firstGenerationOffspringRaces.Contains(baby.kindDef.race.defName))
                {
                    baby.health.AddHediff(RJW_Genes.HediffDefOf.rjw_genes_animal_control_hediff);
                }
            }
        }

    }
}
