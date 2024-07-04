using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    public class ImmunityAgainstGenesExtension : DefModExtension
    {
        /// <summary>
        /// A list of the exact defnames of disease-genes that this extension will make immune against.
        /// Must perfectly match!
        /// </summary>
        public List<string> givesImmunityAgainst;
    }

}
