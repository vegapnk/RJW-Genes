using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using rjw;

namespace RJW_BGS
{
    public class RaceGeneDef : Def
    {
        public int priority;
        public string raceGroup; //keeping this for backwards compatibility
        public List<string> raceGroups; //racegroup, but in list form so multiple can be entered, preference to use this over racegroup 
        public List<string> raceNames;
        public List<string> pawnKindNames;
        public List<BestialityGeneInheritanceDef> genes;
        public string hybridName;
    }
}
