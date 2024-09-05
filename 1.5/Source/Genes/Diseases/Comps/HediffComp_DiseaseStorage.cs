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

        List<int> RemainingTicks = new List<int>();
        List<GeneDef> StoredDiseases = new List<GeneDef>();

        public void StoreDisease(GeneDef disease)
        {
            if (StoredDiseases.Contains(disease)) {
                RemainingTicks[StoredDiseases.IndexOf(disease)] = Props.ticksThatDiseasesAreStored;
            } else
            {
                StoredDiseases.Add(disease);
                RemainingTicks.Add(Props.ticksThatDiseasesAreStored);
            }
        }

        public List<GeneDef> GetStoredDiseases() { return StoredDiseases.ToList(); }

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            List<int> indizesToRemove = new List<int>();
            for (int i = 0; i < RemainingTicks.Count; i++) {
                RemainingTicks[i] = RemainingTicks[i] - 1; 
                if (RemainingTicks[i] <= 0)
                    indizesToRemove.Add(i);
            }

            foreach (int indice in indizesToRemove.OrderByDescending(v => v))
            {
                RemainingTicks.RemoveAt(indice);
                StoredDiseases.RemoveAt(indice);
            }
        }

    }

}
