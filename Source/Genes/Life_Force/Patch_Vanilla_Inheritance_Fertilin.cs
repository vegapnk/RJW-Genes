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
    /// </summary>
    [HarmonyPatch(typeof(PregnancyUtility), "GetInheritedGeneSet", new Type[]
    {
        typeof(Pawn),
        typeof(Pawn),
        //typeof(bool)
    }
    )]
    public static class PatchVanillaPregnancyFertilin
    {
        [HarmonyPostfix]
        public static void InheritedGenes(Pawn father, Pawn mother, ref GeneSet __result)
        {
            //Also make a setting for this
            if (__result.GenesListForReading.Contains(GeneDefOf.rjw_genes_lifeforce))
            {
                List<GeneDef> gene_list = __result.GenesListForReading;

                //If no absorption gene get one from the parents, else a random one
                if(!(gene_list.Contains(GeneDefOf.rjw_genes_drainer) || gene_list.Contains(GeneDefOf.rjw_genes_cum_eater) 
                    || gene_list.Contains(GeneDefOf.rjw_genes_vaginal_absorber) || gene_list.Contains(GeneDefOf.rjw_genes_anal_absorber)))
                {
                    List<GeneDef> absorption_genes_list = new List<GeneDef> { GeneDefOf.rjw_genes_drainer, GeneDefOf.rjw_genes_cum_eater
                        , GeneDefOf.rjw_genes_vaginal_absorber, GeneDefOf.rjw_genes_anal_absorber };
                    List<GeneDef> absorption_genes_parents = new List<GeneDef>();
                    foreach (GeneDef geneDef in absorption_genes_list)
                    {
                        if(mother.genes != null && mother.genes.HasGene(geneDef))
                        {
                            absorption_genes_parents.Add(geneDef);
                        }
                        if (father.genes != null && father.genes.HasGene(geneDef))
                        {
                            absorption_genes_parents.Add(geneDef);
                        }
                    }
                    if (!absorption_genes_parents.NullOrEmpty())
                    {
                        __result.AddGene(absorption_genes_parents.RandomElement());
                    }
                    else
                    {
                        __result.AddGene(absorption_genes_list.RandomElement());
                    }
                }
            }       
        }
    }
}
