using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using rjw;
using RJW_Genes;
using Verse;


namespace CumpilationPatcher
{

    [StaticConstructorOnStartup]
    public static class CumpilationPatcher
    {
        static CumpilationPatcher()
        {
            ModLog.Message("Cumpilation detected, installing relevent RJW_Genes Patches...");
            
            Harmony harmony = new Harmony("rjw_genes_CumPatcher");

            // Gene: Inflatable [Postfix Patch]
            harmony.Patch(AccessTools.Method(typeof(SexUtility), nameof(SexUtility.TransferFluids)),
                postfix: new HarmonyMethod(typeof(Patch_Cumpilation_Inflatable), nameof(Patch_Cumpilation_Inflatable.PostFix)));
            // Gene: Inflation-Resistance [Postfix Patch]
            harmony.Patch(AccessTools.Method(typeof(Cumpilation.Cumflation.CumflationUtility), nameof(Cumpilation.Cumflation.CumflationUtility.CanBeCumflated)),
                postfix: new HarmonyMethod(typeof(Patch_Cumpilation_BlockCumflation), nameof(Patch_Cumpilation_BlockCumflation.PostFix)));
            // Gene: Inflation-Resistance [Postfix Patch]
            harmony.Patch(AccessTools.Method(typeof(Cumpilation.Cumflation.StuffingUtility), nameof(Cumpilation.Cumflation.StuffingUtility.CanBeStuffed)),
                postfix: new HarmonyMethod(typeof(Patch_Cumpilation_BlockStuffing), nameof(Patch_Cumpilation_BlockStuffing.PostFix)));
            // Gene: Living Cumbucket [Postfix Patch] 
            harmony.Patch(AccessTools.Method(typeof(SexUtility), nameof(SexUtility.SatisfyPersonal)),
                postfix: new HarmonyMethod(typeof(Patch_LivingCumbucket_StackHediff), nameof(Patch_LivingCumbucket_StackHediff.PostFix)));

            // JobDriver_processingCumbucket
            harmony.Patch(AccessTools.Method(typeof(JobDriver_ProcessingCumbucket), nameof(JobDriver_ProcessingCumbucket.SpawnCum)),
                postfix: new HarmonyMethod(typeof(Patch_ProcessingCumbucket), nameof(Patch_ProcessingCumbucket.PostFix)));
            // JobGiver_GetLifeForce
            harmony.Patch(AccessTools.Method(typeof(JobGiver_GetLifeForce), nameof(JobGiver_GetLifeForce.GetStoredCum)),
                postfix: new HarmonyMethod(typeof(Patch_JobGiver_GetLifeForce), nameof(Patch_JobGiver_GetLifeForce.PostFix)));

            //CumEater
            /// The patched function is: [HarmonyPatch(typeof(JobDriver_Sex), nameof(JobDriver_Sex.ChangePsyfocus))]
            harmony.Patch(AccessTools.Method(typeof(JobDriver_Sex), nameof(JobDriver_Sex.ChangePsyfocus)),
                postfix: new HarmonyMethod(typeof(Patch_SexTicks_ChangePsyfocus), nameof(Patch_SexTicks_ChangePsyfocus.PostFix)));

        }
    }


    internal class ModLog
    {
        public static string ModId => "RJW-Genes-CumPatcher";

        /// <summary>
        /// Logs the given message with [SaveStorage.ModId] appended.
        /// </summary>
        public static void Error(string message)
        {
            Log.Error($"[{ModId}] {message}");
        }

        /// <summary>
        /// Logs the given message with [SaveStorage.ModId] appended.
        /// </summary>
        public static void Message(string message)
        {
            Log.Message($"[{ModId}] {message}");
        }

        /// <summary>
        /// Logs the given message with [SaveStorage.ModId] appended.
        /// </summary>
        public static void Warning(string message)
        {
            Log.Warning($"[{ModId}] {message}");
        }

        public static void Debug(string message)
        {
            if (RJW_Genes_Settings.rjw_genes_detailed_debug)
            {
                Log.Message($"[{ModId}][debug] {message}");
            }
        }
    }
}
