using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    public class HediffCompProperties_DiseaseStorage : HediffCompProperties
    {
        // 300k = 5 days.
        public int ticksThatDiseasesAreStored = 300000;
        public HediffCompProperties_DiseaseStorage() => this.compClass = typeof(HediffComp_DiseaseStorage);
    }

}
