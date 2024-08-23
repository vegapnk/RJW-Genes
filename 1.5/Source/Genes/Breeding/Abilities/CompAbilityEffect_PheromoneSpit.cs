using RimWorld;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace RJW_Genes
{
    public class CompAbilityEffect_PheromoneSpit : CompAbilityEffect
    {
        private new CompProperties_AbilityPheromoneSpit Props
        {
            get
            {
                return (CompProperties_AbilityPheromoneSpit)this.props;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            AnimalBreedingHelper.DoAnimalBreedingPulse(target.Pawn, Props.calldistance);
        }

    }
}
