using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace RJW_Genes
{
    public class CompProperties_AbilityCocoonWeaver : CompProperties_AbilityEffect
    {
        public CompProperties_AbilityCocoonWeaver()
        {
            this.compClass = typeof(CompAbilityEffect_CocoonWeaver);
        }
    }
}