using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    public class GenitaliaFluidChangeExtension : DefModExtension
    {
        public SexFluidDef penisFluidReplacement;   // Everything for which "isPenis" is true
        public SexFluidDef vaginaFluidReplacement;  // Everything for which "isVagina" is true
        public SexFluidDef breastFluidReplacement;  // Everything for which "isBreasts" is true
        public SexFluidDef otherFluidReplacement;   // Everything not the above.
    }

}
