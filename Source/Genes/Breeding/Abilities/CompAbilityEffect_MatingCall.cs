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

        bool fired = false; 
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
            DoAnimalBreedingPulse();
        }

        private void DoAnimalBreedingPulse()
        {
            if (fired) { return; }

            IEnumerable<Pawn> animals = this.parent.pawn.Map.mapPawns.AllPawnsSpawned.Where<Pawn>((Func<Pawn, bool>)(p => p.IsNonMutantAnimal && p.Position.InHorDistOf(this.parent.pawn.Position, Props.calldistance)));
            int breeder_counter = 0;

            foreach (Pawn animal in animals)
            {   
                if (animal.MentalState != null && (animal.MentalState.def == MentalStateDefOf.Manhunter || animal.MentalState.def == MentalStateDefOf.ManhunterPermanent))
                {
                    Log.Warning("Found an angry Animal to Fuck");
                    animal?.MentalState?.RecoverFromState();
                }

                if(xxx.is_healthy_enough(animal))
                {
                    // Stopping all Jobs in this way is a bit heavy - but as it's only about Animals this should be fine. 
                    animal.jobs.CaptureAndClearJobQueue();
                    animal.jobs.StopAll();
                    Job job = JobMaker.MakeJob(xxx.animalBreed, this.parent.pawn);
                    animal.jobs.TryTakeOrderedJob(job);
                    breeder_counter++;
                }
            }
            ModLog.Message($"{breeder_counter} of {animals.Count()} Animals in range are trying to breed {this.parent.pawn}");
            fired = true;
        }

    }
}
