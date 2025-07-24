using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    public class Patch_eltoro_streching
    {
        /// <summary>
        /// Patch function that connects to Strecher.ApplyInjury, itercepting the creation of injury hediffs, and preventing if a Gene would stop the injury.
        /// </summary>
        /// <param name="pawn"></param>
        /// <param name="part"></param>
        /// <param name="def"></param>
        /// <param name="severity"></param>
        /// <returns></returns>
        public static bool Prefix(Pawn pawn, BodyPartRecord part, HediffDef def, float severity)
        {
            ModLog.Debug($"Checking elasticity genes for pawn {pawn.Name}.");
            if (pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_elasticity))
            {
                ModLog.Debug($"Preventing creation of Injury Hediffs from streching for pawn {pawn.Name}.");
                return false;
            } 
            else 
            {
                return true;
            }
        }
    }
}
