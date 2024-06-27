using RimWorld;
using rjw;
using Verse;
using System.Linq;
using System.Collections.Generic;
using System;
using HarmonyLib;
using RJWLoveFeeding;


namespace RJWLoveFeeding
{
    [HarmonyPatch(typeof(PawnExtensions), "IsPregnant", new Type[]
{
        typeof(Pawn), typeof(bool)
    })]
    class MultiplePregnancies
    {
        static Def MultiPregnancy = DefDatabase<GeneDef>.GetNamed("RS_MultiPregnancy", false);
        [HarmonyPostfix]
        public static void Postfix(ref bool __result, Pawn pawn, bool mustBeVisible)
        {
            bool isPregnant = __result;
            if (MultiPregnancy != null)
            {
                if (RJWSettings.DevMode) RJW_Genes.ModLog.Message("multipreg checks");
                if (isPregnant)
                {
                    try
                    {
                        isPregnant = MultiplePregnancies.RJWMultiplePregnancy(isPregnant, pawn);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.ToString());
                    }
                }
                __result = isPregnant;
            }
        }


        public static bool RJWMultiplePregnancy(bool isPregnant, Pawn fucked)
        {
            if ((fucked != null) && !xxx.is_animal(fucked))
            {


                List<Hediff> set = fucked.health.hediffSet.hediffs;

                //Taking all hediffs that prevent pregnancy but are are not of the type used for pregnancies itself
                List<Hediff> setNoPreggo = set.FindAll(o => (o.def.preventsPregnancy) && !(o is HediffWithParents));

                if (setNoPreggo.NullOrEmpty())
                {
                    Pawn_GeneTracker genes = fucked.genes;
                    if (genes.HasActiveGene(RJW_Genes.GeneDefOf.RS_MultiPregnancy))
                    {
                        Log.Message(xxx.get_pawnname(fucked) + " has multipregnancy gene");
                        return false;
                    }
                }
                else
                {
                    Log.Message(setNoPreggo.First<Hediff>().def.defName + ": This prevents pregnancy");
                }
            }

            return isPregnant;
        }
    }
}
