using RimWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    /// <summary>
    /// DevNote: Issue #37 came along because I checked for getMother() and getFather(), but it can happen that a pawn has two mothers. 
    /// They are called Mother if they have a ParentRelation and are female.
    /// New behaviour iterates over all parents and returns the first queen/drone or null.
    /// </summary>
    public class HiveBirthLogic
    {
        /// <summary>
        /// Central function for the Hive-Birth logic used in Patches. 
        /// *Only* run this, if the pawn has a queen parent (either as mother/father, or as implanter in case of egg-logic).
        /// Covers the following behavior:
        /// 1. look up the Defs for the mother and HiveOffspringChances (or defaults)
        /// 2. If there is no drone involved, default to worker
        /// 3. Roll a random dice
        /// 3.1 Make a queen
        /// 3.2 Make a drone 
        /// 3.3 Make a worker
        /// </summary>
        /// <param name="pawn">The pawn born, that maybe becomes a hive-xenotype.</param>
        /// <param name="hasDroneParent">whether there was a drone parent involved</param>
        public static void ManageHiveBirth(Pawn pawn, bool hasDroneParent = false)
        {
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
                    // TODO: Make a letter ? Letter doesn't show :( 
                    Find.LetterStack.ReceiveLetter("New Queen", "A new Queen was born! Make sure to adress inheritance before the new queen reaches adolesence.", LetterDefOf.BabyBirth, (LookTargets)(Thing)pawn);
                }
                // Case 2.b: New Drone born
                else if (roll < hiveOffspringChanceDef.droneChance + hiveOffspringChanceDef.queenChance)
                {
                    XenotypeDef droneDef = TryFindParentDroneXenotype(pawn);
                    pawn.genes.SetXenotype(droneDef);
                    if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"{pawn} born as a new drone with xenotype {droneDef.defName} ({(hiveOffspringChanceDef.droneChance + hiveOffspringChanceDef.queenChance) * 100}% chance,rolled {roll}))");
                }
                // Case 2.c: Worker
                else
                {
                    if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"{pawn} born as a worker ({(hiveOffspringChanceDef.workerChance) * 100}% chance,rolled {roll}))");
                    MakeWorker(pawn, queenDef);
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

            pawnTobeWorker.genes.xenotypeName = "Worker";
        }

        /// <summary>
        /// Looks up if there is a Xenotype with Drone-Gene for the pawns parents. 
        /// This is to account that maybe father or mother are the drone (instead of hardcoding things for father).
        /// If both are drones, the mothers is returned.
        /// </summary>
        /// <param name="pawn">The pawn for whichs parent the xenotypes is looked up.</param>
        /// <returns>The Drone-Xenotype of a parent or null. If both are drones, mothers are preferred.</returns>
        public static XenotypeDef TryFindParentDroneXenotype(Pawn pawn)
        {
            if (pawn == null)
                return null;

            List<DirectPawnRelation> parentRelations = pawn.relations.DirectRelations.FindAll(rel => rel.def.Equals(PawnRelationDefOf.Parent));
            foreach (DirectPawnRelation parent in parentRelations)
            {
                XenotypeDef xenotype = HiveUtility.TryGetDroneXenotype(parent.otherPawn);
                if (xenotype != null) return xenotype;
            }

            return null;
        }


        /// <summary>
        /// Looks up if there is a Xenotype with Queen-Gene for the pawns parents. 
        /// This is to account that maybe father or mother are the queen (instead of hardcoding things for father).
        /// If both are queens, the first is returned.
        /// </summary>
        /// <param name="pawn">The pawn for whichs parent the xenotypes is looked up.</param>
        /// <returns>The Queen-Xenotype of a parent or null. If both are queens, mothers are preferred.</returns>
        public static XenotypeDef TryFindParentQueenXenotype(Pawn pawn)
        {
            if (pawn == null)
                return null;

            List<DirectPawnRelation> parentRelations = pawn.relations.DirectRelations.FindAll(rel => rel.def.Equals(PawnRelationDefOf.Parent));
            foreach (var parent in parentRelations)
            {
                XenotypeDef xenotype = HiveUtility.TryGetQueenXenotype(parent.otherPawn);
                if (xenotype != null) return xenotype;
            }

            return null;
        }
    }
}
