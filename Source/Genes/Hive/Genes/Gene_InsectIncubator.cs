using RimWorld;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{

    /// <summary>
    /// This Gene checks for all parasitic Insect-Eggs in a Pawn:
    /// 1. Is it fertilized ? => tick it down "extra".
    /// 2. Is it not fertilized? => fertilize it with the Incubator as parent
    /// 
    /// Important: The other half of the behavior for the gene (more egg-capacity) is in `Patch_InsectINcubator_PregnancyHelper`.
    /// </summary>
    public class Gene_InsectIncubator : Gene
    {

        const int TICK_INTERVAL = 60000 / 48; // 60k = 1 day, we want 0.5h which is 1/48th of 1 day. 

        public override void Tick()
        {
            base.Tick();

            // Don't check too often, only in the HashTickInterval to safe some computing power
            if (this.pawn.IsHashIntervalTick(TICK_INTERVAL) && this.pawn.Map != null)
            {
                List<Hediff_InsectEgg> eggs = new List<Hediff_InsectEgg>();
                pawn.health.hediffSet.GetHediffs<Hediff_InsectEgg>(ref eggs);

                foreach (Hediff_InsectEgg egg in eggs)
                {
                    // The implanter check checks if the egg is still in an ovipositor. 
                    if (egg.implanter == null || egg.implanter == pawn)
                        continue;

                    if (!egg.fertilized && egg.implanter != null) { 
                        egg.Fertilize(pawn);
                        if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"Gene_InsectIncubator: fertilized egg {egg} in {pawn}");
                    }
                    else if (egg.fertilized)
                    {
                        egg.lastTick += TICK_INTERVAL;
                    }
                }
            }
        }
    }
}
