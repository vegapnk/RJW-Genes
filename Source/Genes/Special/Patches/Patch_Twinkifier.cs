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
            switch (hediff.SeverityLabel)
            {
                case "severe":
                case "critical":
                    {
                        if (Random.NextDouble() < MAJOR_APPLICATION_CHANCE)
                            majorChange(pawn);
                    } break;
                case "minor":
                    {
                        if (Random.NextDouble() < MINOR_APPLICATION_CHANCE)
                            minorChange(pawn);
                    } break;
            }

        }

        private static void minorChange(Pawn pawn)
        {
            // Minor Infectious Vulnerability
            // Smaller Genitalia 
            // Remove Beard 
            // Thin Body Type
        }

        private static void majorChange(Pawn pawn)
        {
            // Final Gene-Pool should have: 
            // - Fragile (?)
            // - Infectious Vulnerability
            // - Infectious Homosexuality
            // - Beauty
            // - Fertile Anus 

            pawn.genes.AddGene(GeneDefOf.rjw_genes_fertile_anus, !RJW_Genes_Settings.rjw_genes_genetic_disease_as_endogenes);
            pawn.genes.AddGene(GeneDefOf.rjw_genes_infectious_homosexuality, !RJW_Genes_Settings.rjw_genes_genetic_disease_as_endogenes);
        }
    }
}
