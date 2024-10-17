using Cumpilation.Oscillation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    public class HediffCompProperties_MatchSeverityOfHediff : HediffCompProperties
    {
        public HediffDef hediffToMatch;

        public HediffCompProperties_MatchSeverityOfHediff() => this.compClass = typeof(HediffComp_MatchSeverityOfHediff);
    }
}
