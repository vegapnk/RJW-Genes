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


namespace RJW_Genes
{
    /// <summary>
    /// Changes LicentiaLabs (if Present) to alter the TransferNutrition for rjw_genes_generous_donor. 
    /// This code is exercised / loaded in the HarmonyInit.
    /// Patched File: https://gitgud.io/John-the-Anabaptist/licentia-labs/-/blob/master/Source/LicentiaLabs/LicentiaLabs/Cumflation.cs
    /// </summary>
    /// 
    class Patch_TransferNutrition
    {
        // This patch does not need the normal Harmony Targetting, 
        // as it needs to be added only on demand (See HarmonyInit.cs)
        public static void Postfix(Pawn giver, Pawn receiver, float cumAmount)
        {
            // Design decision:
            // I could have done some transpiler stuff, but that is scary and might need to be adjusted quite a lot 
            // Hence, I simply re-book the nutrition back to the giver in the Postfix. That should be robust and easy. 

        }
    }
}