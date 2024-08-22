using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;
using HarmonyLib;
using rjw;

namespace RJW_BGS
{

    
[HarmonyPatch(typeof(Hediff_BasePregnancy))]
public class Patch_RJW_BasePregnancy_VanillaExpandedGenetics
{
    public static HediffDef controler = DefDatabase<HediffDef>.GetNamed("rjw_genes_animal_control_hediff", false);

    /// <summary>
    /// This Patch (only) adds the "rjw_genes_animal_control_hediff" to newborn VE hybrid-animals. 
    /// </summary>
    /// <param name="__instance"></param>
    [HarmonyPostfix]
    [HarmonyPatch("GenerateBabies")]
    public static void AddHediff (Hediff_BasePregnancy __instance)
    {

        if (controler == null) return;

        if (!RJW_BGSSettings.rjw_bgs_VE_genetics)
        {
            return;
        }

        foreach (Pawn baby in __instance.babies)
        {
            if(baby != null && VGEHybridUtility.SupportedHybridRaces.Contains(baby.kindDef))
               baby.health.AddHediff(controler);
        }

        }
    }
}
