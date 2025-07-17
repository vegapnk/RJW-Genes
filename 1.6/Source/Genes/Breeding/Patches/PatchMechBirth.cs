

using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using rjw;
using Verse;

namespace RJW_Genes
{
    /// <summary>
    /// This Class patches the RJW-Mechbirth to not deal damage when the pawn has the MechBreeder Gene.
    /// This harmony patch was kindly provided by 'shabalox' https://github.com/Shabalox/RJW_Genes_Addons/
    /// </summary>
    [HarmonyPatch(typeof(Hediff_MechanoidPregnancy), "GiveBirth")]
    public static class PatchMechBirth
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            bool found_call = false;
            bool finished = false;
            Label skip_label = il.DefineLabel();
            MethodInfo removeHediff = AccessTools.Method(typeof(Pawn_HealthTracker), nameof(Pawn_HealthTracker.RemoveHediff));
            MethodInfo ismechbreeder = AccessTools.Method(typeof(GeneUtility), "IsMechbreeder");
            foreach (CodeInstruction codeInstruction in instructions)
            {
                yield return codeInstruction;

                if (finished)
                {
                    continue;
                }

                if (!found_call && codeInstruction.Calls(removeHediff))
                {
                    //Load pawn, call function to check if a mechbreeder, and skip past the part which does damage
                    yield return new CodeInstruction(OpCodes.Ldloc_0);
                    yield return new CodeInstruction(OpCodes.Call, ismechbreeder);
                    yield return new CodeInstruction(OpCodes.Brfalse_S, skip_label);
                    yield return new CodeInstruction(OpCodes.Ret);
                    found_call = true;
                }
                else if (found_call)
                {
                    // next instruction after the insert
                    codeInstruction.labels.Add(skip_label);
                    finished = true;
                }
            }
        }
    }
}