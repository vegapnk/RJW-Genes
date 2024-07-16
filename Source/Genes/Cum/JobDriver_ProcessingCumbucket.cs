using LicentiaLabs;
using System.Collections.Generic;
using Verse;
using Verse.AI;
using UnityEngine;
using System;
using rjw;
using RimWorld;

namespace RJW_Genes
{
    /// <summary>
    /// Shamelessly stolen from LicentaLabs
    /// [Jaals Fork] https://gitgud.io/Jaaldabaoth/licentia-labs/-/blob/master/Source/LicentiaLabs/LicentiaLabs/JobDriver_VomitCum.cs
    /// </summary>
    class JobDriver_ProcessingCumbucket : JobDriver_Vomit
    {
        public override bool CanBeginNowWhileLyingDown()
        {
            return true;
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            if (ModsConfig.IsActive("rjw.sexperience"))
                yield break;

            ModLog.Debug("Starting Toils for Processing Cumbucket");

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
                        if (ModsConfig.IsActive("LustLicentia.RJWLabs"))
                            FilthMaker.TryMakeFilth(this.job.targetA.Cell, base.Map, Licentia.ThingDefs.FilthCum, sourceName);
                        SpawnCum(this.pawn, this.job.targetA.Cell, base.Map);
                    }
                    else
                    {
                        if (ModsConfig.IsActive("LustLicentia.RJWLabs"))
                            FilthMaker.TryMakeFilth(this.job.targetA.Cell, base.Map, Licentia.ThingDefs.FilthCum);
                        SpawnCum(this.pawn, this.job.targetA.Cell, base.Map);
                    }
                }
                this.ticksLeft--;
                if (this.ticksLeft <= 0)
                {
                    base.ReadyForNextToil();
                    TaleRecorder.RecordTale(Licentia.TaleDefs.VomitedCum, new object[]
                    {
                        this.pawn
                    });
                }
            };
            toil.defaultCompleteMode = ToilCompleteMode.Never;
            toil.WithEffect(EffecterDefOf.Vomit, TargetIndex.A, new Color(100f, 100f, 100f, 0.5f));
            toil.PlaySustainerOrSound(() => SoundDefOf.Vomit, 1f);
            yield return toil;
            yield break;
        }

        private void SpawnCum(Pawn pawn, IntVec3 cell, Map map)
        {
            ModLog.Warning($"This will be spawning cum for {pawn} at {cell}");
        }

        private int ticksLeft;

        public string sourceName;
    }
}
