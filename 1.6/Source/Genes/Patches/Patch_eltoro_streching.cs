using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using HarmonyLib;

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
        public static void Postfix(Pawn pawn, BodyPartRecord part, HediffDef def, float severity, ref bool __result)
        {
            if (pawn?.genes?.HasActiveGene(GeneDefOf.rjw_genes_elasticity) ?? false)
            {
                ModLog.Debug($"Preventing creation of Injury Hediffs from streching for pawn {pawn.Name}.");
                __result = false;
                return;
            } 
            else 
            {
                return;
            }
        }
    }

    public class Patch_eltoro_strechheal
    {
        /// <summary>
        /// Patch function that connects to Strecher.ApplyInjury, itercepting the creation of injury hediffs, and preventing if a Gene would stop the injury.
        /// </summary>
        /// <returns></returns>
        public static void Postfix(ref HediffComp __instance, ref float __result)
        {
            if (__instance.Pawn?.genes?.HasActiveGene(GeneDefOf.rjw_genes_elasticity) ?? false)
            {
                ModLog.Debug($"Healing streching factor @ x2 speed for pawn : {__instance.Pawn.Name}.");
                __result = 2f;
            } else
            {
                return;
            }

                
        }
    }

}
