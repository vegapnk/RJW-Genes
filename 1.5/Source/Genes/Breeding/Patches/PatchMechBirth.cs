

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            MethodInfo ismechbreeder = AccessTools.Method(typeof(GeneUtility), "IsMechbreeder");
            foreach (CodeInstruction codeInstruction in instructions)
            {
                yield return codeInstruction;
                //find the call to source.Any()
                if (codeInstruction.opcode == OpCodes.Call)
                {
                    if (codeInstruction.operand.ToString() == "Boolean Any[BodyPartRecord](System.Collections.Generic.IEnumerable`1[Verse.BodyPartRecord])")
                    {
                        //Load pawn, call function to check if a mechbreeder, reverse result, call and instruction
                        yield return new CodeInstruction(OpCodes.Ldloc_0);
                        yield return new CodeInstruction(OpCodes.Call, ismechbreeder);
                        yield return new CodeInstruction(OpCodes.Ldc_I4_0);
                        yield return new CodeInstruction(OpCodes.Ceq);
                        yield return new CodeInstruction(OpCodes.And);
                        
                    }
                }

            }
            yield break;
        }
    }
}