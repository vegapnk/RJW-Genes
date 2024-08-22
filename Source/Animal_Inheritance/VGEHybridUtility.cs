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
        /// <summary>
        /// All VGE-Hybrids that can result from Bestiality - these are drawn from the existing XML-Defs. 
        /// </summary>
        public static List<PawnKindDef> SupportedHybridRaces { 
            get{return DefDatabase<VGEHybridOffspringDefs>.AllDefs.SelectMany(def => def.PossibleHybridChildKindDefs).Distinct().ToList();}
        }

        /// <summary>
        /// All Animals that can produce VGE Hybrids - these are drawn from the existing XML-Defs. 
        /// </summary>
        public static List<PawnKindDef> SupportedInitialAnimalRaces { 
            get { return DefDatabase<VGEHybridOffspringDefs>.AllDefs.SelectMany(def => def.SupportedParentKindDefs).Distinct().ToList(); } 
        }

        /// <summary>
        /// Returns a possible Hybrid KindDef for a given Animal. 
        /// Null if there is none. 
        /// Random one if there are multiple. 
        /// </summary>
        /// <param name="Parent">The animal fathering the baby</param>
        /// <returns>KindDef for Hybrid originated from Parent Animal. Null on None, Not-Supported or Error. Random one from multiple.</returns>
        public static PawnKindDef LookupPossiblyOffspringHybrid(PawnKindDef Parent)
        {
            if (Parent == null) return null;
            if (!SupportedInitialAnimalRaces.Contains(Parent)) return null; 
            else
            {
                return DefDatabase<VGEHybridOffspringDefs>.AllDefs
                    .Where(def => def.SupportedParentKindDefs.Contains(Parent))
                    .SelectMany(def => def.PossibleHybridChildKindDefs)
                    .Distinct()
                    .RandomElementWithFallback(null);
                // Man I am a true Java Developer
            }
        }

        /// <summary>
        /// Small Method for debugging - I used it mostly on game-startup to see if reading all Defs worked fine. 
        /// Introduced after the VGE-Hybridization Rework from #116
        /// </summary>
        public static void LogAllFoundVGEHybridDefinitions()
        {
            IEnumerable<VGEHybridOffspringDefs> defs = DefDatabase<VGEHybridOffspringDefs>.AllDefs;
            var parents = defs.SelectMany(def => def.SupportedParentKindDefs).Distinct();
            var offsprings = defs.SelectMany(def => def.PossibleHybridChildKindDefs).Distinct();
            RJW_Genes.ModLog.Debug($"Found {defs.Count()} VGEHybridOffspringDefs, covering {parents.Count()} distinct possible parent-animals and {offsprings.Count()} distinct possible hybrid-children.");
        }

    }

}
