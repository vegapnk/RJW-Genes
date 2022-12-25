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
            if (!RJW_BGSSettings.enabled)
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
