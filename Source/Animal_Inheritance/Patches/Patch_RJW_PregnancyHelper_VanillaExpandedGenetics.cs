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
        /// Iff the hybrdiization applies, this prefix skips the normal AddPregnancyHediff (by returning false). 
        ///
        /// Small Note: Below we use `Hediff_BasePregnancy.Create<Hediff_BestialPregnancy>(mother, father, DnaGivingParent.Mother);`
        /// This completely creates the pregnancy, it does not need to be assigned to anything or added to some hediffs. 
        /// </summary>
        [HarmonyPrefix]
        [HarmonyPatch("AddPregnancyHediff")]
        public static bool AddPregnancyHediffPrefix(Pawn mother, Pawn father)
        {
            if (!RJW_BGSSettings.rjw_bgs_VE_genetics) return true;
            if (mother == null || father == null) return true;
            bool humanMotherAndSupportedAnimal = mother.IsHuman() && Patch_RJW_BasePregnancy_VanillaExpandedGenetics.supportedInitialAnimalRaces.Contains(father.kindDef.race.defName);
            bool humanMotherAndSupportedHybrid = mother.IsHuman() && Patch_RJW_BasePregnancy_VanillaExpandedGenetics.supportedHybridRaces.Contains(father.kindDef.race.defName);
            bool humanFatherAndSupportedAnimal = father.IsHuman() && Patch_RJW_BasePregnancy_VanillaExpandedGenetics.supportedInitialAnimalRaces.Contains(mother.kindDef.race.defName);
            bool humanFatherAndSupportedHybrid = father.IsHuman() && Patch_RJW_BasePregnancy_VanillaExpandedGenetics.supportedHybridRaces.Contains(mother.kindDef.race.defName);

            if (!(humanMotherAndSupportedAnimal || humanMotherAndSupportedHybrid||humanFatherAndSupportedAnimal|| humanFatherAndSupportedHybrid)) return true;
            if (humanMotherAndSupportedAnimal)
            {
                Hediff_BasePregnancy.Create<Hediff_BestialPregnancy>(mother, father, DnaGivingParent.Father);
                return false;
            }
            else if (humanMotherAndSupportedHybrid)
            {
                if (RJW_Genes_Settings.rjw_genes_detailed_debug)
                    RJW_Genes.ModLog.Message("preg hediffdefof PregnantHuman " + RimWorld.HediffDefOf.PregnantHuman);
                
                PregnancyHelper.StartVanillaPregnancy(mother, father);
                return false;
            }
            else if (humanFatherAndSupportedAnimal)
            {
                Hediff_BasePregnancy.Create<Hediff_BestialPregnancy>(mother, father, DnaGivingParent.Mother);
                return false;
            }
            else if (humanFatherAndSupportedHybrid)
            {
                Hediff_BasePregnancy.Create<Hediff_BestialPregnancy>(mother, father, DnaGivingParent.Father);
                return false;
            }
            return true;
        }

    }
}
