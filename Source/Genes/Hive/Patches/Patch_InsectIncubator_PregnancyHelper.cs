using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Verse;
using RimWorld;
using rjw;

namespace RJW_Genes
{
    /// <summary>
    /// This Class patches the RJW-DoEgg to allow up to MaxEggSizeMul times the original amount of eggs.
    /// This harmony patch was kindly provided by 'shabalox' https://github.com/Shabalox/RJW_Genes_Addons/
    /// 
    /// For Gene: rjw_genes_insectincubator 
    /// </summary>
    [HarmonyPatch(typeof(PregnancyHelper), "DoEgg")]
    static class Patch_InsectIncubator_PregnancyHelper
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            //MethodInfo isinsectincubator = AccessTools.Method(typeof(GeneUtility), "IsInsectIncubator");
            MethodInfo maxeggsizemul = AccessTools.Method(typeof(GeneUtility), "MaxEggSizeMul");
            FieldInfo partner = AccessTools.Field(typeof(SexProps), "partner");

            Label skiplabel = il.DefineLabel();
            bool finished = false;
            foreach (CodeInstruction codeInstruction in instructions)
            {
                if (!finished)
                {
                    if (codeInstruction.opcode == OpCodes.Ldc_R4 && codeInstruction.operand.ToString() == "0")
                    {
                        yield return new CodeInstruction(OpCodes.Ldarg_0, null);
                        yield return new CodeInstruction(OpCodes.Ldfld, partner);
                        //yield return new CodeInstruction(OpCodes.Call, isinsectincubator);
                        yield return new CodeInstruction(OpCodes.Callvirt, maxeggsizemul);
                        //yield return new CodeInstruction(OpCodes.Brfalse_S, skiplabel);
                        yield return new CodeInstruction(OpCodes.Ldloc_0, null);
                        //yield return new CodeInstruction(OpCodes.Ldc_R4, 2f);
                        yield return new CodeInstruction(OpCodes.Mul, null);
                        yield return new CodeInstruction(OpCodes.Stloc_0, null);
                        //codeInstruction.labels.Add(skiplabel);
                        finished = true;
                    }
                }
                yield return codeInstruction;
            }
        }
    }
}
