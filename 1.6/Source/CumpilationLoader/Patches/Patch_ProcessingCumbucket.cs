using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rjw;
using Verse;
using RJW_Genes;

namespace CumpilationPatcher
{
    internal class Patch_ProcessingCumbucket
    {


        //Patches the 'empty' SpawnCum method in 'JobDriver_ProcessingCumbucket'
        public static void PostFix(Pawn pawn, IntVec3 cell, Map map)
        {
            Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.rjw_genes_filled_living_cumbucket);
            if (hediff == null)
            {
                ModLog.Warning($"{pawn} has the JobDriver_ProcessCumbucket but does not have the Hediff for filled cumbucket.");
                return;
            }


            var storage = hediff.TryGetComp<Cumpilation.Cumflation.HediffComp_SourceStorage>();
            var random_entry = storage.sources.RandomElementByWeight(p => p.amount);

            ThingDef ToSpawn = random_entry.fluid.consumable == null ? Cumpilation.DefOfs.Cumpilation_Cum : random_entry.fluid.consumable;

            ThingDef cumDef = Cumpilation.DefOfs.Cumpilation_Cum;

            // Case 1: "Normal Severity", just puke out a bit of cum here and there. 
            if (hediff.Severity <= 10)
            {
                Thing cum = ThingMaker.MakeThing(cumDef);
                cum.Position = cell;
                int stacks = Math.Max(1, (int)(hediff.Severity * 1.5));
                stacks = Math.Min(stacks, 75); // 75 is the default max stacksize ...
                cum.stackCount = stacks;
                cum.SpawnSetup(map, false);
                hediff.Severity -= (stacks / 50);
            }
            else
            // Case 2: Reserviour mode, put out a lot of cum at once but less often. 
            {
                int stacks = Math.Max(1, (int)(hediff.Severity * 1.5));

                while (stacks > 0)
                {
                    Thing cum = ThingMaker.MakeThing(cumDef);
                    cum.Position = cell;
                    var curStacks = Math.Min(stacks, 75); // 75 is the default max stacksize ...
                    cum.stackCount = stacks;
                    cum.SpawnSetup(map, false);
                    hediff.Severity -= (curStacks / 50);
                    stacks -= curStacks;
                }
            }
        }
    }
}
