using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    internal static class Patch_RJWSexualizer
    {
        /// <summary>
        /// This Harmony patch is called after Sexualiser, and runs all the genes on that pawn that would have not been functional before sexualization.
        /// </summary>
        public static void ApplyGenesPostfix(Pawn pawn) 
        {
            //Generate a sorted list of Genes that need to be processed.
            if (pawn == null) return;
            Dictionary<int,List<Gene>> geneDict = new Dictionary<int,List<Gene>>();
            if (pawn.genes == null) return;
            foreach (Gene curGene in pawn.genes.GenesListForReading)
            {
                
                if (!curGene.def.HasModExtension<SexualizerGeneExtension>()) continue;

                SexualizerGeneExtension geneExtension = curGene.def.GetModExtension<SexualizerGeneExtension>();
                if (geneDict.ContainsKey(geneExtension.order))
                {
                    geneDict[geneExtension.order].Add(curGene);
                }
                else
                {
                    geneDict.Add(geneExtension.order, new List<Gene> { curGene });
                }
                    

            }
            //Once we have the List, iterate over it to run the relavant functions.
            List<int> keysList = geneDict.Keys.ToList();
            keysList.Sort();
            foreach (int key in keysList)
            {
                foreach (var curGene in geneDict[key])
                {
                    curGene.PostAdd();
                }
            }
        }
    }
}
