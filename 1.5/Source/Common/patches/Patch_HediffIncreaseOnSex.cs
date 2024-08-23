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
    /// This patch "only" applies the hediff increase on sex. 
    /// It checks for the hediff, creates it if necessary and applicable, 
    /// and increases it based on the severity, chance and genders specified in the Extension. 
    /// 
    /// Some hediffs want follow up logic, e.g. the Feminization Gene, 
    /// which is handled in an extra patch (that requires the hediff to be present already). 
    /// </summary>
    [HarmonyPatch(typeof(SexUtility), "Aftersex")]
    public class Patch_HediffIncreaseOnSex
    {

        public static void Postfix(SexProps props)
        {
            if (props == null || props.pawn == null || props.partner == null || props.partner.IsAnimal() || props.pawn.IsAnimal())
            {
                return;
            }

            Pawn pawn = props.pawn;
            Pawn partner = props.partner;

            if (pawn.genes == null || partner.genes == null) return;

            TryApplyHediffsOfSex(pawn, partner, props);
            TryApplyHediffsOfSex(partner, pawn, props);
        }

        /// <summary>
        /// Checks for every gene in a pawn if it applies a hediff or the severity of an existing hediff in a sexual partner. 
        /// If the checks pass, the hediff is added or changed accordingly. 
        /// </summary>
        /// <param name="pawn">Pawn that holds (one or many) genes that might apply a hediff change</param>
        /// <param name="partner">Pawn that will receive or alter any hediffs, if applicable</param>
        /// <param name="props">The Sexprops, used for checking if sex is penetrative</param>
        static void TryApplyHediffsOfSex(Pawn pawn, Pawn partner, SexProps props)
        {
            var random = new Random();

            foreach (Gene gene in pawn.genes.GenesListForReading)
            {
                HediffIncreaseOnSexExtension ext = gene.def.GetModExtension<HediffIncreaseOnSexExtension>();
                if (ext == null) continue;

                if (DiseaseHelper.IsImmuneAgainstGeneticDisease(partner, gene.def)) continue;
                if (ext.requiresPenetrativeSex && DiseaseHelper.IsPenetrativeSex(props)) continue;
                if (!ext.applicableForMen && partner.gender == Gender.Male) continue;
                if (!ext.applicableForWomen && partner.gender == Gender.Female) continue;
                if (random.NextDouble() >= ext.applicationChance) continue;

                Hediff hediff = partner.health.hediffSet.GetFirstHediffOfDef(ext.hediffDef);
                if (hediff == null)
                {
                    if (!ext.canCreateHediff) continue;
                    hediff = partner.health.GetOrAddHediff(ext.hediffDef);
                    hediff.Severity = 0.01f;
                    ModLog.Debug($"{partner} got hediff {hediff.def.defName} from Patch_HediffIncreaseOnSex ({gene.def.defName}) upon sex with {pawn}");
                }
                float initial_severity = hediff.Severity;
                ModLog.Debug($"{partner}s hediff {hediff.def.defName} was changed by Patch_HediffIncreaseOnSex ({gene.def.defName}) upon sex with {pawn} [from {initial_severity} to {initial_severity + ext.severityIncrease}]");
                hediff.Severity += ext.severityIncrease;

                // DevNote: I also want to have "negative" hediff changes here, but I think its not necessary. Once the severity reaches 0, or below, the hediff should remove itself. 
            }
        }

    }
}
