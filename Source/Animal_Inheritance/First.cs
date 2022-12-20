using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rjw;
using Verse;
using RimWorld;

namespace RJW_BGS
{
    [StaticConstructorOnStartup]
    internal static class First
    {
        static First()
        {
            //RJWcopy.Racegroupdictbuilder();
            //Prints all found race dicts (debugging only)
            //logAllFoundRaceGroupGenes
            
        }

        private static void logAllFoundRaceGroupGenes()
        {
            foreach (RaceGroupDef def in DefDatabase<RaceGroupDef>.AllDefs)
            {
                Log.Message("defName = " + def.defName);
                if (def.raceNames != null)
                {
                    foreach (string race in def.raceNames)
                    {
                        Log.Message(race);
                    }
                }
            }
        }
    }
}
