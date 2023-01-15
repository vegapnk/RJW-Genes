using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace RJW_BGS
{
    /// <summary>
    /// This Patch is applied to change the normal pregnancy to add animal-inheritance. 
    /// If the settings allow animal gene inheritance, 
    /// the genes are determined and "simply added". 
    /// </summary>
    [HarmonyPatch(typeof(PregnancyUtility), "GetInheritedGeneSet", new Type[] 
    { 
        typeof(Pawn), 
        typeof(Pawn),
        //typeof(bool)
    }
    )]
    public static class PatchVanillaPregnancyUtility
    {
        [HarmonyPostfix]
        public static void AnimalInheritedGenes(Pawn father, Pawn mother, ref GeneSet __result)
        {
            if (!RJW_BGSSettings.rjw_bgs_enabled)
            {
                return;
            }
            List<GeneDef> genes = InheritanceUtility.AnimalInheritedGenes(father, mother);
            if (genes.Any())
            {
                foreach (GeneDef gene in genes)
                {
                    __result.AddGene(gene);
                }
            }        
        }
    }
}
