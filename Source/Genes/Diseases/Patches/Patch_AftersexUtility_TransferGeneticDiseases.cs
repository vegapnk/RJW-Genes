using HarmonyLib;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes.Genes.Diseases.Patches
{
    [HarmonyPatch(typeof(SexUtility), "Aftersex")]
    public class Patch_AftersexUtility_TransferGeneticDiseases
    {

        public static void Postfix(SexProps props)
        {
            if (!RJW_Genes_Settings.rjw_genes_genetic_disease_spread) return;
            
            if (props == null || props.pawn == null || props.partner == null) return;

            Pawn pawn = props.pawn;
            Pawn partner = props.partner;

            if (pawn == partner) return;
            if (pawn.IsAnimal() || partner.IsAnimal()) return;
            if (pawn.genes == null || partner.genes == null) return;
            // No Infections on Condom Use
            if (props.usedCondom) return;

            // Exit early if settings require penetrative sex, but this is not penetrative sex
            if (!IsPenetrativeSex(props) && RJW_Genes_Settings.rjw_genes_genetic_disease_spread_only_on_penetrative_sex) return;

            ModLog.Debug($"Firing Patch_TransferGeneticDiseases for {pawn} and {partner}");
            TryTransferGeneticDiseases(pawn, partner, props);
            TryTransferGeneticDiseases(partner, pawn, props);
        }

        private static void TryTransferGeneticDiseases(Pawn infector, Pawn infected, SexProps props)
        {
            if (infected.genes.HasActiveGene(GeneDefOf.rjw_genes_genetic_disease_immunity))
            {
                ModLog.Debug($"{infected} is immune to genetic diseases");
                return;
            }

            foreach (GeneDef disease in GetGeneticDiseaseGenes(infector)) {
                ModLog.Debug($"Found genetic disease {disease} in {infector}, trying to infect {infected}");

                if (infected.genes.HasActiveGene(disease))
                    continue;

                if ((new Random()).NextDouble() <= LookupDiseaseInfectionChance(disease))
                {
                    infected.genes.AddGene(disease, !RJW_Genes_Settings.rjw_genes_genetic_disease_as_endogenes);
                }
            }
        }

        private static List<GeneDef> GetGeneticDiseaseGenes(Pawn pawn)
        {
            if (pawn != null && pawn.genes != null)
            {
                return pawn.genes
                    .GenesListForReading
                    .ConvertAll(gene => gene.def)
                    .Where(genedef => pawn.genes.HasActiveGene(genedef))
                    .Where(IsGeneticDiseaseGene)
                    .ToList();
            }

            return new List<GeneDef>() { };
        }

        private static bool IsPenetrativeSex(SexProps props)
        {
            if (props == null) return false;

            return props.sexType ==
                xxx.rjwSextype.Vaginal
                || props.sexType == xxx.rjwSextype.Anal
                || props.sexType == xxx.rjwSextype.Oral
                || props.sexType == xxx.rjwSextype.DoublePenetration
                || props.sexType == xxx.rjwSextype.Fellatio
                || props.sexType == xxx.rjwSextype.Sixtynine;
        }

        private static bool IsGeneticDiseaseGene(GeneDef geneDef)
        {
            return geneDef.geneClass.FullName.Contains("DiseaseGene");
        }

        private static float LookupDiseaseInfectionChance(GeneDef geneDef)
        {
            if (IsGeneticDiseaseGene(geneDef))
            {
                GeneticDiseaseExtension diseaseExt = geneDef.GetModExtension<GeneticDiseaseExtension>();
                return diseaseExt != null ? diseaseExt.infectionChance : 0.0f;
            } 
            else 
                return 0.0f;
        }
    }
}
