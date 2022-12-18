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

        public String raceGroup;
        public List<string> raceNames;
        public List<string> pawnKindNames;
        public List<GeneChance> genes;
        //public List<float> genechances;
        public String hybridName;
    }
}
