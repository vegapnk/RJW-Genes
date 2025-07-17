using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using RimWorld;
using rjw;
using UnityEngine;
using Verse;
using Verse.AI;

namespace RJW_Genes
{
    /// <summary>
    /// Shamelessly stolen from LicentaLabs
    /// [Jaals Fork] https://gitgud.io/Jaaldabaoth/licentia-labs/-/blob/master/Source/LicentiaLabs/LicentiaLabs/JobDriver_VomitCum.cs
    /// </summary>
    public class JobDriver_ProcessingCumbucket : JobDriver_Vomit
    {
        public override bool CanBeginNowWhileLyingDown()
        {
            return true;
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            // DevNote: Right now, this needs RJW.sexperience to produce the Cum-Item. 
            if (!ModsConfig.IsActive("vegapnk.cumpilation"))
                yield break;

            Toil toil = new Toil();
            toil.initAction = delegate ()
            {
                this.ticksLeft = Rand.Range(150, 600);
                int num = 0;
                IntVec3 c;
                for (; ; )
                {
                    c = this.pawn.Position + GenAdj.AdjacentCellsAndInside[Rand.Range(0, 9)];
                    num++;
                    if (num > 12)
                    {
                        break;
                    }
                    if (c.InBounds(this.pawn.Map) && c.Standable(this.pawn.Map))
                    {
                        // DevNote: I am not 100% what this all means, but IL_77 is a jump to the case below (it says IL_77).
                        // basically, this calls the next part of the function, but I am not super sure why this has to be like this. 
                        // JobDrivers are scary. 
                        goto IL_77;
                    }
                }
                c = this.pawn.Position;
            IL_77:
                this.job.targetA = c;
                this.pawn.pather.StopDead();
            };
            toil.tickAction = delegate ()
            {
                if (this.ticksLeft % 150 == 149)
                {
                    if (!sourceName.NullOrEmpty())
                    {
                        //TODO: Currently disabled due to Errors (#129), not a Fix but atleast no more errors
                        //if (ModsConfig.IsActive("LustLicentia.RJWLabs"))
                        //    FilthMaker.TryMakeFilth(this.job.targetA.Cell, base.Map, Licentia.ThingDefs.FilthCum, sourceName);
                        SpawnCum(this.pawn, this.job.targetA.Cell, base.Map);
                    }
                    else
                    {
                        //TODO: Currently disabled due to Errors (#129), not a Fix but atleast no more errors
                        // if (ModsConfig.IsActive("LustLicentia.RJWLabs"))
                        //    FilthMaker.TryMakeFilth(this.job.targetA.Cell, base.Map, Licentia.ThingDefs.FilthCum);
                        
                        SpawnCum(this.pawn, this.job.targetA.Cell, base.Map);
                    }
                }
                this.ticksLeft--;
                if (this.ticksLeft <= 0)
                {
                    base.ReadyForNextToil();
                }
            };
            toil.defaultCompleteMode = ToilCompleteMode.Never;
            toil.WithEffect(EffecterDefOf.Vomit, TargetIndex.A, new Color(100f, 100f, 100f, 0.5f));
            toil.PlaySustainerOrSound(() => SoundDefOf.Vomit, 1f);
            yield return toil;
            yield break;
        }

        /// <summary>
        /// This Function is intentionaly left blank to avoid typeExceptions when cumpilation addon is not present, it is patched using the Patch_processingCumbucket Function in the optional DLL that is only loaded if Cumpilation is present.
        /// </summary>
        /// <param name="pawn"></param>
        /// <param name="cell"></param>
        /// <param name="map"></param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SpawnCum(Pawn pawn, IntVec3 cell, Map map)
        {
            if (!ModsConfig.IsActive("vegapnk.cumpilation"))
                return;
            /*
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
          */
        }

        private int ticksLeft;

        public string sourceName;
    }
}
