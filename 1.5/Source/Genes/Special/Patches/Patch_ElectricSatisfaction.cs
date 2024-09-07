using HarmonyLib;
using RimWorld;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    [HarmonyPatch(typeof(SexUtility), nameof(SexUtility.SatisfyPersonal))]
    public static class Patch_ElectricSatisfaction
    {


        public static void Postfix(SexProps props)
        {
            if (props.pawn == null || props.pawn.Map == null || props.pawn.GetRoom() == null) return;
            if (props.pawn.genes == null || !props.pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_electric_satisfaction)) return;

            BoundedExtension bounds = GeneDefOf.rjw_genes_electric_satisfaction.GetModExtension<BoundedExtension>();
            (float,float) electricityRange =bounds != null ? (bounds.minimum, bounds.maximum) : (100,100); 
            DistanceExtension range = GeneDefOf.rjw_genes_electric_satisfaction.GetModExtension<DistanceExtension>();
            float maxDistance = range != null ? range.distance : 5;

            Room room = props.pawn.GetRoom();
            List<Building> possibleTargets = new List<Building>();
            possibleTargets.AddRange(room.ContainedThings<Building_Battery>());
            possibleTargets.AddRange(room.ContainedThings<Building_PowerSwitch>());
            possibleTargets.AddRange(room.ContainedThings<Building>().Where(f => f.def == RimWorld.ThingDefOf.HiddenConduit));
            possibleTargets.AddRange(room.ContainedThings<Building>().Where(f => f.def == RimWorld.ThingDefOf.PowerConduit));

            List<Building> possibleCloseTargets = possibleTargets
                .Where(target => target.PositionHeld.InHorDistOf(props.pawn.PositionHeld, maxDistance))
                .OrderBy(target => target.PositionHeld.DistanceTo(props.pawn.PositionHeld))
                .ToList();
            ModLog.Debug($"Electric Satisfaction had {possibleTargets.Count()} Building-Targets in the room, {possibleCloseTargets.Count()} in range of {maxDistance} distance to {props.pawn}");
            
            foreach(Building building in possibleCloseTargets)
            {
                CompPower powerInfo = building.TryGetComp<CompPower>();
                if (powerInfo == null || powerInfo.PowerNet == null) continue;

                float resulting_charge = (new Random()).Next((int)electricityRange.Item1, (int)electricityRange.Item2); 

                PowerNet net = powerInfo.PowerNet;
                var charge = net.GetType().GetMethod("ChangeStoredEnergy", BindingFlags.NonPublic | BindingFlags.Instance);
                charge.Invoke(net, new object[] { resulting_charge });
                ModLog.Debug($"Charged {building} Powernet {net} with {resulting_charge} WattHours");
                return;
            }


        }

    }
}
