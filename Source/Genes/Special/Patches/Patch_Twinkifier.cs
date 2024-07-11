using HarmonyLib;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    /// <summary>
    /// This patch handles the changes produced by `rjw_genes_twinkifier`.
    /// It requires the hediff `rjw_genes_twinkification_in_progress` which is managed separately, in `Patch_HediffIncreaseOnSex`. 
    /// </summary>
    [HarmonyPatch(typeof(SexUtility), "Aftersex")]
    public static class Patch_Twinkifier
    {
        const float MINOR_APPLICATION_CHANCE = 0.25f; // = 25% to have a minor transformation
        const float MAJOR_APPLICATION_CHANCE = 0.10f; // = 10% to have a major transformation

        public static void Postfix(SexProps props)
        {
            if (props == null || props.pawn == null || !props.hasPartner() || props.partner == null)
                return;
            if (props.pawn.IsAnimal() || props.partner.IsAnimal())
                return;

            ApplyTwinkification(props.pawn);
            ApplyTwinkification(props.partner);
        }

        private static void ApplyTwinkification(Pawn pawn)
        {
            if (pawn == null) return;
            Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.rjw_genes_twinkification_progress); 
            if (hediff == null) return;

            var Random = new Random();
            // DevNote: I first had a switch (hediff.SeverityLabel) but SeverityLabel was null.
            // So now I have this approach which feels a bit more robust. 
            // I was thinking about looking for strings in the label, but I think that will break the logic in case of translations.
            switch (hediff.Severity)
            {
                case float f when f > 0.8f:
                    {
                        if (Random.NextDouble() < MAJOR_APPLICATION_CHANCE)
                            MajorChange(pawn);
                    } break;
                case float f when f > 0.6f:
                    {
                        if (Random.NextDouble() < MINOR_APPLICATION_CHANCE)
                            MinorChange(pawn);
                    } break;
                default:
                    {
                        ModLog.Debug($"Tried to twinkify {pawn} - severity of twinkification was too low ({hediff.def} @ {hediff.Severity} - {hediff.Label})") ;
                    } break;
            }

        }

        private static void MinorChange(Pawn pawn)
        {
            List<GeneDef> possibleGenes = new List<GeneDef>() {
                GeneDefOf.rjw_genes_small_male_genitalia,
                DefDatabase<GeneDef>.GetNamed("Beard_NoBeardOnly"),
                DefDatabase<GeneDef>.GetNamed("Body_Thin"),
                GeneDefOf.rjw_genes_homosexual
            };

            GeneDef chosen = possibleGenes.RandomElement();
            if (chosen == null)
            {
                ModLog.Warning($"Error in retrieving a minor-twinkification gene for twinkifying {pawn}");
                return;
            }

            // DevNote: I could do "hasActiveGene" but that could lead to the gene being there but not active. 
            if (!pawn.genes.GenesListForReading.Any(p => p.def == chosen))
            {
                ModLog.Debug($"{pawn} experienced a minor twinkification change; {pawn} got new gene {chosen}.");
                pawn.genes.AddGene(chosen, !RJW_Genes_Settings.rjw_genes_genetic_disease_as_endogenes);
            } else
            {
                ModLog.Debug($"Tryed a minor twinkification for {pawn} - {pawn} already had {chosen}");
            }
        }

        private static void MajorChange(Pawn pawn)
        {
            List<GeneDef> possibleGenes = new List<GeneDef>() {
                GeneDefOf.rjw_genes_fertile_anus,
                DefDatabase<GeneDef>.GetNamed("Beauty_Pretty"),
                DefDatabase<GeneDef>.GetNamed("Delicate"),
                GeneDefOf.rjw_genes_minor_vulnerability, 
                GeneDefOf.rjw_genes_infectious_homosexuality
            };

            GeneDef chosen = possibleGenes.RandomElement();
            if (chosen == null)
            {
                ModLog.Warning($"Error in retrieving a minor-twinkification gene for twinkifying {pawn}");
                return;
            }

            // DevNote: I could do "hasActiveGene" but that could lead to the gene being there but not active. 
            if (!pawn.genes.GenesListForReading.Any(p => p.def == chosen))
            {
                ModLog.Debug($"{pawn} experienced a major twinkification change; {pawn} got new gene {chosen}.");
                pawn.genes.AddGene(chosen, !RJW_Genes_Settings.rjw_genes_genetic_disease_as_endogenes);
            }
            else
            {
                ModLog.Debug($"Tryed a major twinkification for {pawn} - {pawn} already had {chosen}");
                ModLog.Debug($"Trying minor twinkification for {pawn} instead ...");
                MinorChange(pawn);
            }
        }
    }
}
