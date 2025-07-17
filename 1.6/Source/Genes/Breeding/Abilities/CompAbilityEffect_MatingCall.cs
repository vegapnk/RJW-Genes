using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse.Sound;
using Verse;
using RimWorld.Planet;
using rjw;
using HarmonyLib;
using Verse.AI;

namespace RJW_Genes
{
    public class CompAbilityEffect_MatingCall : CompAbilityEffect
    {

        private new CompProperties_AbilityMatingCall Props
        {
            get
            {
                return (CompProperties_AbilityMatingCall)this.props;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            ModLog.Message($"{this.parent.pawn} is casting MatingCall");
            AnimalBreedingHelper.DoAnimalBreedingPulse(this.parent.pawn, Props.calldistance);
        }
        
    }
}
