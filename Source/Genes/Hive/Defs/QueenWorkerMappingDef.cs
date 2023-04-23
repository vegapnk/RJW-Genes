using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    /// <summary>
    /// This def covers the birth of workers for each queen-xenotype. 
    /// 
    /// It is used when a baby is born to a pawn with the queen-xenotype; 
    /// There is a random check for the type of the baby, and if the baby is born to be a worker, 
    /// additional genes are looked up here. 
    /// </summary>
    public class QueenWorkerMappingDef : Def
    {
        public string queenXenotype;
        public List<string> workerGenes;
    }
}
