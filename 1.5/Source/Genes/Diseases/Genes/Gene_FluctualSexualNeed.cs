using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using static HarmonyLib.Code;
using static RimWorld.ColonistBar;

namespace RJW_Genes
{
    public class Gene_FluctualSexualNeed : Gene
    {

        int event_interval;
        float event_chance;

        const float REST_INCREASE = 0.1f;
        const float SET_SEXNEED_TO = 0.1f;

        public Gene_FluctualSexualNeed() : base()
        {
            TickBasedChanceExtension tickbasedChanceExt = GeneDefOf.rjw_genes_fluctual_sexual_needs.GetModExtension<TickBasedChanceExtension>();
            event_interval = tickbasedChanceExt?.tickInterval ?? 30000; // 30K = 1/2 day
            event_chance = tickbasedChanceExt?.eventChance ?? 0.1f; 
        }


        public override void Tick()
        {
            base.Tick();

            if (pawn.IsHashIntervalTick(event_interval) && (new Random()).NextDouble() < event_chance)
            {
                ModLog.Debug($"Firing Gene_FluctualSexualNeed for {pawn}");
                ApplyFluctualSexNeedEffect(pawn);
            }
        }

        public static void ApplyFluctualSexNeedEffect(Pawn pawn)
        {
            if (pawn == null || pawn.needs == null) return; 

            var sexneed = pawn.needs.TryGetNeed<rjw.Need_Sex>();
            if (sexneed != null)
            {
                sexneed.CurLevelPercentage = SET_SEXNEED_TO;
            }

            // Pump up Wake-Ness
            if (pawn.needs.rest != null)
                pawn.needs.rest.CurLevel += REST_INCREASE;
        }

    }
}
