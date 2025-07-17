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
        /// 
        /// There was an issue with Pawn Generation and this has been Reworked - Please see #116 for more documentation. 
        /// The current state of affairs is that hybrids are generated using RJW-Pregnancy and "switching" the Fathers KindDef only for Child Generation, before switching it back. 
        /// It's not easy to just change the Babies kindDef, because RJW-Preg runs a PawnGeneration Request. 
        /// Thus, if you just change that from Husky to Dogman, most of the things are still Husky and you get a lot of red errors after birth.  
        /// 
        /// Relevant RJW Files:
        /// 
        /// - Hediff_BestialPregnancy https://gitgud.io/Ed86/rjw/-/blob/master/1.5/Source/Modules/Pregnancy/Hediffs/Hediff_BestialPregnancy.cs?ref_type=heads
        /// - Hediff_BasePregnancy https://gitgud.io/Ed86/rjw/-/blob/master/1.5/Source/Modules/Pregnancy/Hediffs/Hediff_BasePregnancy.cs?ref_type=heads
        /// </summary>
        [HarmonyPrefix]
        [HarmonyPatch("AddPregnancyHediff")]
        public static bool AddPregnancyHediffPrefix(Pawn mother, Pawn father)
        {
        
            // Error & Setting HandlingHandling, "true" means the normal method is run (and nothing else from this patch).
            // Behaviour of Harmony Prefixes: https://harmony.pardeike.net/articles/patching-prefix.html
            if (!RJW_BGSSettings.rjw_bgs_VE_genetics) return true;
            if (mother == null || father == null) return true;

            RJW_Genes.ModLog.Debug("Trying to add RJW Pregnancy Hediff - Checking for potential VGE Animal-Hybridization");

            bool humanMotherAndSupportedAnimal = xxx.is_human(mother) && VGEHybridUtility.SupportedInitialAnimalRaces.Contains(father.kindDef);
            bool humanMotherAndSupportedHybrid = xxx.is_human(mother) && VGEHybridUtility.SupportedHybridRaces.Contains(father.kindDef);
            bool humanFatherAndSupportedAnimal = xxx.is_human(father) && VGEHybridUtility.SupportedInitialAnimalRaces.Contains(mother.kindDef);
            bool humanFatherAndSupportedHybrid = xxx.is_human(father) && VGEHybridUtility.SupportedHybridRaces.Contains(mother.kindDef);

            // Exit if there are no supported parents / nothing to do for my logic
            if (!(humanMotherAndSupportedAnimal || humanMotherAndSupportedHybrid || humanFatherAndSupportedAnimal || humanFatherAndSupportedHybrid))
            {
                RJW_Genes.ModLog.Debug("Aborting VGE-Hybdrization Pregnancy - Parents were unsupported RaceKinds");
                return true;
            }
            // Exit by chance
            if((new Random()).NextDouble() > RJW_BGSSettings.rjw_bgs_ve_genetics_chance)
            {
                RJW_Genes.ModLog.Debug($"VGE-Hybridization chance ({Math.Round(RJW_BGSSettings.rjw_bgs_ve_genetics_chance,3)*100}%) was not met - continuing with normal pregnancy behaviour.");
                return true;
            }

            if (humanMotherAndSupportedAnimal)
            {
                RJW_Genes.ModLog.Debug("Found a human mother and a supported animal resulting in an animal-child - starting VGE pregnancy (rjw.Hediff_BestialPregnancy)");
                Hediff_BasePregnancy.Create<Hediff_BestialPregnancy>(mother, father, DnaGivingParent.Father);

                var kindDef = VGEHybridUtility.LookupPossiblyOffspringHybrid(father.kindDef);
                var stored = father.kindDef;
                father.kindDef = kindDef;
                Hediff_BasePregnancy preg = Hediff_BasePregnancy.Create<Hediff_BestialPregnancy>(mother, father, DnaGivingParent.Father);
                father.kindDef = stored;

                // "false" means the normal method is not run
                return false;
            }
            else if (humanMotherAndSupportedHybrid)
            {

                RJW_Genes.ModLog.Debug("Found a human mother and a hybrid - this behaviour has been disabled from 2.2.1 onward - sorry :(");
                return true;
            }
            else if (humanFatherAndSupportedAnimal)
            {
                RJW_Genes.ModLog.Debug("Found a human father and a supported animal resulting in an animal-child - starting VGE pregnancy (rjw.Hediff_BestialPregnancy)");

                var kindDef = VGEHybridUtility.LookupPossiblyOffspringHybrid(mother.kindDef);
                var stored = mother.kindDef;
                mother.kindDef = kindDef;
                Hediff_BasePregnancy preg = Hediff_BasePregnancy.Create<Hediff_BestialPregnancy>(mother, father, DnaGivingParent.Mother);
                mother.kindDef = stored;
                
                return false;
            }
            else if (humanFatherAndSupportedHybrid)
            {
                RJW_Genes.ModLog.Debug("Found a human father and a hybrid - this behaviour has been disabled from 2.2.1 onward - sorry :(");
                return true;
            }

            RJW_Genes.ModLog.Debug("Issues in applying the Patch for VGE hybdritization - doing nothing and continuing with normal pregnancy.");
            return true;
        }
    }
    
}
