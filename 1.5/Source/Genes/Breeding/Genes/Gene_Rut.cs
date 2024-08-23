using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    public class Gene_Rut : Gene
    {
        public override void Tick()
        {
            base.Tick();

            if (pawn == null || pawn.genes == null)
                return;

            var chanceExtension = this.def.GetModExtension<TickBasedChanceExtension>();
            if (chanceExtension == null) return;

            if (pawn.IsHashIntervalTick(chanceExtension.tickInterval)){
                Random r = new Random();
                if (r.NextDouble() < chanceExtension.eventChance)
                {
                    Hediff rut = pawn.health.GetOrAddHediff(HediffDefOf.rjw_genes_genetic_rut);
                    rut.Severity = 1;
                    ModLog.Debug($"Pawn {pawn} gained rjw_genes_genetic_rut based on chance.");
                }
            }
        }
    }
}
