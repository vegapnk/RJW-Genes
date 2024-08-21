using RJW_Genes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_BGS
{
    public class VGEHybridUtility
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


        /// <summary>
        /// Small Method for debugging - I used it mostly on game-startup to see if reading all Defs worked fine. 
        /// Introduced after the VGE-Hybridization Rework from #116
        /// </summary>
        public static void LogAllFoundVGEHybridDefinitions()
        {
            IEnumerable<VGEHybridOffspringDefs> defs = DefDatabase<VGEHybridOffspringDefs>.AllDefs;
            var parents = defs.SelectMany(def => def.SupportedParentKindDefs).Distinct();
            var offsprings = defs.SelectMany(def => def.PossibleHybdridChildKindDefs).Distinct();
            RJW_Genes.ModLog.Message($"Found {defs.Count()} VGEHybridOffspringDefs, covering {parents.Count()} distinct possible parent-animals and {offsprings.Count()} distinct possible hybrid-children.");
        }

    }

}
