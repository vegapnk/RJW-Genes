using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    public class HediffIncreaseOnSexExtension: DefModExtension
    {
        public HediffDef hediffDef;
        public float severityIncrease;
        public float applicationChance;

        public bool canCreateHediff;

        public bool applicableForWomen;
        public bool applicableForMen;
        public bool requiresPenetrativeSex;
    }

}
