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
        public String raceGroup;
        public List<string> raceNames;
        public List<string> pawnKindNames;
        public List<BestialityGeneInheritanceDef> genes;
        //public List<float> genechances;
        public String hybridName;
    }
}
