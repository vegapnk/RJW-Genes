using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using Verse;
using rjw;

namespace RJW_BGS
{
    [HarmonyPatch(typeof(ParentRelationUtility))]
    public class PatchGetParents
    {
        // Token: 0x0600000F RID: 15
        [HarmonyPostfix]
        [HarmonyPatch("GetFather")]
        private static void FatherPostfix(ref Pawn __result, Pawn pawn)
        {
            if (__result == null && pawn.RaceProps.IsFlesh && pawn.relations != null)
            {
                List<DirectPawnRelation> directRelations = pawn.relations.DirectRelations;
                bool flag = false;
                for (int i = 0; i < directRelations.Count; i++)
                {
                    DirectPawnRelation directPawnRelation = directRelations[i];
                    if (directPawnRelation.def == PawnRelationDefOf.Parent)
                    {
                        if (flag)
                        {
                            __result = directPawnRelation.otherPawn;
                            return;
                        }
                        flag = true;
                    }
                }
            }
        }

        // Token: 0x06000010 RID: 16
        [HarmonyPostfix]
        [HarmonyPatch("GetMother")]
        private static void MotherPostfix(ref Pawn __result, Pawn pawn)
        {
            if (__result == null && pawn.RaceProps.IsFlesh && pawn.relations != null)
            {
                List<DirectPawnRelation> directRelations = pawn.relations.DirectRelations;
                for (int i = 0; i < directRelations.Count; i++)
                {
                    DirectPawnRelation directPawnRelation = directRelations[i];
                    if (directPawnRelation.def == PawnRelationDefOf.Parent)
                    {
                        __result = directPawnRelation.otherPawn;
                        return;
                    }
                }
            }
        }

        // Token: 0x0600001F RID: 31
        [HarmonyPostfix]
        [HarmonyPatch("HasSameFather")]
        private static void HasSameFatherPostfix(ref bool __result, Pawn pawn, Pawn other)
        {
            if (!__result && pawn.RaceProps.IsFlesh && pawn.relations != null)
            {
                Pawn parent = pawn.GetFather();
                Pawn parent2 = other.GetMother();
                Pawn parent3 = other.GetFather();
                Pawn parent4 = pawn.GetMother();
                if (parent != null && parent2 != null && parent == parent2)
                {
                    __result = true;
                    return;
                }
                if (parent3 != null && parent4 != null && parent3 == parent4)
                {
                    __result = true;
                    return;
                }
                if (parent != null && parent3 != null && parent == parent3)
                {
                    __result = true;
                    return;
                }
                if (parent2 != null && parent4 != null && parent2 == parent4)
                {
                    __result = true;
                    return;
                }
            }
        }

        // Token: 0x06000020 RID: 32
        [HarmonyPostfix]
        [HarmonyPatch("HasSameMother")]
        private static void HasSameMotherPostfix(ref bool __result, Pawn pawn, Pawn other)
        {
            if (!__result && pawn.RaceProps.IsFlesh && pawn.relations != null)
            {
                Pawn parent = pawn.GetFather();
                Pawn parent2 = other.GetMother();
                Pawn parent3 = other.GetFather();
                Pawn parent4 = pawn.GetMother();
                if (parent != null && parent2 != null && parent == parent2)
                {
                    __result = true;
                    return;
                }
                if (parent3 != null && parent4 != null && parent3 == parent4)
                {
                    __result = true;
                    return;
                }
                if (parent != null && parent3 != null && parent == parent3)
                {
                    __result = true;
                    return;
                }
                if (parent2 != null && parent4 != null && parent2 == parent4)
                {
                    __result = true;
                    return;
                }
            }
        }
    }
}
