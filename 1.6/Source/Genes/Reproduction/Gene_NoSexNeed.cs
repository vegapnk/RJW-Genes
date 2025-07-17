using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    public class Gene_NoSexNeed : Gene
    {

        public override void Tick()
        {
            base.Tick();

            if (!pawn.IsHashIntervalTick(3000))
                return;
            if (!pawn.ageTracker.Adult)
                return;

            if ((bool)(pawn?.genes?.HasActiveGene(GeneDefOf.rjw_genes_no_sex_need)))
            {
                var sex_need = pawn?.needs?.TryGetNeed<rjw.Need_Sex>();
                if (sex_need != null)
                    sex_need.CurLevelPercentage = 0.6f;
            }
        }
    }
}
