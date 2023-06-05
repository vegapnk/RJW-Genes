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
        static void HandleHiveBasedInheritance(ref Thing __result, ref Hediff_InsectEgg __instance)
        {
            // Check: Was the born thing a pawn? 
            if (__result == null || !(__result is Pawn))
            {
                if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message("There was a birth of something non-human - not entering logic for queen-drone-xenotype inheritance.");
                return;
            }

            Pawn pawn = (Pawn)__result;

            XenotypeDef queenDef = HiveBirthLogic.TryFindParentQueenXenotype(pawn) ?? TryFindParentQueenXenotypeFromEgg(__instance);
            XenotypeDef droneDef = HiveBirthLogic.TryFindParentDroneXenotype(pawn) ?? TryFindParentDroneXenotypeFromEgg(__instance);

            bool hasQueenParent = queenDef != null;
            bool hasDroneParent = droneDef != null;

            if (hasQueenParent)
            {
                if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"PostFix Hediff_InsectEgg::ProcessHumanLikeInsectEgg - Checking Hive Inheritance because {pawn} has a queen parent.");
                HiveBirthLogic.ManageHiveBirth(pawn, hasDroneParent, fallbackQueenDef: queenDef, fallbackDroneDef: droneDef);
            } else
            {
                if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"Ignoring Postfix Hediff_InsectEgg::ProcessHumanLikeInsectEgg - No Queen Parent - No Action.");
            }
        }

        /// <summary>
        /// Tries to retrieve a queen-xenotype-def from a given egg. 
        /// Checking priority goes: Implanter > Fertilizer > Null Otherwise. 
        /// 
        /// This is meant to be a fallback to the parent-relations which were not present in RJW 5.3.1. 
        /// Some comments and thoughts are captured in Issue #37. 
        /// </summary>
        /// <param name="egg">An Egg for which queens are looked up for</param>
        /// <returns>The relevant xenotypedef of a queen, or null.</returns>
        public static XenotypeDef TryFindParentQueenXenotypeFromEgg(Hediff_InsectEgg egg)
        {
            XenotypeDef queenDef = null;
            if (egg == null)
                return null;

            if (egg.implanter != null)
                queenDef = HiveUtility.TryGetQueenXenotype(egg.implanter);

            if (queenDef == null && egg.father != null)
                queenDef = HiveUtility.TryGetQueenXenotype(egg.implanter);

            return queenDef;
        }



        /// <summary>
        /// Tries to retrieve a drone-xenotype-def from a given egg. 
        /// Checking priority goes: Implanter > Fertilizer > Null Otherwise. 
        /// 
        /// This is meant to be a fallback to the parent-relations which were not present in RJW 5.3.1. 
        /// Some comments and thoughts are captured in Issue #37. 
        /// </summary>
        /// <param name="egg">An Egg for which drones are looked up for</param>
        /// <returns>The relevant xenotypedef of a drone, or null.</returns>
        public static XenotypeDef TryFindParentDroneXenotypeFromEgg(Hediff_InsectEgg egg)
        {
            XenotypeDef droneDef = null;
            if (egg == null)
                return null;

            if (egg.implanter != null)
                droneDef = HiveUtility.TryGetQueenXenotype(egg.implanter);

            if (droneDef == null && egg.father != null)
                droneDef = HiveUtility.TryGetQueenXenotype(egg.implanter);

            return droneDef;
        }
    }
}
