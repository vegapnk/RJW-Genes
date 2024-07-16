using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    public class HediffsCompProperties_ProcessCumbucketMTB : HediffCompProperties
    {
        public HediffsCompProperties_ProcessCumbucketMTB()
        {
            this.compClass = typeof(HediffComp_ProcessCumbucket);
        }

        public override IEnumerable<string> ConfigErrors(HediffDef parentDef)
        {
            foreach (string text in base.ConfigErrors(parentDef))
            {
                yield return text;
            }
            if (this.mtbDaysPerStage == null)
            {
                yield return "mtbDaysPerStage is not defined";
            }
            else if (this.mtbDaysPerStage.Count != parentDef.stages.Count)
            {
                yield return "mtbDaysPerStage count doesn't match Hediffs number of stages";
            }
            yield break;
        }

        public List<float> mtbDaysPerStage;
    }
}
