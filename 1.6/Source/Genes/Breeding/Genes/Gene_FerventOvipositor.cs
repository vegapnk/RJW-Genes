using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Verse;
using rjw;

namespace RJW_Genes
{

    /// <summary>
    /// Manages the rjw_genes_fervent_ovipositor to grow eggs much faster.
    /// 
    /// TODO: Move the Multiplier into XML 
    /// TODO: This gene only works after the first egg, the first egg for two new pawns spawns at the same time (strange). 
    /// </summary>
    public class Gene_FerventOvipositor : Gene
    {

        const int MULTIPLIER = 3; // Tick 3 times as much, making a pawn with this Gene Produce Eggs 4 times as fast as the normal.

        public override void Tick()
        {
            base.Tick();

            if (pawn == null) return;

            Hediff_NaturalSexPart OvipositorF = (Hediff_NaturalSexPart)pawn.health.hediffSet.GetFirstHediffOfDef(rjw.Genital_Helper.ovipositorF);

            if (OvipositorF == null) return;

            OvipositorF.AsHediff.TryGetComp<HediffComp_Ovipositor>().eggInterval.max = 10000 / MULTIPLIER;

            // DevNote: I first had a for-loop calling OviPositorF.tick(), but I fear that would be a performance sink.
            // Also, it would double other aspects as well, such as bleeding out through your insect-PP or dropping out the eggs.
        }


    }
}