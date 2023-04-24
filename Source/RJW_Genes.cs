using System.Collections.Generic;
using System.Linq;
using Verse;

namespace RJW_Genes
{
    [StaticConstructorOnStartup]
    public static class RJW_Genes
    {
        static RJW_Genes()
        {
            ModLog.Message("RJW-Genes loaded");
            if (RJW_Genes_Settings.rjw_genes_detailed_debug)
            {
                ModLog.Message($"{HiveUtility.getQueenXenotypes().EnumerableCount()} Queen-Xenotypes ({string.Join(",", HiveUtility.getQueenXenotypes().Select(t => t.defName))})");
                ModLog.Message($"{HiveUtility.getDroneXenotypes().EnumerableCount()} Drone-Xenotypes ({string.Join(",", HiveUtility.getDroneXenotypes().Select(t => t.defName))})");
                ModLog.Message($"Found {HiveUtility.GetQueenWorkerMappings().Count} Queen-Worker Mappings  ({string.Join(",", HiveUtility.GetQueenWorkerMappings().Keys.Select(t => t.defName))} + Default) ");


                IEnumerable<HiveOffspringChanceDef> offspringChanceDefs = DefDatabase<HiveOffspringChanceDef>.AllDefs;
                IEnumerable<HiveOffspringChanceDef> faultOffspringDefs = offspringChanceDefs.Where(t => t.queenChance + t.workerChance + t.workerChance > 1.02 || t.queenChance + t.workerChance + t.workerChance < 0.98 );
                ModLog.Message($"Found {offspringChanceDefs.Count()} OffspringChanceDefs, of which {faultOffspringDefs.Count()} had faulty chances ({string.Join(",", faultOffspringDefs.Select(t => t.defName))})");
            }
        }
    }
}
