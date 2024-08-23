using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    /// <summary>
    /// Patches the method `ApplyBirthOutcome` from `PregnancyUtility`. 
    /// 
    /// The 'ApplyBirthOutcome' returns the finished baby, for which we alter the pawn according to our xenotypes.
    /// </summary>
    
    [HarmonyPatch(typeof(PregnancyUtility), nameof(PregnancyUtility.ApplyBirthOutcome))]
    public class Patch_BirthOutcome_SetHiveGenes
    {


        [HarmonyPostfix]
        static void HandleHiveBasedInheritance(ref Thing __result)
        {
            
            // Check: Was the born thing a pawn? 
            if (__result == null || !(__result is Pawn))
            {
                if (RJW_Genes_Settings.rjw_genes_detailed_debug)  ModLog.Message("There was a birth of something non-human - not entering logic for queen-drone-xenotype inheritance.");
                return;
            }

            Pawn pawn = (Pawn)__result;

            // Important: Not all pawns have mother/father. Some Pawns are born in Growth-Vats or born from mod. 
            bool hasQueenParent = HiveBirthLogic.TryFindParentQueenXenotype(pawn) != null;
            bool hasDroneParent = HiveBirthLogic.TryFindParentDroneXenotype(pawn) != null;

            if (hasQueenParent)
            {
                if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"PostFix PregnancyUtility::ApplyBirthOutcome - Checking Hive Inheritance because {pawn} has a queen parent.");

                HiveBirthLogic.ManageHiveBirth(pawn, hasDroneParent);
            }
            else
            {
                if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"Ignoring Postfix PregnancyUtility::ApplyBirthOutcome - No Quene Parent - Doing Nothing");
            }
        }

    }
}
