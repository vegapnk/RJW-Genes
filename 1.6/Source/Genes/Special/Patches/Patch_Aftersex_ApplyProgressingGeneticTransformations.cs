using HarmonyLib;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;
using Verse;

namespace RJW_Genes
{
    /// <summary>
    /// This patch handles the changes caused by transformation genes.
    /// It requires a transformation progress hediff managed by "HediffIncreaseOnSexExtension" and a set of genes, managed by GeneAlteringExtension.
    /// Otherwise, it will not operate.
    /// 
    /// See #145 and the introducing PR #150. Thanks Archer. 
    /// </summary>
    [HarmonyPatch(typeof(SexUtility), "Aftersex")]
    public static class Patch_Aftersex_ApplyProgressingGeneticTransformations
    {
        public static List<GeneDef> GetGeneticTransformationGenes(Pawn pawn)
        {
            if (pawn != null && pawn.genes != null)
            {
                return pawn.genes
                    .GenesListForReading
                    .ConvertAll(gene => gene.def)
                    .Where(genedef => pawn.genes.HasActiveGene(genedef))
                    .Where(IsGeneAlteringGene)
                    .Where(IsHediffIncreaseOnSexGene)
                    .ToList();
            }
            return new List<GeneDef>() { };
        }
        public static bool IsGeneAlteringGene(GeneDef geneDef)
        {
            if (geneDef == null) return false;
            GeneAlteringExtension geneAlteringExtension = geneDef.GetModExtension<GeneAlteringExtension>();
            return geneAlteringExtension != null;
        }

        public static bool IsHediffIncreaseOnSexGene(GeneDef geneDef)
        {
            if (geneDef == null) return false;
            HediffIncreaseOnSexExtension hediffOnSexExt = geneDef.GetModExtension<HediffIncreaseOnSexExtension>();
            return hediffOnSexExt != null;
        }
        
        public static void Postfix(SexProps props)
        {
            if (props == null || props.pawn == null || !props.hasPartner() || props.partner == null)
                return;
            if (props.pawn.IsAnimal() || props.partner.IsAnimal())
                return;

            foreach (GeneDef geneDef in GetGeneticTransformationGenes(props.pawn))
            {
                ApplyTransformation(props.partner, geneDef);
            }
            foreach (GeneDef geneDef in GetGeneticTransformationGenes(props.partner))
            {
                ApplyTransformation(props.pawn, geneDef);
            }
        }

        public static void ApplyTransformation(Pawn pawn, GeneDef geneDef)
        {
            if (pawn == null || geneDef == null) return;
            var Random = new System.Random();

            GeneAlteringExtension geneAlteringExtension = geneDef.GetModExtension<GeneAlteringExtension>();
            if (geneAlteringExtension == null) return;

            HediffIncreaseOnSexExtension hediffOnSexExt = geneDef.GetModExtension<HediffIncreaseOnSexExtension>();
            if (hediffOnSexExt == null) return;

            Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(hediffOnSexExt.hediffDef);
            if (hediff == null) return;

            if (GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_genetic_disease_immunity)) return;

            ModLog.Debug($"Found transformation gene {geneDef} in {pawn}, trying to perform transformation");
            switch (hediff.Severity)
            {
                case float f when f > 0.8f:
                    {
                        if (Random.NextDouble() <= geneAlteringExtension.majorApplicationChance)
                            MajorChange(pawn, geneAlteringExtension, hediff.def.ToString());
                    }
                    break;
                case float f when f > 0.6f:
                    {
                        if (Random.NextDouble() <= geneAlteringExtension.minorApplicationChance)
                            MinorChange(pawn, geneAlteringExtension, hediff.def.ToString());
                    }
                    break;
                default:
                    {
                        ModLog.Debug($"Tried to transform {pawn} - severity of transformation was too low ({hediff.def} @ {hediff.Severity} - {hediff.Label})");
                    }
                    break;
            }
        }

        private static void MinorChange(Pawn pawn, GeneAlteringExtension geneAlteringExtension, string Source = null)
        {
            List<GeneDef> possibleGenes = geneAlteringExtension.minorGenes.ToList();

            GeneDef chosen = possibleGenes.RandomElement();
            if (chosen == null)
            {
                ModLog.Warning($"Error in retrieving a minor transformation gene for transforming {pawn} {(Source == null ? "" : $"(from {Source})")}");
                return;
            }

            if (!pawn.genes.GenesListForReading.Any(p => p.def == chosen))
            {
                List<Gene> genes = pawn.genes.GenesListForReading;
                genes = genes.Where(x => pawn.genes.HasActiveGene(x.def)).ToList();

                foreach (Gene gene in genes)
                {
                    ImmunityAgainstGenesExtension ext = gene.def.GetModExtension<ImmunityAgainstGenesExtension>();
                    if (ext != null)
                    {
                        foreach (string defname in ext.givesImmunityAgainst)
                            if (chosen.defName == defname)
                                return;
                    }
                }

                ModLog.Debug($"{pawn} experienced a minor transformation; {pawn} got new gene {chosen} {(Source == null ? "" : $"(from {Source})")}.");
                pawn.genes.AddGene(chosen, !RJW_Genes_Settings.rjw_genes_genetic_disease_as_endogenes);
            }

            else
            {
                ModLog.Debug($"Tried to do a minor transformation {(Source == null ? "" : $"(from {Source}) ")}for {pawn} - {pawn} already had {chosen} or has immunity against it");
            }
        }

        private static void MajorChange(Pawn pawn, GeneAlteringExtension geneAlteringExtension, string Source = null)
        {
            List<GeneDef> possibleGenes = geneAlteringExtension.majorGenes.ToList();

            GeneDef chosen = possibleGenes.RandomElement();
            if (chosen == null)
            {
                ModLog.Warning($"Error in retrieving a major transformation gene for transforming {pawn} {(Source == null ? "" : $"(from {Source})")}");
                return;
            }

            if (!pawn.genes.GenesListForReading.Any(p => p.def == chosen))
            {
                List<Gene> genes = pawn.genes.GenesListForReading;
                genes = genes.Where(x => pawn.genes.HasActiveGene(x.def)).ToList();

                foreach (Gene gene in genes)
                {
                    ImmunityAgainstGenesExtension ext = gene.def.GetModExtension<ImmunityAgainstGenesExtension>();
                    if (ext != null)
                    {
                        foreach (string defname in ext.givesImmunityAgainst)
                            if (chosen.defName == defname)
                                return;
                    }
                }

                ModLog.Debug($"{pawn} experienced a major transformation; {pawn} got new gene {chosen} {(Source == null ? "" : $"(from {Source})")}.");
                pawn.genes.AddGene(chosen, !RJW_Genes_Settings.rjw_genes_genetic_disease_as_endogenes);
            }

            else
            {
                ModLog.Debug($"Tried to do a major transformation{(Source == null ? "" : $" (from {Source})")} for {pawn} - {pawn} already had {chosen}. Trying minor transformation for {pawn} instead");
                MinorChange(pawn, geneAlteringExtension, Source+"-MajorChangeFailed");
            }
        }
    }
}