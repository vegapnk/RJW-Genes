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
            bool a = mother.IsHuman() && BasePregnancyPatcher.racesgen0.Contains(father.kindDef.race.defName);
            bool b = mother.IsHuman() && BasePregnancyPatcher.racesgen1.Contains(father.kindDef.race.defName);
            bool c = father.IsHuman() && BasePregnancyPatcher.racesgen0.Contains(mother.kindDef.race.defName);
            bool d = father.IsHuman() && BasePregnancyPatcher.racesgen1.Contains(mother.kindDef.race.defName);

            if (!(a || b||c|| d)) return true;
            if (a)
            {
                Hediff_BasePregnancy.Create<Hediff_BestialPregnancy>(mother, father, DnaGivingParent.Father);
                return false;
            }
            else if (b)
            {
                ModLog.Message("preg hediffdefof PregnantHuman " + RimWorld.HediffDefOf.PregnantHuman);
                PregnancyHelper.StartVanillaPregnancy(mother, father);
                return false;
            }
            else if (c)
            {
                Hediff_BasePregnancy.Create<Hediff_BestialPregnancy>(mother, father, DnaGivingParent.Mother);
                return false;
            }
            else if (d)
            {
                Hediff_BasePregnancy.Create<Hediff_BestialPregnancy>(mother, father, DnaGivingParent.Father);
                return false;
            }
            return true;
        }

    }
}
