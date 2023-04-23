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
            }
        }
    }
}
