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
    /// To save performance, this gene fires (default) every 0.5h, which also means a slight delay until fertilization happens.
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
                // This part works as intended and shows Non-Human Eggs too
                //if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"Gene_InsectIncubator: Found {eggs.Count} Hediff_InsectEgg in {pawn}");


                foreach (Hediff_InsectEgg egg in eggs)
                {
                    // The implanter check checks if the egg is still in an ovipositor. 
                    if (egg.implanter == null || egg.implanter == pawn)
                        continue;

                    if (!egg.fertilized && egg.implanter != null)
                    {
                        egg.Fertilize(pawn);
                        // DevNote Issue 38: Sometimes Eggs are not fertilized here, because the normal Fertilize Function is called which has an upper Limit on Gestation.
                        // I will not do anything about it here, maybe upstream, but I print here. 
                        if (RJW_Genes_Settings.rjw_genes_detailed_debug)
                        {
                            if (egg.fertilized)
                                ModLog.Message($"Gene_InsectIncubator: fertilized egg {egg} in {pawn}");
                            else if (egg.GestationProgress > 0.5)
                                ModLog.Message($"Gene_InsectIncubator: Failed to fertilize {egg} in {pawn} due to high gestation progress");
                            else
                                ModLog.Message($"Gene_InsectIncubator: failed to fertiliz egg {egg} in {pawn}");
                        }
                    }
                    // DevNote: There is an issue with Eggs reaching too much gestation progress (>100%), which causes DownStream bugs. To avoid this, there are some extra checks in place.  
                    else if (egg.fertilized && egg.GestationProgress <= .93)
                    {
                        egg.lastTick += TICK_INTERVAL;
                    }
                }
            }
        }
    }
}
