using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RJW_Genes;

namespace CumpilationPatcher
{
    public class Patch_Cumpilation_BlockStuffing
    {

        public static bool Prepare() => ModsConfig.IsActive("vegapnk.cumpilation");
        public static void PostFix(Pawn pawn,ref bool __result) { 
            if (pawn != null && pawn.genes != null && pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_un_inflatable))
                __result = false;
        }

    }
}
