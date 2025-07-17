using RimWorld;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse.AI;
using Verse;

namespace RJW_Genes
{
    public class AnimalBreedingHelper
    {

        /// <summary>
        /// Finds animals in a distance around a pawn, and schedules a breeding job. 
        /// This is done regardless of the animals genitalia at the moment.
        /// This function has no checks if the Pawn is hostile, downed, etc., such checks must be done upstream!
        /// </summary>
        /// <param name="toBeBred">The pawn that will be target of breeding animals</param>
        /// <param name="pulse_distance">The range around the pawn for which animals will be triggered.</param>
        public static void DoAnimalBreedingPulse(Pawn toBeBred, int pulse_distance, bool ends_manhunter = true)
        {
            IEnumerable<Pawn> animals = GetAnimalsInRange(toBeBred.Map, toBeBred.Position, pulse_distance);
            int breeder_counter = 0;

            foreach (Pawn animal in animals)
            {
                if (ends_manhunter)
                    EndManHunter(animal);

                if (!RJW_Genes_Settings.animalMatingPulseCheckForGenitals || rjw.xxx.can_rape(animal))
                {
                    ForceBreedingJob(toBeBred, animal);
                    breeder_counter++;
                }
               
            }
            ModLog.Message($"{breeder_counter} of {animals.Count()} Animals in range are trying to breed {toBeBred}");
        }

        private static IEnumerable<Pawn> GetAnimalsInRange(Map map, IntVec3 position, int distance)
        {
            IEnumerable<Pawn> animals = 
                map.mapPawns
                    .AllPawnsSpawned
                    .Where<Pawn>((Func<Pawn, bool>)(p => 
                        //1.6 Fix
                        //p.IsNonMutantAnimal 
                        p.IsAnimal
                        && !p.IsMutant
                        && p.Position.InHorDistOf(position, distance)
                        && xxx.is_healthy_enough(p))
                    );

            return animals;
        }

        private static void ForceBreedingJob(Pawn toBeBred, Pawn animal)
        {
            // Stopping all Jobs in this way is a bit heavy - but as it's only about Animals this should be fine. 
            animal.jobs.CaptureAndClearJobQueue();
            animal.jobs.StopAll();
            Job job = JobMaker.MakeJob(xxx.animalBreed, toBeBred);
            animal.jobs.TryTakeOrderedJob(job);
        }

        private static void EndManHunter(Pawn animal)
        {
            if (animal.MentalState != null && (animal.MentalState.def == MentalStateDefOf.Manhunter || animal.MentalState.def == MentalStateDefOf.ManhunterPermanent))
            {
                animal?.MentalState?.RecoverFromState();
            }

        }
    }
}
