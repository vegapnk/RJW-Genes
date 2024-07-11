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
    /// This patch handles the changes produced by `rjw_genes_feminizer`.
    /// It requires the hediff `rjw_genes_feminzation_in_progress` which is managed separately, in `Patch_HediffIncreaseOnSex`. 
    /// </summary>
    [HarmonyPatch(typeof(SexUtility), "Aftersex")]
    public static class Patch_Feminizer
    {
        const float MINOR_APPLICATION_CHANCE = 0.25f; // = 25% to have a minor transformation
        const float MAJOR_APPLICATION_CHANCE = 0.10f; // = 10% to have a major transformation

        public static void Postfix(SexProps props)
        {
            if (props == null || props.pawn == null || !props.hasPartner() || props.partner == null)
                return;
            if (props.pawn.IsAnimal() || props.partner.IsAnimal())
                return;

            ApplyFeminization(props.pawn);
            ApplyFeminization(props.partner);
        }

        private static void ApplyFeminization(Pawn pawn)
        {
            if (pawn == null) return;
            Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.rjw_genes_feminization_progress); 
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
                        ModLog.Debug($"Tried to feminize {pawn} - severity of feminization was too low ({hediff.def} @ {hediff.Severity} - {hediff.Label})") ;
                    } break;
            }

        }

        private static void MinorChange(Pawn pawn)
        {
            List<GeneDef> possibleGenes = new List<GeneDef>() {
                GeneDefOf.rjw_genes_small_male_genitalia,
                GeneDefOf.rjw_genes_big_breasts,
                GeneDefOf.rjw_genes_no_cum,
                DefDatabase<GeneDef>.GetNamed("Beard_NoBeardOnly"),
                DefDatabase<GeneDef>.GetNamed("Hair_LongOnly")
            };

            GeneDef chosen = possibleGenes.RandomElement();
            if (chosen == null)
            {
                ModLog.Warning($"Error in retrieving a minor-feminization gene for feminizing {pawn}");
                return;
            }

            // DevNote: I could do "hasActiveGene" but that could lead to the gene being there but not active. 
            if (!pawn.genes.GenesListForReading.Any(p => p.def == chosen))
            {
                ModLog.Debug($"{pawn} experienced a minor feminization change; {pawn} got new gene {chosen}.");
                pawn.genes.AddGene(chosen, !RJW_Genes_Settings.rjw_genes_genetic_disease_as_endogenes);
            } else
            {
                ModLog.Debug($"Tryed a minor feminization for {pawn} - {pawn} already had {chosen}");
            }
        }

        private static void MajorChange(Pawn pawn)
        {
            List<GeneDef> possibleGenes = new List<GeneDef>() {
                GeneDefOf.rjw_genes_female_only,
                GeneDefOf.rjw_genes_no_penis,
                GeneDefOf.rjw_genes_minor_vulnerability, 
            };

            GeneDef chosen = possibleGenes.RandomElement();
            if (chosen == null)
            {
                ModLog.Warning($"Error in retrieving a minor-feminization gene for feminizing {pawn}");
                return;
            }

            // DevNote: I could do "hasActiveGene" but that could lead to the gene being there but not active. 
            if (!pawn.genes.GenesListForReading.Any(p => p.def == chosen))
            {
                ModLog.Debug($"{pawn} experienced a major feminization change; {pawn} got new gene {chosen}.");
                pawn.genes.AddGene(chosen, !RJW_Genes_Settings.rjw_genes_genetic_disease_as_endogenes);
            }
            else
            {
                ModLog.Debug($"Tryed a major feminization for {pawn} - {pawn} already had {chosen}");
                ModLog.Debug($"Trying minor feminization for {pawn} instead ...");
                MinorChange(pawn);
            }
        }
    }
}
