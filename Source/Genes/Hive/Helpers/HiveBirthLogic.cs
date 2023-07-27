using RimWorld;
using rjw;
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
        public static void ManageHiveBirth(Pawn pawn, bool hasDroneParent = false, Either<XenotypeDef, CustomXenotype> fallbackQueenDef = null, Either<XenotypeDef, CustomXenotype> fallbackDroneDef = null)
        {
            Either<XenotypeDef,CustomXenotype> queenDef = TryFindParentQueenXenotype(pawn);
            if (queenDef == null) queenDef = fallbackQueenDef;
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
                    MakeQueen(pawn, queenDef);
                    if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"Queen Chance: {hiveOffspringChanceDef.queenChance * 100}% chance,rolled { roll}");
                }
                // Case 2.b: New Drone born
                else if (roll < hiveOffspringChanceDef.droneChance + hiveOffspringChanceDef.queenChance)
                {
                    var droneDef = TryFindParentDroneXenotype(pawn); 
                    MakeDrone(pawn,droneDef);
                    if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"Drone Chance ({(hiveOffspringChanceDef.droneChance + hiveOffspringChanceDef.queenChance) * 100}% chance,rolled {roll}))");
                }
                // Case 2.c: Worker
                else
                {
                    if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"{pawn} born as a worker ({(hiveOffspringChanceDef.workerChance) * 100}% chance,rolled {roll}))");
                    MakeWorker(pawn, queenDef);
                }
            }
        }

        private static void MakeQueen(Pawn pawnToBeQueen, Either<XenotypeDef,CustomXenotype> queenDef) {
            if (queenDef == null && pawnToBeQueen == null)
                return;
            if (queenDef.isLeft) {
                var xenotype = queenDef.left;
                pawnToBeQueen.genes.SetXenotype(xenotype);
                if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"{pawnToBeQueen} born as a new queen with Xenotype {xenotype.defName}");
            } else {
                var customXenotype = queenDef.right;

                foreach (var gene in customXenotype.genes)
                    pawnToBeQueen.genes.AddGene(gene, true);

                pawnToBeQueen.genes.xenotypeName = customXenotype.name;
                pawnToBeQueen.genes.iconDef = customXenotype.iconDef;

                if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"{pawnToBeQueen} born as a new queen with custom Xenotype {customXenotype.name}");
            }

            MakeQueenBornLetter(pawnToBeQueen);
        }


        private static void MakeDrone(Pawn pawnToBeDrone, Either<XenotypeDef, CustomXenotype> droneDef)
        {
            if (droneDef == null && pawnToBeDrone == null)
                return;
            if (droneDef.isLeft)
            {
                var xenotype = droneDef.left;
                pawnToBeDrone.genes.SetXenotype(xenotype);
                if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"{pawnToBeDrone} born as a new drone with Xenotype {xenotype.defName}");
            }
            else
            {
                var customXenotype = droneDef.right;

                foreach (var gene in customXenotype.genes)
                    pawnToBeDrone.genes.AddGene(gene, true);

                pawnToBeDrone.genes.xenotypeName = customXenotype.name;
                pawnToBeDrone.genes.iconDef = customXenotype.iconDef;

                if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"{pawnToBeDrone} born as a new drone with custom Xenotype {customXenotype.name}");
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
        private static void MakeWorker(Pawn pawnTobeWorker, Either<XenotypeDef, CustomXenotype> queenDef)
        {
            if (pawnTobeWorker == null)
                return;

            var mappings = HiveUtility.GetQueenWorkerMappings();
            String queenDefName = HiveUtility.GetXenotypeDefName(queenDef);
            if (queenDef == null || mappings.NullOrEmpty())
                return;

            var genes = mappings.TryGetValue(queenDefName, HiveUtility.LookupDefaultWorkerGenes());
            if (genes == null)
                return;

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
        public static Either<XenotypeDef,CustomXenotype> TryFindParentDroneXenotype(Pawn pawn)
        {
            if (pawn == null)
                return null;

            List<DirectPawnRelation> parentRelations = pawn.relations.DirectRelations.FindAll(rel => rel.def.Equals(PawnRelationDefOf.Parent));
            foreach (DirectPawnRelation parent in parentRelations)
            {
                var xenotype = HiveUtility.TryGetDroneXenotype(parent.otherPawn);
                if (xenotype != null) return xenotype;
            }

            return null;
        }

        public static void MakeQueenBornLetter(Pawn bornQueen)
        {
            if (bornQueen == null) return;

            var letter = LetterMaker.MakeLetter(
                "rjw_genes_queenbirth_letter_label".Translate(),
                string.Format("rjw_genes_queenbirth_letter_description".Translate(), xxx.get_pawnname(bornQueen)),
                LetterDefOf.NeutralEvent, bornQueen);
            Find.LetterStack.ReceiveLetter(letter); 
        }

        /// <summary>
        /// Looks up if there is a Xenotype with Queen-Gene for the pawns parents. 
        /// This is to account that maybe father or mother are the queen (instead of hardcoding things for father).
        /// If both are queens, the first is returned.
        /// </summary>
        /// <param name="pawn">The pawn for whichs parent the xenotypes is looked up.</param>
        /// <returns>The Queen-Xenotype of a parent or null. If both are queens, mothers are preferred.</returns>
        public static Either<XenotypeDef,CustomXenotype> TryFindParentQueenXenotype(Pawn pawn)
        {
            if (pawn == null)
                return null;

            List<DirectPawnRelation> parentRelations = pawn.relations.DirectRelations.FindAll(rel => rel.def.Equals(PawnRelationDefOf.Parent));
            foreach (var parent in parentRelations)
            {
                var xenotype = HiveUtility.TryGetQueenXenotype(parent.otherPawn);
                if (xenotype != null) return xenotype;
            }

            return null;
        }
    }
}
