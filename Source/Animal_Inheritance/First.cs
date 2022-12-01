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
            RJWcopy.Racegroupdictbuilder();
            //foreach (RaceGroupDef raceGroupDef2  in DefDatabase<RaceGroupDef>.AllDefs)      
            //{
                //Log.Message("defName = " + raceGroupDef2.defName);
            //    if (raceGroupDef2.raceNames != null)
            //    {
            //        foreach (string race in raceGroupDef2.raceNames)
            //        {
                        //Log.Message(race);
            //        }
            //    }
           //}
        }
    }
}
