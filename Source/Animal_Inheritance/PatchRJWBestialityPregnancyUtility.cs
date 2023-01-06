using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using Verse;
using rjw;

namespace RJW_BGS
{
    [HarmonyPatch(typeof(Hediff_BasePregnancy), "Initialize")]
    public static class PatchRJWBestialityPregnancyUtility
    {
        [HarmonyPostfix]
        public static void AddGenes(Pawn mother, Pawn dad, ref Hediff_BasePregnancy __instance)
        {
            foreach (Pawn baby in __instance.babies)
            {
                if (baby.RaceProps.Humanlike)
                {
                    if (baby.genes == null)
                    {
                        baby.genes = new Pawn_GeneTracker(baby);
                    }


                    //Remove the hair and skin genes pawns always start with, should get correct ones from human parent anyway.
                    for (int i = baby.genes.Endogenes.Count - 1; i >= 0; i--)
                    {
                        baby.genes.RemoveGene(baby.genes.Endogenes[i]);
                    }

                    List<GeneDef> humangenes = PregnancyUtility.GetInheritedGenes(dad, mother);
                    List<GeneDef> beastgenes = InheritanceUtility.AnimalInheritedGenes(dad, mother);
                    InheritanceUtility.AddGenes(baby, beastgenes);
                    InheritanceUtility.AddGenes(baby, humangenes);

                    // The mix-breed babies should be labelled hybrids
                    baby.genes.hybrid = true;
                    baby.genes.xenotypeName = "Hybrid";
                }
            }
        }
    }
}
