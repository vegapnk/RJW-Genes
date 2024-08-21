using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RJW_BGS;
using HarmonyLib;
using rjw;
using static rjw.Hediff_BasePregnancy;
using RJW_Genes;

namespace RJW_BGS
{

[HarmonyPatch(typeof(PregnancyHelper))]
public class Patch_RJW_PregnancyHelper_VanillaExpandedGenetics
{

        /// <summary>
        /// This Patch changes the pregnancy logic to check for possible hybridization. 
        /// Iff the hybridization applies, this prefix skips the normal AddPregnancyHediff (by returning false). 
        ///
        /// Small Note: Below we use `Hediff_BasePregnancy.Create<Hediff_BestialPregnancy>(mother, father, DnaGivingParent.Mother);`
        /// This completely creates the pregnancy, it does not need to be assigned to anything or added to some hediffs.
        /// </summary>
        [HarmonyPrefix]
        [HarmonyPatch("AddPregnancyHediff")]
        public static bool AddPregnancyHediffPrefix(Pawn mother, Pawn father)
        {
        
            // Error & Setting HandlingHandling, "true" means the normal method is run (and nothing else from this patch).
            // Behaviour of Harmony Prefixes: https://harmony.pardeike.net/articles/patching-prefix.html
            if (!RJW_BGSSettings.rjw_bgs_VE_genetics)
            {
                //RJW_Genes.ModLog.Debug("Started VGE Pregnancy Patch - but settings are off so going into Vanilla");
                return true;
            }
            if (mother == null || father == null) return true;

            RJW_Genes.ModLog.Debug("Trying to add RJW Pregnancy Hediff - Checking for potential VGE Animal-Hybridization");

            bool humanMotherAndSupportedAnimal = mother.IsHuman() && VGEHybridUtility.supportedInitialAnimalRaces.Contains(father.kindDef.race.defName);
            bool humanMotherAndSupportedHybrid = mother.IsHuman() && VGEHybridUtility.supportedHybridRaces.Contains(father.kindDef.race.defName);
            bool humanFatherAndSupportedAnimal = father.IsHuman() && VGEHybridUtility.supportedInitialAnimalRaces.Contains(mother.kindDef.race.defName);
            bool humanFatherAndSupportedHybrid = father.IsHuman() && VGEHybridUtility.supportedHybridRaces.Contains(mother.kindDef.race.defName);

            if (!(humanMotherAndSupportedAnimal || humanMotherAndSupportedHybrid || humanFatherAndSupportedAnimal || humanFatherAndSupportedHybrid))
            {
                RJW_Genes.ModLog.Debug("Aborting VGE-Hybdrization Pregnancy - Parents were unsupported RaceKinds");
                return true;
            }

            if (humanMotherAndSupportedAnimal)
            {
                RJW_Genes.ModLog.Debug("Found a human mother and a supported animal resulting in an animal-child - starting VGE pregnancy (rjw.Hediff_BestialPregnancy)");
                Hediff_BasePregnancy.Create<Hediff_BestialPregnancy>(mother, father, DnaGivingParent.Father);
                // "false" means the normal method is not run
                return false;
            }
            else if (humanMotherAndSupportedHybrid)
            {
                RJW_Genes.ModLog.Debug("Found a human mother and a supported hybrid resulting in an human-child - starting VGE pregnancy (Biotech Pregnancy)");

                PregnancyHelper.StartVanillaPregnancy(mother, father);
                return false;
            }
            else if (humanFatherAndSupportedAnimal)
            {
                RJW_Genes.ModLog.Debug("Found a human father and a supported animal resulting in an animal-child - starting VGE pregnancy (rjw.Hediff_BestialPregnancy)");
                Hediff_BasePregnancy.Create<Hediff_BestialPregnancy>(mother, father, DnaGivingParent.Mother);
                return false;
            }
            else if (humanFatherAndSupportedHybrid)
            {
                RJW_Genes.ModLog.Debug("Found a human father and a supported hybrid resulting in an animal-child - starting VGE pregnancy (rjw.Hediff_BestialPregnancy)");
                Hediff_BasePregnancy.Create<Hediff_BestialPregnancy>(mother, father, DnaGivingParent.Father);
                return false;
            }

            RJW_Genes.ModLog.Debug("Issues in applying the Patch for VGE hybdritization - doing nothing and continuing with normal pregnancy.");
            return true;
        }
    }
    
}
