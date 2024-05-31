using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJW_Genes
{
    public class CompProperties_AbilityMatingCall : CompProperties_AbilityEffect
    {

        public int calldistance;

        public CompProperties_AbilityMatingCall()
        {
            this.compClass = typeof(CompAbilityEffect_MatingCall);
        }
    }
}
