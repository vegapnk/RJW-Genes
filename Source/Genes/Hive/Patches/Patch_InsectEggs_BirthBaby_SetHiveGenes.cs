using HarmonyLib;
using RimWorld;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    /// <summary>
    /// Patches the method `ProcessHumanLikeInsectEgg` from `Hediff_InsectEgg`. 
    /// 
    /// The 'ProcessHumanLikeInsectEgg' returns the finished baby, for which we alter the pawn according to our xenotypes.
    /// Note: This covers Insect-Egg Pregnancies only, and there is a (very similar) class `Patch_BirthOutCome_SetHiveGenes.cs` that handles normal pregnancies
    /// </summary>

    [HarmonyPatch(typeof(Hediff_InsectEgg), "ProcessHumanLikeInsectEgg")]
    public class Patch_InsectEgg_BirthBaby_SetHiveGenes
    {


        [HarmonyPostfix]
        static void HandleHiveBasedInheritance(ref Thing __result)
        {

            // Check: Was the born thing a pawn? 
            if (__result == null || !(__result is Pawn))
            {
                if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message("There was a birth of something non-human - not entering logic for queen-drone-xenotype inheritance.");
                return;
            }

            Pawn pawn = (Pawn)__result;

            // Important: Not all pawns have mother/father. Some Pawns are born in Growth-Vats or born from mod. 
            bool hasQueenParent = HiveBirthLogic.TryFindParentQueenXenotype(pawn) != null;
            bool hasDroneParent = HiveBirthLogic.TryFindParentDroneXenotype(pawn) != null;

            if (hasQueenParent)
            {
                if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"PostFix Hediff_InsectEgg::ProcessHumanLikeInsectEgg - Checking Hive Inheritance because {pawn} has a queen parent.");
                HiveBirthLogic.ManageHiveBirth(pawn, hasDroneParent);
            } else
            {
                if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"Ignoring Postfix Hediff_InsectEgg::ProcessHumanLikeInsectEgg - No Queen Parent - No Action.");
            }
        }

    }
}
