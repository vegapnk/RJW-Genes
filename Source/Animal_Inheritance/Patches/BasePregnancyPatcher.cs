﻿using System;
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
    public class BasePregnancyPatcher
    {
        public static List<string> racesgen1 = new List<string>()
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

        public static List<string> racesgen0 = new List<string>()
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
        public static HediffDef controler = DefDatabase<HediffDef>.GetNamed("RJWGenes_AnimalControlHediff", true);
        [HarmonyPostfix]
        [HarmonyPatch("GenerateBabies")]
        public static void addHedif (Hediff_BasePregnancy __instance)
        {
            if (controler == null) return;
            
            foreach (Pawn p in __instance.babies)
            {
                if(p != null)
                {
                    if (racesgen1.Contains(p.kindDef.race.defName))
                    {
                        p.health.AddHediff(controler);
                    }
                }
            }
        }

        

    }
}
