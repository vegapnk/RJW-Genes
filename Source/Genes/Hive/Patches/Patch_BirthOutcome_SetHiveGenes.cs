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
            bool hasQueenParent = TryFindParentQueenXenotype(pawn) != null;
            bool hasDroneParent = TryFindParentDroneXenotype(pawn) != null;

            if (hasQueenParent)
            {
                if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"PostFix PregnancyUtility::ApplyBirthOutcome - Checking Hive Inheritance because {pawn} has a queen parent.");

                XenotypeDef queenDef = TryFindParentQueenXenotype(pawn);
                HiveOffspringChanceDef hiveOffspringChanceDef = HiveUtility.LookupHiveInheritanceChances(queenDef);

                // Case 1: Mother is Queen, Father is something else. Produce Worker.
                if (!hasDroneParent)
                {
                    if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"{pawn} was born as a worker, as it did not have Drone Father ({100}% chance)");
                    MakeWorker(pawn, queenDef);
                }
                // Case 2: Mother is Queen, Father is drone. Apply xenotype as per chance.
                else
                {
                    double roll = (new Random()).NextDouble();
                    // Case 2.a: New Queen born
                    if (roll < hiveOffspringChanceDef.queenChance)
                    {
                        pawn.genes.SetXenotype(queenDef);
                        if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"{pawn} born as a new queen with xenotype {queenDef.defName} ({hiveOffspringChanceDef.queenChance * 100}% chance,rolled {roll})");
                        // TODO: Make a letter ? 
                    }
                    // Case 2.b: New Drone born
                    else if (roll < hiveOffspringChanceDef.droneChance + hiveOffspringChanceDef.queenChance)
                    {
                        XenotypeDef droneDef = TryFindParentDroneXenotype(pawn);
                        pawn.genes.SetXenotype(droneDef);
                        if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"{pawn} born as a new drone with xenotype {droneDef.defName} ({(hiveOffspringChanceDef.droneChance + hiveOffspringChanceDef.queenChance) * 100}% chance,rolled {roll}))");
                    }
                    // Case 2.c: Worker
                    else {
                        if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"{pawn} born as a worker ({(hiveOffspringChanceDef.workerChance) * 100}% chance,rolled {roll}))");
                        MakeWorker(pawn, queenDef);
                    }
                }
            }
        }

        /// <summary>
        /// Turns a given pawn into a worker, by looking up the relevant genes as per queen.
        /// 
        /// If the queen xenotype has no mapping, the  "rjw_genes_default_worker_xenotype" are used instead.
        /// The genes are added as endogenes, so the worker can still become a xenotype. 
        /// </summary>
        /// <param name="pawnTobeWorker">The pawn for which the genes are added.</param>
        /// <param name="queenDef">The xenotype of the queen, used for lookup.</param>
        private static void MakeWorker(Pawn pawnTobeWorker, XenotypeDef queenDef)
        {
            if (pawnTobeWorker == null)
                return;

            var mappings = HiveUtility.GetQueenWorkerMappings();

            var genes = mappings.TryGetValue(queenDef, HiveUtility.LookupDefaultWorkerGenes());

            foreach (var gene in genes)
                pawnTobeWorker.genes.AddGene(gene, false);

        }

        /// <summary>
        /// Looks up if there is a Xenotype with Drone-Gene for the pawns parents. 
        /// This is to account that maybe father or mother are the drone (instead of hardcoding things for father).
        /// If both are drones, the mothers is returned.
        /// </summary>
        /// <param name="pawn">The pawn for whichs parent the xenotypes is looked up.</param>
        /// <returns>The Drone-Xenotype of a parent or null. If both are drones, mothers are preferred.</returns>
        private static XenotypeDef TryFindParentDroneXenotype(Pawn pawn)
        {
            if (pawn == null)
                return null;

            var motherXenotype = HiveUtility.TryGetDroneXenotype(pawn.GetMother());
            var fatherXenotype = HiveUtility.TryGetDroneXenotype(pawn.GetFather());

            if (motherXenotype != null)
                return motherXenotype;
            if (fatherXenotype != null) 
                return fatherXenotype;

            return null;
        }


        /// <summary>
        /// Looks up if there is a Xenotype with Queen-Gene for the pawns parents. 
        /// This is to account that maybe father or mother are the queen (instead of hardcoding things for father).
        /// If both are queens, the mothers is returned.
        /// </summary>
        /// <param name="pawn">The pawn for whichs parent the xenotypes is looked up.</param>
        /// <returns>The Queen-Xenotype of a parent or null. If both are queens, mothers are preferred.</returns>
        private static XenotypeDef TryFindParentQueenXenotype(Pawn pawn)
        {
            if (pawn == null)
                return null;

            var motherXenotype = HiveUtility.TryGetQueenXenotype(pawn.GetMother());
            var fatherXenotype = HiveUtility.TryGetQueenXenotype(pawn.GetFather());

            if (motherXenotype != null)
                return motherXenotype;
            if (fatherXenotype != null)
                return fatherXenotype;

            return null;
        }
    }
}
