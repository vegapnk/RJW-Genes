using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJW_Genes
{
    public class VGEHybridOffspringDefs : Verse.Def
    {
        public List<Verse.PawnKindDef> SupportedParentKindDefs;
        public List<Verse.PawnKindDef> PossibleHybridChildKindDefs;
    }
}
