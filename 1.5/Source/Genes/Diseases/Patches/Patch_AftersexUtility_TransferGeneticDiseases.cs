using HarmonyLib;
using RimWorld;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    [HarmonyPatch(typeof(SexUtility), "Aftersex")]
    public class Patch_AftersexUtility_TransferGeneticDiseases
    {

        public const int FACTION_GOODWILL_CHANGE = -2;

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
            if (!DiseaseHelper.IsPenetrativeSex(props) && RJW_Genes_Settings.rjw_genes_genetic_disease_spread_only_on_penetrative_sex) return;

            //ModLog.Debug($"Firing Patch_TransferGeneticDiseases for {pawn} and {partner}");
            TryTransferGeneticDiseases(pawn, partner, props);
            TryTransferGeneticDiseases(partner, pawn, props);
        }

        private static void TryTransferGeneticDiseases(Pawn infector, Pawn infected, SexProps props)
        {
            foreach (GeneDef disease in DiseaseHelper.GetGeneticDiseaseGenes(infector)) {
                ModLog.Debug($"Found genetic disease {disease} in {infector}, trying to infect {infected}");

                DiseaseHelper.TryStoreGeneticDiseaseInCarrier(disease, infected);
                if (DiseaseHelper.IsImmuneAgainstGeneticDisease(infected,disease))
                    continue;

                if ((new Random()).NextDouble() <= DiseaseHelper.LookupDiseaseInfectionChance(disease))
                {
                    infected.genes.AddGene(disease, !RJW_Genes_Settings.rjw_genes_genetic_disease_as_endogenes);
                    FactionUtility.HandleFactionGoodWillPenalties(infector, infected, "rjw_genes_GoodwillChangedReason_spread_genetic_disease", FACTION_GOODWILL_CHANGE);
                }
            }
        }


    }
}
