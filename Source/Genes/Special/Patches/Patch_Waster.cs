using HarmonyLib;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using VanillaRacesExpandedWaster;

namespace RJW_Genes
{


    public static class Patch_Waster
    {
        public static void Gene_Randomizer_Prefix(Gene_Randomizer __instance)
        {
            if (Patch_OrgasmMytosis.mytosis_mutation != null)
            {
                if (!__instance.mutationGenes.Contains(GeneDefOf.rjw_genes_mytosis_mutation))
                {
                    __instance.mutationGenes.Add(GeneDefOf.rjw_genes_mytosis_mutation);
                }
                if (!__instance.mutationGenes.Contains(GeneDefOf.rjw_genes_Necro_genitalia))
                {
                    __instance.mutationGenes.Add(GeneDefOf.rjw_genes_Necro_genitalia);
                }
                if (!__instance.mutationGenes.Contains(GeneDefOf.rjw_genes_Tentacle_genitalia))
                {
                    __instance.mutationGenes.Add(GeneDefOf.rjw_genes_Tentacle_genitalia);
                }
            }
        }
        public static GeneDef VRE_GauntBody = DefDatabase<GeneDef>.GetNamed("VRE_GauntBody", false);
        public static void SatisfiesTeratophile_Postfix(ref bool __result, Pawn partner)
        {
            if (!__result)
            {
                ModLog.Message("check 1");
                if (VRE_GauntBody != null)
                {
                    ModLog.Message("check 2");
                    if (partner.genes.HasActiveGene(VRE_GauntBody))
                    {
                        ModLog.Message("check 3");
                        __result = true;
                        return;
                    }
                }


            }
        }




    }

}
