using HarmonyLib;
using RimWorld;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{

    [HarmonyPatch(typeof(SexUtility), "Aftersex")]
    public class Patch_AfterSexUtility_ApplyGeneticInfectors
    {

        const int FACTION_GOODWILL_CHANGE = -3;

        public static void Postfix(SexProps props)
        {
            if (props == null || props.pawn == null || props.partner == null) return;

            Pawn pawn = props.pawn;
            Pawn partner = props.partner;

            if (pawn == partner) return;
            if (pawn.IsAnimal() || partner.IsAnimal()) return;
            if (pawn.genes == null || partner.genes == null) return;
            // No Infections on Condom Use
            if (props.usedCondom) return;

            // Exit early if settings require penetrative sex, but this is not penetrative sex
            if (!DiseaseHelper.IsPenetrativeSex(props) && RJW_Genes_Settings.rjw_genes_genetic_disease_spread_only_on_penetrative_sex) return;

            TryApplyGeneticInfections(pawn, partner);
            TryApplyGeneticInfections(partner, pawn);
        }

        private static void TryApplyGeneticInfections(Pawn infector, Pawn partner)
        {
            foreach (GeneDef infectorGeneDef in DiseaseHelper.GetGeneticInfectorGenes(infector))
            {
                GeneticInfectorExtension diseaseExt = infectorGeneDef.GetModExtension<GeneticInfectorExtension>();
                if (diseaseExt == null) continue;
                float application_chance = diseaseExt.infectionChance;

                foreach (GeneDef diseaseGeneDef in DiseaseHelper.LookupInfectionGeneDefs(diseaseExt))
                {
                    DiseaseHelper.TryStoreGeneticDiseaseInCarrier(diseaseGeneDef, partner);
                    if (DiseaseHelper.IsImmuneAgainstGeneticDisease(partner, diseaseGeneDef)) 
                        continue;

                    if ((new Random()).NextDouble() < application_chance)
                    {
                        partner.genes.AddGene(diseaseGeneDef, !RJW_Genes_Settings.rjw_genes_genetic_disease_as_endogenes);
                        FactionUtility.HandleFactionGoodWillPenalties(infector, partner, "rjw_genes_GoodwillChangedReason_infected_with_disease",FACTION_GOODWILL_CHANGE);
                    }
                }
            }
        }

    }
}
