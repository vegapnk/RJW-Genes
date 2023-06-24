using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Verse;
using rjw;

namespace RJW_Genes
{
    /// <summary>
    /// This Class patches the AfterSexUtility to also fertilize eggs if Pawn A has "InsectBreeder" and Pawn B has Insect Eggs.
    /// Patched Class is https://gitgud.io/Ed86/rjw/-/blob/master/1.4/Source/Common/Helpers/SexUtility.cs
    /// 
    /// Normal Egg-Pregnancy logic is in https://gitgud.io/Ed86/rjw/-/blob/master/1.4/Source/Modules/Pregnancy/Pregnancy_Helper.cs
    /// Gene: rjw_genes_insectbreeder 
    /// </summary>
    [HarmonyPatch(typeof(SexUtility), "Aftersex")]
    static class Patch_InsectBreeder_EggFertilization
    {
        public static void Postfix(SexProps props)
        {
            // Only Fertilize on vaginal / anal sex
            if (!(props.sexType == xxx.rjwSextype.Vaginal || props.sexType == xxx.rjwSextype.Anal))
            {
                return;
            }


            if (canDoEggFertilization(props.pawn, props.partner))
            {
                // Pawn has gene and Partner has eggs
                if (props.pawn.genes.GenesListForReading.Any(x => x.def == GeneDefOf.rjw_genes_insectbreeder) && !getEggsforPawn(props.partner).NullOrEmpty())
                {
                    Pawn eggHolder = props.partner;
                    Pawn impregnator = props.pawn;

                    foreach (Hediff_InsectEgg egg in getEggsforPawn(eggHolder))
                    {
                        if (!egg.fertilized)
                            egg.Fertilize(impregnator);
                    }
                }

                // Partner has gene and Pawn has eggs
                if (props.partner.genes.GenesListForReading.Any(x => x.def == GeneDefOf.rjw_genes_insectbreeder) && !getEggsforPawn(props.pawn).NullOrEmpty())
                {
                    Pawn eggHolder = props.pawn;
                    Pawn impregnator = props.partner;

                    foreach (Hediff_InsectEgg egg in getEggsforPawn(eggHolder))
                    {
                        if (!egg.fertilized)
                            egg.Fertilize(impregnator);
                    }
                }


            }
        }


        private static bool canDoEggFertilization(Pawn a, Pawn b)
        {

            // No Partner / Other Errors
            if (a != null || b != null)
                return false;
            // None of the pawns has the relevant gene
            if (!a.genes.GenesListForReading.Any(x => x.def == GeneDefOf.rjw_genes_insectbreeder) && !b.genes.GenesListForReading.Any(x => x.def == GeneDefOf.rjw_genes_insectbreeder))
                return false;
            // None of the pawns has eggs
            if (getEggsforPawn(a).NullOrEmpty() && getEggsforPawn(b).NullOrEmpty())
                return false;

            // A has gene and B has eggs
            if (a.genes.GenesListForReading.Any(x => x.def == GeneDefOf.rjw_genes_insectbreeder) && !getEggsforPawn(b).NullOrEmpty())
            {
                return true;
            }
            // B has gene and A has eggs
            if (b.genes.GenesListForReading.Any(x => x.def == GeneDefOf.rjw_genes_insectbreeder) && !getEggsforPawn(a).NullOrEmpty())
            {
                return true;
            }
            // Any other case: Do nothing
            return false;
        }

        private static List<Hediff_InsectEgg> getEggsforPawn(Pawn pawn)
        {
            List<Hediff_InsectEgg> eggs = new List<Hediff_InsectEgg>();
            pawn.health.hediffSet.GetHediffs(ref eggs);
            foreach (var egg in eggs)
                egg.Fertilize(pawn);

            return eggs;
        }
    }
}
