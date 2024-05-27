using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace RJW_Genes
{
    /// <summary>
    /// This Patch is applied to add a absorption gene for fertilin if it has none, but it does have the fertilin gene
    /// First tries to get one from the parents else chooses one of them at random
    /// the genes are determined and "simply added". 
    /// 
    /// This fixes the potential problem that Pawns could inherit Fertilin, but no gene to gain Fertilin.
    /// </summary>
    [HarmonyPatch(typeof(PregnancyUtility), "GetInheritedGeneSet", new Type[]
    {
        typeof(Pawn),
        typeof(Pawn)
    }
    )]
    public static class Patch_Vanilla_Inheritance_Fertilin
    {
        [HarmonyPostfix]
        public static void InheritedGenes(Pawn father, Pawn mother, ref GeneSet __result)
        {
            //Also make a setting for this
            if (__result.GenesListForReading.Contains(GeneDefOf.rjw_genes_lifeforce))
            {
                List<GeneDef> babies_genes = __result.GenesListForReading;

                //If there is no absorption gene get one from the parents, else a random one
                if(!Has_Fertilin_Source_Gene(babies_genes))
                {
                    if (RJW_Genes_Settings.rjw_genes_detailed_debug)
                        ModLog.Message($"Child of ({father.Name};{mother.Name}) has Genes with LifeForce-Resource but no Source-Gene, adding one of parents random if possible or any random otherwise.");
                    // Gather Parents Source-Genes
                    List<GeneDef> absorption_genes_parents = new List<GeneDef>();
                    foreach (GeneDef geneDef in FertilinSourceGenes)
                    {
                        if(mother.genes != null && mother.genes.HasActiveGene(geneDef))
                            absorption_genes_parents.Add(geneDef);

                        if (father.genes != null && father.genes.HasActiveGene(geneDef))
                            absorption_genes_parents.Add(geneDef);
                    }
                    // Parents had Genes - Pick a random one of them 
                    if (!absorption_genes_parents.NullOrEmpty())
                        __result.AddGene(absorption_genes_parents.RandomElement());
                    // Create a fully random one for your little Cumfueled missbreed
                    else
                        __result.AddGene(FertilinSourceGenes.RandomElement());
                }
            }       
        }

        private static List<GeneDef> FertilinSourceGenes = new List<GeneDef>() {
            GeneDefOf.rjw_genes_drainer, 
            GeneDefOf.rjw_genes_cum_eater, 
            GeneDefOf.rjw_genes_fertilin_absorber,
            GeneDefOf.rjw_genes_cockeater 
        };

        private static bool Has_Fertilin_Source_Gene(List<GeneDef> genes)
        {
            foreach (GeneDef gene in genes)
            {
                if (FertilinSourceGenes.Contains(gene))
                {
                    return true;
                }
            }
            return false;
        }


    }
}
