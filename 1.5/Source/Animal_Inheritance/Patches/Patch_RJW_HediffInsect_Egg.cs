using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using rjw;

namespace RJW_BGS
{
    [HarmonyPatch(typeof(Hediff_InsectEgg), "GiveBirth")]
    public static class Patch_RJW_HediffInsect_Egg
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            MethodInfo newgenes = AccessTools.Method(typeof(InheritanceUtility), "NewGenes", null, null);
            FieldInfo implanter = AccessTools.Field(typeof(Hediff_InsectEgg), "implanter");
            FieldInfo father = AccessTools.Field(typeof(Hediff_InsectEgg), "father");

            foreach (CodeInstruction instruction in instructions)
            {
                yield return instruction;
                if (instruction.opcode == OpCodes.Call && instruction.operand.ToString() == "Void BabyPostBirth(Verse.Pawn, Verse.Pawn, Verse.Pawn)")
                {
                    yield return new CodeInstruction(OpCodes.Ldloc_0, null);
                    yield return new CodeInstruction(OpCodes.Ldfld, implanter);
                    yield return new CodeInstruction(OpCodes.Ldarg_0, null);
                    yield return new CodeInstruction(OpCodes.Ldfld, father);
                    yield return new CodeInstruction(OpCodes.Ldloc_1, null);
                    yield return new CodeInstruction(OpCodes.Call, newgenes);
                }
                
            }
        }
    }
}
