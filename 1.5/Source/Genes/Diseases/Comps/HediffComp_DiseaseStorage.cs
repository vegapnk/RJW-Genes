using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    public class HediffComp_DiseaseStorage : HediffComp
    {
        public HediffCompProperties_DiseaseStorage Props => (HediffCompProperties_DiseaseStorage)this.props;

        List<int> remainingTicks = new List<int>();
        List<GeneDef> storedDiseases = new List<GeneDef>();

        public void StoreDisease(GeneDef disease)
        {
            if (storedDiseases.Contains(disease)) {
                remainingTicks[storedDiseases.IndexOf(disease)] = Props.ticksThatDiseasesAreStored;
            } else
            {
                storedDiseases.Add(disease);
                remainingTicks.Add(Props.ticksThatDiseasesAreStored);
            }
        }
        public List<GeneDef> GetStoredDiseases() { return storedDiseases.ToList(); }

        public override void CompExposeData()
        {
            base.CompExposeData();

            Scribe_Collections.Look<int>(ref remainingTicks, "remainingTicks");
            Scribe_Collections.Look<GeneDef>(ref storedDiseases, "storedDiseases");
        }

    }

}
