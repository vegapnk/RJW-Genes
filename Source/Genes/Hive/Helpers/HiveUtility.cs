using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    internal class HiveUtility
    {

        /// <summary>
        /// Checks for existance of the RJW-Gene `queen`, if the pawn is spawned and if the pawn has reached adulthood.
        /// Despite the naming, a Queen can also be male.
        /// </summary>
        /// <param name="pawn">The pawn that could be an Adult Queen</param>
        /// <returns>Whether the pawn is an adult queen.</returns>
        public static bool IsAdultQueen(Pawn pawn)
        {

            if (pawn == null || !pawn.Spawned)
                return false;

            if (GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_queen)) 
            { 
                return pawn.ageTracker.Adult;
            }

            return false;
        }

        public static int QueensOnMap() => GetQueensOnMap().Count;

        /// <summary>
        /// Checks for all pawns on the Players Home Map if they are an adult queen.
        /// Adultness is determined by Base-Game Logic, Queen is determined by the rjw_genes_queen GeneDefOf (Not Xenotype). 
        /// </summary>
        /// <returns>A list of queens on the players HomeMap</returns>
        public static List<Pawn> GetQueensOnMap()
        {
            Map map = Find.Maps.Where(mapCandidate => mapCandidate.IsPlayerHome).First();

            if (map != null)
            {
                List<Pawn> playersPawns = map.mapPawns.SpawnedPawnsInFaction(Faction.OfPlayer);
                return playersPawns.FindAll(pawn => pawn.Spawned && IsAdultQueen(pawn));
            }
            // Fallback: Something is wrong with Map 
            return new List<Pawn>();
        }

        /// <summary>
        /// Checks if the pawn is on the players home map. 
        /// 
        /// Reason is that drones should only be punished for absence of queen if they are on the map and there is no queen.
        /// If they are on a mission, transport-pod etc. they should not get boni or mali. 
        /// </summary>
        /// <param name="pawn">The pawn for which to check map-presence.</param>
        /// <returns>True if the pawn is on the home-map, False otherwise.</returns>
        public static bool PawnIsOnHomeMap(Pawn pawn)
        {
            if  (Find.Maps.NullOrEmpty() || !Find.Maps.Where(mapCandidate => mapCandidate.IsPlayerHome).Any()) { 
                return false; 
            }
            Map homeMap = Find.Maps.Where(mapCandidate => mapCandidate.IsPlayerHome).First();
            return
                homeMap != null && pawn != null
                && pawn.Spawned
                && pawn.Map == homeMap;
        }

        /// <summary>
        /// Returns the Xenotype of a pawn if the pawn has a xenotype with the queen gene.
        /// Null otherwise.
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns>A xenotype with a queen gene, or null.</returns>
        public static Either<XenotypeDef,CustomXenotype> TryGetQueenXenotype(Pawn pawn)
        {
            if (pawn == null)
                return null; 

            if (GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_queen))
            {
                var potentialXenotype = pawn.genes.Xenotype;
                if (potentialXenotype != null && potentialXenotype.genes.Contains(GeneDefOf.rjw_genes_queen))
                {
                    return new Either<XenotypeDef,CustomXenotype>(potentialXenotype);
                }

                var potentialCustomXenotype = pawn.genes.CustomXenotype;
                if (potentialCustomXenotype != null && potentialCustomXenotype.genes.Contains(GeneDefOf.rjw_genes_queen))
                {
                    return new Either<XenotypeDef, CustomXenotype>(potentialCustomXenotype);
                }

            }

            return null;
        }


        /// <summary>
        /// Returns the Xenotype of a pawn if the pawn has a xenotype with the drone gene.
        /// Null otherwise.
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns>A xenotype with a drone gene, or null.</returns>
        public static Either<XenotypeDef,CustomXenotype> TryGetDroneXenotype(Pawn pawn)
        {
            if (pawn == null)
                return null;

            if (GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_drone))
            {
                var potentialXenotype = pawn.genes.Xenotype;
                if (potentialXenotype != null && potentialXenotype.genes.Contains(GeneDefOf.rjw_genes_drone))
                {
                    return new Either<XenotypeDef,CustomXenotype>(potentialXenotype);
                }

                var potentialCustomXenotype = pawn.genes.CustomXenotype;
                if (potentialCustomXenotype != null && potentialCustomXenotype.genes.Contains(GeneDefOf.rjw_genes_drone))
                {
                    return new Either<XenotypeDef, CustomXenotype>(potentialCustomXenotype);
                }
            }

            return null;
        }

        /// <summary>
        /// Looks up the Queen-WorkerMappings and returns a cleaned / updated dictionary.
        /// 
        /// This method takes care of genes maybe not existing (from other mods) or misspellings etc. 
        /// Prints a bigger piece of information when debug printing is enabled. 
        /// </summary>
        /// <returns>A mapping which Queen-Xenotypes should produce which worker genes.</returns>
        public static Dictionary<String,List<GeneDef>> GetQueenWorkerMappings()
        {
            IEnumerable<QueenWorkerMappingDef> mappingDefs = DefDatabase<QueenWorkerMappingDef>.AllDefs;

            Dictionary<string, List<GeneDef>> results = new Dictionary<string, List<GeneDef>>();

            // Dev-Note: I first a nice lambda here, but I used nesting in favour of logging.
            foreach (QueenWorkerMappingDef mappingDef in mappingDefs)
            {
                // Option A) This is the default worker genes, expected, just ignore.
                if (mappingDef.defName == "rjw_genes_default_worker_genes")
                {
                    // Do nothing, there is no lookup but this entry is fine and should not log a warning. 
                    continue;
                }
                // Option B) The Xenotype is a "static" Xenotype, from a mod or something.
                XenotypeDef queenDef = DefDatabase<XenotypeDef>.GetNamed(mappingDef.queenXenotype);
                string defName = null;
                if (queenDef != null)
                {
                    defName = queenDef.defName;
                }
                else if (Current.Game != null && Current.Game.customXenotypeDatabase != null)
                {
                    // Option C) The Xenotype is a Custom Xenotype, created by the player
                    CustomXenotype customQueenDef = Current.Game.customXenotypeDatabase.customXenotypes.Find(f => f.name == mappingDef.defName);
                    defName = customQueenDef?.name;
                }
                if (defName != null)
                {
                    List<GeneDef> workerGenes = new List<GeneDef>();
                    foreach (string geneName in mappingDef.workerGenes)
                    {
                        GeneDef workerGene = DefDatabase<GeneDef>.GetNamed(geneName);
                        if (workerGene != null)
                            workerGenes.Add(workerGene);
                        else if(RJW_Genes_Settings.rjw_genes_detailed_debug)
                            ModLog.Warning($"Could not look up Gene {geneName} for {mappingDef.queenXenotype}.");
                    }
                    results.Add(defName, workerGenes);
                    continue;
                }

                // Option D) Fallback, unknown
                if (RJW_Genes_Settings.rjw_genes_detailed_debug) 
                    ModLog.Warning($"Did not find a matching xenotype for {mappingDef.queenXenotype}! Defaulting to rjw_genes_default_worker_genes.");
                
            }

            return results;
        }

        /// <summary>
        /// Looks up the default genes for any queen offspring that has no other definition for it. 
        /// This is done by looking for the mapping with *exactly* defName rjw_genes_default_worker_genes. 
        /// 
        /// The idea is that players can edit the default types, but that this is a protected keyword. 
        /// </summary>
        /// <returns>A list of genes for workers that do not have specific mappings defined.</returns>
        public static List<GeneDef> LookupDefaultWorkerGenes()
        {
            IEnumerable<QueenWorkerMappingDef> mappingDefs = DefDatabase<QueenWorkerMappingDef>.AllDefs;

            List<GeneDef> workerGenes = new List<GeneDef>();

            var defaultMapping = mappingDefs.First(m => m.defName == "rjw_genes_default_worker_genes");
            if (defaultMapping == null)
            {
                ModLog.Error("Did not find default worker genes for queen-offspring! Please make sure you did not rename the 'rjw_genes_default_worker_genes'.");
                return workerGenes;
            }

            foreach (string geneName in defaultMapping.workerGenes)
            {
                GeneDef workerGene = DefDatabase<GeneDef>.GetNamed(geneName);
                if (workerGene != null)
                    workerGenes.Add(workerGene);
                else if (RJW_Genes_Settings.rjw_genes_detailed_debug)
                    ModLog.Warning($"Could not look up gene {geneName} for rjw_genes_default_worker_genes.");
            }

            return workerGenes;
        }

        public static IEnumerable<XenotypeDef> getQueenXenotypes()
        {
            return DefDatabase<XenotypeDef>.AllDefs.Where(type => type.genes.Contains(GeneDefOf.rjw_genes_queen));
        }

        public static IEnumerable<XenotypeDef> getDroneXenotypes()
        {
            return DefDatabase<XenotypeDef>.AllDefs.Where(type => type.genes.Contains(GeneDefOf.rjw_genes_drone));
        }


        public static String GetXenotypeDefName(Either<XenotypeDef, CustomXenotype> def)
        {
            if (def == null)
                return null;
            else if (def.isLeft)
                return def.left.defName;
            else
                return def.right.name;
        }

        public static HiveOffspringChanceDef LookupDefaultHiveInheritanceChances()
        {
            IEnumerable<HiveOffspringChanceDef> offspringChanceDefs = DefDatabase<HiveOffspringChanceDef>.AllDefs;

            List<GeneDef> workerGenes = new List<GeneDef>();

            var defaultChance = offspringChanceDefs.First(m => m.defName == "rjw_genes_default_hive_offspring_chances");

            if (defaultChance == null)
                ModLog.Warning("Did not find `rjw_genes_default_hive_offspring_chances`. Someone likely changed the defname!");

            return defaultChance;
        }

        public static HiveOffspringChanceDef LookupHiveInheritanceChances(Either<XenotypeDef, CustomXenotype> queenDef)
        {
            if (queenDef == null)
                return LookupDefaultHiveInheritanceChances();

            String queenDefName = queenDef.isLeft ? queenDef.left.defName : queenDef.right.name;

            IEnumerable<HiveOffspringChanceDef> offspringChanceDefs = DefDatabase<HiveOffspringChanceDef>.AllDefs;

            return offspringChanceDefs.FirstOrFallback(t => t.queenXenotype == queenDefName, LookupDefaultHiveInheritanceChances());
        }


    }
}
