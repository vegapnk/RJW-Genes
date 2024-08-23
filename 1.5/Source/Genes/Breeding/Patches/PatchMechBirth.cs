

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using rjw;

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
            bool found_skip = false;
            Label skip_label = il.DefineLabel();
            MethodInfo ismechbreeder = AccessTools.Method(typeof(GeneUtility), "IsMechbreeder");
            foreach (CodeInstruction codeInstruction in instructions)
            {
                //Check if the first opcode after endfinally ldloc_0 is and in that case add the label to skip the code
                if (found_skip && codeInstruction.opcode == OpCodes.Ldloc_0)
                {
                    codeInstruction.labels.Add(skip_label);
                }
                found_skip = false;
                if (codeInstruction.opcode == OpCodes.Endfinally)
                {
                    found_skip = true;
                }

                yield return codeInstruction;

                if (codeInstruction.opcode == OpCodes.Call)
                {
                    if (codeInstruction.operand.ToString() == "Boolean TryMakeFilth(Verse.IntVec3, Verse.Map, Verse.ThingDef, System.String, Int32, RimWorld.FilthSourceFlags)")
                    {
                        found_call = true;
                    }
                }
                //Triggers after the pop opcode (after generating filth in c#).
                else if (found_call)
                {
                    //Load pawn, call function to check if a mechbreeder, and skip past the part which does damage
                    yield return new CodeInstruction(OpCodes.Ldloc_0, null);
                    yield return new CodeInstruction(OpCodes.Call, ismechbreeder);
                    yield return new CodeInstruction(OpCodes.Brtrue_S, skip_label);
                    found_call = false;
                }
            }
            yield break;
        }
    }
}