using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using Verse;
using rjw;

namespace RJW_Genes
{

    public class PatchLitteredBirth
    {

        static Dictionary<string, LaborState> laborStateMap = new Dictionary<string, LaborState>();
        public static void Hediff_Labor_PostRemovedPostFix(ref Hediff_Labor __instance)
        {
            bool randomTwinsRoll;
            int totalBirths;
            bool laborStateIsNull = !laborStateMap.ContainsKey(__instance.pawn.ThingID);
            bool hasLitteredBirthsGene = __instance.pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_littered_births);

            // we'll never do additional processing if this is the guaranteed last birth (eg birth #4)
            if (!laborStateIsNull && laborStateMap.TryGetValue(__instance.pawn.ThingID).birthCount == 4)
            {
                return;
            }

            // For now, littered birth overrides ovary agitator and twin calculations, so if a LaborState already exists
            // with littered births gene, move on
            if (!laborStateIsNull && hasLitteredBirthsGene)
            {
                if (RJW_Genes_Settings.rjw_genes_detailed_debug)
                {
                    ModLog.Message("Found active LaborState and LitteredBirths gene - skipping additional Hediff_Labor_PostRemovedPostFix work");
                    ModLog.Message("Pawn: " + __instance.pawn.NameShortColored + " (" + __instance.pawn.ThingID + ")");
                    ModLog.Message("birthCount: " + laborStateMap.TryGetValue(__instance.pawn.ThingID).birthCount);
                }

                return;
            }

            // Make a new LaborState for the null case with littered births
            if (laborStateIsNull && hasLitteredBirthsGene)
            {
                ModLog.Message("Found littered births gene");
                int litteredBirthsTotalRoll = Rand.RangeInclusive(2, 4);
                laborStateMap.SetOrAdd(__instance.pawn.ThingID, new LaborState(__instance.pawn, litteredBirthsTotalRoll));
                return;
            }

            // Finally, regardless of littered births gene, we only want new state creation on
            // pawns that don't already have state, so return if state is !null (STATE SHOULD ALWAYS BE CLEANED IN LABORPUSHING POSTFIX)
            if (!laborStateIsNull)
            {
                if (RJW_Genes_Settings.rjw_genes_detailed_debug)
                {
                    ModLog.Warning("Labor state for pawn " + __instance.pawn.NameShortColored + " (" + __instance.pawn.ThingID + 
                        ") is not null despite all checks passing for determining first instance of Hediff_Labor - this warning should never occur, " +
                        "and may indicate a bug in Hediff_LaborPushing of lingering labor state from a previous pregnancy");
                }
                return;
            }

            // For everything else, we do random twin and OvaryAgitator handling
            // -------
            // If we fail a base chance twins roll, return without any additional processing and proceed with vanilla childbirth
            // Notes on rolls:
            // -> Chance without OvaryAgitator to have twins: 1%
            // -> Chance with OvaryAgitator to have twins: Guaranteed
            // ---> Chance with OvaryAgitator to have triplets (MUST HAVE SUCCEEDED TWINS ROLL): 50%
            // ---> Chance with OvaryAgitator to have quadruplets (MUST HAVE SUCCEEDED TRIPLETS ROLL): 10%
            // -> Chance with Littered Births gene: random between 2 and 4 (inclusive)
            randomTwinsRoll = Rand.Chance(0.01f);
            bool hasAgitator = __instance.pawn.health.hediffSet.HasHediff(HediffDef.Named("OvaryAgitator"));
            if (!randomTwinsRoll && !hasAgitator)
            {
                // We failed rolls, and we don't have an agitator - no additional processing, do vanilla single baby birth
                if (RJW_Genes_Settings.rjw_genes_detailed_debug)
                {
                    ModLog.Message("Inside Hediff_Labor_PostRemovedPostFix random twins check fail");
                    ModLog.Message("Pawn: " + __instance.pawn.NameShortColored);
                    ModLog.Message("Random twins roll outcome: " + randomTwinsRoll);
                    ModLog.Message("Has OvaryAgitator: " + hasAgitator);
                }
                return;
            }

            // Beyond this point, we can assume the pawn has an agitator
            totalBirths = 2;
            bool agitatorTriplets = Rand.Chance(0.5f);
            bool agitatorQuadruplets = Rand.Chance(0.1f);
            if (hasAgitator)
            {
                if (agitatorTriplets) totalBirths = 3;
                if (agitatorTriplets && agitatorQuadruplets) totalBirths = 4;
            }

            // Set new LaborState
            laborStateMap.Add(__instance.pawn.ThingID, new LaborState(__instance.pawn, totalBirths));
        }

        public static void Hediff_LaborPushing_PostRemovedPostFix(ref Hediff_LaborPushing __instance)
        {
            bool hasAgitator = __instance.pawn.health.hediffSet.HasHediff(HediffDef.Named("OvaryAgitator"));
            bool hasLitteredBirthsGene = __instance.pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_littered_births);
            bool laborStateIsNull = !laborStateMap.ContainsKey(__instance.pawn.ThingID);
            LaborState currentLaborState;
            laborStateMap.TryGetValue(__instance.pawn.ThingID, out currentLaborState);

            if (laborStateIsNull)
            {
                if (__instance.pawn.health.hediffSet.HasHediff(HediffDef.Named("Bioscaffold")))
                {
                    __instance.pawn.health.RemoveHediff(__instance.pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Bioscaffold));
                }
                return;
            }

            if (currentLaborState.birthTotal == currentLaborState.birthCount)
            {
                laborStateMap.Remove(__instance.pawn.ThingID);
                if (__instance.pawn.health.hediffSet.HasHediff(HediffDef.Named("Bioscaffold")))
                {

                    __instance.pawn.health.RemoveHediff(__instance.pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Bioscaffold));
                }
                return;
            }

            ((Hediff_Labor)__instance.pawn.health.AddHediff(RimWorld.HediffDefOf.PregnancyLabor)).SetParents(__instance.pawn, __instance.Father, PregnancyUtility.GetInheritedGeneSet(__instance.Father, __instance.pawn));
            currentLaborState.birthCount++;

            if (!hasAgitator && !hasLitteredBirthsGene)
            {
                if (RJW_Genes_Settings.rjw_genes_detailed_debug)
                {
                    ModLog.Message("Pawn " + __instance.pawn.NameShortColored + " (" + __instance.pawn.ThingID + ") is having random twins");
                }
                Find.LetterStack.ReceiveLetter("rjw_genes_twin_letter".Translate(), __instance.pawn.NameShortColored + " " + "rjw_genes_twin_letter_content".Translate(), 
                    LetterDefOf.AnotherBaby, __instance.pawn);
                return;
            }

            Find.LetterStack.ReceiveLetter("rjw_genes_another_baby_letter".Translate(), __instance.pawn.NameShortColored + " " + "rjw_genes_another_baby_letter_content".Translate(), 
                LetterDefOf.AnotherBaby, __instance.pawn);
        }
    }
}
