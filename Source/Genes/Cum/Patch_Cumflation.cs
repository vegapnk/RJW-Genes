using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using rjw;
using RimWorld;
using Verse;
using LicentiaLabs;

namespace RJW_Genes
{
    /// <summary>
    /// Changes LicentiaLabs (if Present) to not cumflate pawns that are cumflation immune. 
    /// This code is exercised / loaded in the HarmonyInit.
    /// </summary>
    /// 
    class Patch_Cumflation
    {
        // This patch does not need the normal Harmony Targetting, 
        // as it needs to be added only on demand (See HarmonyInit.cs)
        public static bool Prefix(SexProps props)
        {
            // Harmony Logic skips the original Method after Prefix when "false" is returned 
            // See https://harmony.pardeike.net/articles/execution.html 

            // We skip the whole Cumflation Logic when the Partner is Cumflation Immune
            if (props != null && props.partner != null && GeneUtility.IsCumflationImmune(props.partner))
            {
                return false;
            }
            return true;
        }
    }
}