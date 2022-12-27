using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rjw;
using Verse;
using RimWorld;
using rjw.Modules.Interactions.Rules.PartKindUsageRules;
using rjw.Modules.Interactions.Internals.Implementation;

namespace RJW_Genes
{
    [StaticConstructorOnStartup]
    internal static class First
    {
        static First()
        {
            AddtoIPartPreferenceRule();
        }

        //Modified code from https://gitgud.io/lutepickle/rjw_menstruation/-/tree/main/1.4/source/RJW_Menstruation/RJW_Menstruation
        private static void AddtoIPartPreferenceRule()
        {
            List<IPartPreferenceRule> partPreferenceRules = Unprivater.GetProtectedValue<List<IPartPreferenceRule>>("_partKindUsageRules", typeof(PartPreferenceDetectorService));
            partPreferenceRules.Add(new Interactions.GenesPartKindUsageRule());
        }
    }
}
