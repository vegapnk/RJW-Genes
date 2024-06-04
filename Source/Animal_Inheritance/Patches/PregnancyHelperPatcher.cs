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

namespace RJW_BGS
{
    [HarmonyPatch(typeof(PregnancyHelper))]
    public class PregnancyHelperPatcher
    {

        [HarmonyPrefix]
        [HarmonyPatch("AddPregnancyHediff")]
        public static bool AddPregnancyHediffPrefix(Pawn mother, Pawn father)
        {
            if (!RJW_BGSSettings.rjw_bgs_VE_genetics) return true;
            if (mother == null || father == null) return true;
            bool humanMotherAndSupportedAnimal = mother.IsHuman() && BasePregnancyPatcher.supportedInitialAnimalRaces.Contains(father.kindDef.race.defName);
            bool humanMotherAndSupportedHybrid = mother.IsHuman() && BasePregnancyPatcher.supportedHybridRaces.Contains(father.kindDef.race.defName);
            bool humanFatherAndSupportedAnimal = father.IsHuman() && BasePregnancyPatcher.supportedInitialAnimalRaces.Contains(mother.kindDef.race.defName);
            bool humanFatherAndSupportedHybrid = father.IsHuman() && BasePregnancyPatcher.supportedHybridRaces.Contains(mother.kindDef.race.defName);

            if (!(humanMotherAndSupportedAnimal || humanMotherAndSupportedHybrid||humanFatherAndSupportedAnimal|| humanFatherAndSupportedHybrid)) return true;
            if (humanMotherAndSupportedAnimal)
            {
                Hediff_BasePregnancy.Create<Hediff_BestialPregnancy>(mother, father, DnaGivingParent.Father);
                return false;
            }
            else if (humanMotherAndSupportedHybrid)
            {
                ModLog.Message("preg hediffdefof PregnantHuman " + RimWorld.HediffDefOf.PregnantHuman);
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
