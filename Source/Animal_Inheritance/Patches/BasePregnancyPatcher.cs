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

        //public static HediffDef controler = DefDatabase<HediffDef>.GetNamed("RJWGenes_AnimalControlHediff", true);

        [HarmonyPostfix]
        [HarmonyPatch("GenerateBabies")]
        public static void AddComfortableWithHumansHediff (Hediff_BasePregnancy __instance)
        {
            //if (controler == null) return;
            
            foreach (Pawn p in __instance.babies)
            {
                if(p != null)
                {
                    if (racesgen1.Contains(p.kindDef.race.defName))
                    {
                        p.health.AddHediff(RJW_Genes.HediffDefOf.rjw_genes_animal_control_hediff);
                    }
                }
            }
        }

        

    }
}

/*
 * Error Received on 04.06.2024 
 * Failed to find Verse.HediffDef named RJWGenes_AnimalControlHediff. There are 446 defs of this type loaded.
UnityEngine.StackTraceUtility:ExtractStackTrace ()
(wrapper dynamic-method) MonoMod.Utils.DynamicMethodDefinition:Verse.Log.Error_Patch1 (string)
Verse.DefDatabase`1<Verse.HediffDef>:GetNamed (string,bool)
RJW_BGS.BasePregnancyPatcher:.cctor ()
(wrapper dynamic-method) MonoMod.Utils.DynamicMethodDefinition:rjw.PregnancyHelper.AddPregnancyHediff_Patch1 (Verse.Pawn,Verse.Pawn)
rjw.PregnancyHelper:DoImpregnate (Verse.Pawn,Verse.Pawn)
(wrapper dynamic-method) MonoMod.Utils.DynamicMethodDefinition:rjw.PregnancyHelper.impregnate_Patch1 (rjw.SexProps)
(wrapper dynamic-method) MonoMod.Utils.DynamicMethodDefinition:rjw.JobDriver_Sex.Orgasm_Patch1 (rjw.JobDriver_Sex)
(wrapper dynamic-method) MonoMod.Utils.DynamicMethodDefinition:rjw.JobDriver_Sex.SexTick_Patch2 (rjw.JobDriver_Sex,Verse.Pawn,Verse.Thing,bool,bool)
rjw.JobDriver_Mating/<>c__DisplayClass1_0:<MakeNewToils>b__5 ()
Verse.AI.JobDriver:DriverTick ()
Verse.AI.Pawn_JobTracker:JobTrackerTick ()
Verse.Pawn:Tick ()
Verse.TickList:Tick ()
(wrapper dynamic-method) MonoMod.Utils.DynamicMethodDefinition:Verse.TickManager.DoSingleTick_Patch2 (Verse.TickManager)
Verse.TickManager:TickManagerUpdate ()
Verse.Game:UpdatePlay ()
Verse.Root_Play:Update ()
 */