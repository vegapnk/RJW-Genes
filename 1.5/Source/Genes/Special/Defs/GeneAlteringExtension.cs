using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    public class GeneAlteringExtension : DefModExtension
    {
        public List<GeneDef> minorGenes;
        public List<GeneDef> majorGenes;

        public float minorApplicationChance;
        public float majorApplicationChance;
    }
}
