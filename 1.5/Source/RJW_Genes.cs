using System;
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

            RJW_BGS.VGEHybridUtility.LogAllFoundVGEHybridDefinitions();
        }
    }
}
