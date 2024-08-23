using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using rjw;
using rjw.Modules.Interactions.Defs.DefFragment;
using rjw.Modules.Interactions.Enums;

namespace RJW_Genes
{
    public class CompProperties_SexInteractionRequirements : AbilityCompProperties
    {
        public CompProperties_SexInteractionRequirements()
        {
            this.compClass = typeof(CompAbility_SexInteractionRequirements);
        }

        public List<InteractionTag> tags = new List<InteractionTag>();
        public InteractionRequirement dominantRequirement;
        public InteractionRequirement submissiveRequirement;
    }
}
