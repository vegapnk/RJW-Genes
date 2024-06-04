using HarmonyLib;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using static rjw.Dialog_Sexcard;

namespace RJW_Genes
{
    [HarmonyPatch(typeof(JobDriver_Sex), "SexTick")]
    public static class Patch_DangerousGenitalia
    {

        const float BASE_DAMAGE = 1.5f;
        const float BASE_APPLICATION_CHANCE = 1.0f;
        const float BASE_CHANCE_FOR_INNER_DAMAGE = 0.3f;
        const float THRESHOLD_FOR_INNER_DAMAGE = 2.0f;

        public static void Postfix(JobDriver_Sex __instance)
        {
            if (__instance == null || __instance.Sexprops == null) return;
            // Below automatically checks that not both pawns have dangerous genitalia
            Pawn dangerousGenitaliaPawn = ExactlyOnePawnHasDangerousGenitalia(__instance.Sexprops.pawn, __instance.Sexprops.partner);
            if (dangerousGenitaliaPawn == null) return;
            Pawn fuckedPawn = GetPawnThatHasNotTheDangerousGenitalia(__instance.Sexprops.pawn, __instance.Sexprops.partner);

            if (dangerousGenitaliaPawn.IsHashIntervalTick(__instance.ticks_between_thrusts)) {

                if ((new System.Random()).NextDouble() < BASE_APPLICATION_CHANCE)
                {

                    // TODO: Setting only for Rape
                    // TODO: Testing
                    // Open Issues: 
                    // Currently, if a "Dangerous Vagina" Pawn "Reverse rapes", the vagina pawn gets damaged.
                    // I think there needs to be a check and logic for isReverse ... 

                    ModLog.Message($"Test - Tick {__instance.Sexprops.pawn} <> {__instance.Sexprops.partner} --- Thrust");
                    ApplyDangerousGenitaliaDamage(dangerousGenitaliaPawn, fuckedPawn, __instance.Sexprops);
                }
            }
        }

        private static void ApplyDangerousGenitaliaDamage(Pawn damager, Pawn damaged, SexProps props)
        {
            DamageWorker DamageWorker = new DamageWorker_AddInjury();
            bool damagerIsMaleOrFuta = GenderUtility.IsMale(damager) || Genital_Helper.has_penis_fertile(damager) || Genital_Helper.has_penis_infertile(damager);

            float penetrator_genitalsize = GetBiggestGenitalSize(damager);
            float penetrator_bodysize = GetBodySize(damager, GetBiggestGenital(damager, penis: damagerIsMaleOrFuta));

            switch (props.sexType)
            {
                case xxx.rjwSextype.Oral:
                case xxx.rjwSextype.Fellatio:
                case xxx.rjwSextype.Cunnilingus:
                    {
                        float penetrated_bodysize = GetBodySize(damaged);
                        float damage = CalculateSizeRelatedDamage(penetrator_bodysize, penetrator_genitalsize, penetrated_bodysize, 0.0f);
                        bool allow_for_inner_damage = damage >= THRESHOLD_FOR_INNER_DAMAGE;

                        DamageInfo dInfo = new DamageInfo(
                            VariousDefOf.rjw_genes_dangerous_genitalia_damage,
                            damage,
                            instigator: damager, category: DamageInfo.SourceCategory.ThingOrUnknown,
                            hitPart: GetRandomOralBodyPartRecord(damaged, allow_for_inner_damage));
                        DamageWorker.Apply(dInfo, damaged);
                    } break;


                case xxx.rjwSextype.Vaginal:
                case xxx.rjwSextype.Scissoring:
                    {
                        Hediff vagina = Genital_Helper.get_AllPartsHediffList(damaged).FirstOrDefault(part => Genital_Helper.is_vagina(part));
                        if (vagina == null) return;
                        CompHediffBodyPart comps = vagina.TryGetComp<rjw.CompHediffBodyPart>();
                        float penetrated_bodysize = GetBodySize(damaged, vagina);
                        float damage = CalculateSizeRelatedDamage(penetrator_bodysize, penetrator_genitalsize, penetrated_bodysize, vagina.Severity);
                        bool allow_for_inner_damage = damage >= THRESHOLD_FOR_INNER_DAMAGE;

                        DamageInfo dInfo = new DamageInfo(
                            VariousDefOf.rjw_genes_dangerous_genitalia_damage,
                            damage,
                            instigator: damager, category: DamageInfo.SourceCategory.ThingOrUnknown,
                            hitPart: GetRandomGenitalBodyPartRecord(damaged, allow_for_inner_damage));
                        DamageWorker.Apply(dInfo, damaged);
                    } break;

                case xxx.rjwSextype.Anal:
                    {
                        Hediff anus = Genital_Helper.get_AllPartsHediffList(damaged).FirstOrDefault(part => Genital_Helper.is_anus(part));
                        if (anus == null) return;
                        CompHediffBodyPart comps = anus.TryGetComp<rjw.CompHediffBodyPart>();
                        float penetrated_bodysize = GetBodySize(damaged, anus);
                        float damage = CalculateSizeRelatedDamage(penetrator_bodysize, penetrator_genitalsize, penetrated_bodysize, anus.Severity);
                        bool allow_for_inner_damage = damage >= THRESHOLD_FOR_INNER_DAMAGE;

                        DamageInfo dInfo = new DamageInfo(
                            VariousDefOf.rjw_genes_dangerous_genitalia_damage,
                            damage,
                            instigator: damager, category: DamageInfo.SourceCategory.ThingOrUnknown,
                            hitPart: GetRandomAnalBodyPartRecord(damaged, allow_for_inner_damage));
                        DamageWorker.Apply(dInfo, damaged);
                    } break;

                default: return;
            }
        }

        /// <summary>
        /// Checks both pawns for their genes, 
        /// and returns the pawn that has the rjw_genes_dangerous_genitalia def. 
        /// Returns null if both or none have it.
        /// </summary>
        private static Pawn ExactlyOnePawnHasDangerousGenitalia(Pawn pawnA, Pawn pawnB)
        {
            if (pawnA == null) return null;
            if (pawnB == null) return null;

            bool pawnAHasDangerousGen = pawnA.genes != null && pawnA.genes.HasActiveGene(GeneDefOf.rjw_genes_dangerous_genitalia);
            bool pawnBHasDangerousGen = pawnB.genes != null && pawnB.genes.HasActiveGene(GeneDefOf.rjw_genes_dangerous_genitalia);

            if (pawnAHasDangerousGen && pawnBHasDangerousGen)
                return null;
            else if (!pawnAHasDangerousGen && !pawnBHasDangerousGen)
                return null;
            else if (pawnAHasDangerousGen)
            {
                return pawnA;
            } else if(pawnBHasDangerousGen)
            {
                return pawnB;
            }

            return null;
        }

        // This method assumes that the pawn is the penetrator - the biggest penis-severity is returned.
        private static float GetBiggestGenitalSize(Pawn pawn)
        {
            float best = 0.0f;
            var parts = Genital_Helper.get_AllPartsHediffList(pawn);

            foreach (var part in parts)
            {
                if (Genital_Helper.is_sex_part(part) && Genital_Helper.is_penis(part)) {
                    best = part.Severity > best ? part.Severity : best;
                }
            }

            return best;
        }

        /// <summary>
        /// Calculates the damage that will be applied when penetrated based on the genital and body sizes. 
        /// This is done by a simple multiplier on the base damaged, based on the absolute differences in sizes.
        /// </summary>
        /// <param name="penetrator_bodysize">The bodysize of the pawn, or, if applicable, the bodysize of the damaging genital.</param>
        /// <param name="penetrator_genitalsize">The severity of the penetrating body-part</param>
        /// <param name="penetrated_bodysize">The bodysize of the penetrated, or, if applicable, the bodysize of the damaged genital.</param>
        /// <param name="penetrated_genitalsize">The severity of the penetrated body-part, 0 for oral.</param>
        /// <returns>The damage applied to a bodypart-record, 0.0 if there is no damage to be done.</returns>
        private static float CalculateSizeRelatedDamage(float penetrator_bodysize, float penetrator_genitalsize, float penetrated_bodysize, float penetrated_genitalsize=0.0f)
        {
            float diff_in_bodysize = penetrator_bodysize - penetrated_bodysize;
            float diff_in_genital_size = penetrator_genitalsize - penetrated_genitalsize;

            float damage_multiplier = 1.0f + diff_in_bodysize + diff_in_genital_size;

            if (damage_multiplier > 0.5f)
                return BASE_DAMAGE * damage_multiplier;
            // If the size differences are too big, but the penetrator is the small one, do not deal damage.
            else
                return 0.0f;
        }

        /// <summary>
        /// Returns the pawn that does not have the gene for dangerous genitalia. 
        /// Returns null if neither or both have it.
        /// </summary>
        private static Pawn GetPawnThatHasNotTheDangerousGenitalia(Pawn a, Pawn b)
        {
            Pawn withGenitalia = ExactlyOnePawnHasDangerousGenitalia(a,b);

            if (withGenitalia == null) return null;
            else if (a == withGenitalia) return b; 
            else if (b == withGenitalia) return a;
            else return null;
        }

        /// <summary>
        /// Returns the biggest (by bodysize, then by severity) Genital.
        /// Biggest mean highest severity for penisses, and lowest for vaginas.
        /// </summary>
        /// <param name="pawn">The pawn for whom to look up.</param>
        /// <param name="penis">Whether to look for penisses - this should be true for males and futas.</param>
        /// <returns>Largest Penis or Tightest Vagina. Null in case of errors / non genitals. </returns>
        private static Hediff GetBiggestGenital(Pawn pawn, bool penis = true) {
            Hediff best = null;
            var parts = Genital_Helper.get_AllPartsHediffList(pawn);

            if (penis)
            {
                foreach (var part in parts)
                {
                    if (Genital_Helper.is_penis(part))
                    {
                        if (best == null) best = part;
                        CompHediffBodyPart CompHediff = part.TryGetComp<rjw.CompHediffBodyPart>();
                        CompHediffBodyPart CompHediffBest = best.TryGetComp<rjw.CompHediffBodyPart>();

                        if (CompHediffBest.SizeOwner > CompHediff.SizeOwner)
                        {
                            best = part;
                        } else if (CompHediffBest.SizeOwner == CompHediff.SizeOwner) {
                            best = part.Severity > best.Severity? part : best;
                        }
                    }
                }
            } else
            {
                foreach (var part in parts)
                {
                    if (Genital_Helper.is_vagina(part))
                    {
                        if (best == null) best = part;
                        CompHediffBodyPart CompHediff = part.TryGetComp<rjw.CompHediffBodyPart>();
                        CompHediffBodyPart CompHediffBest = best.TryGetComp<rjw.CompHediffBodyPart>();

                        if (CompHediffBest.SizeOwner < CompHediff.SizeOwner)
                        {
                            best = part;
                        }
                        else if (CompHediffBest.SizeOwner == CompHediff.SizeOwner)
                        {
                            best = part.Severity < best.Severity ? part : best;
                        }
                    }
                }
            }

            return best;
        }

        /// <summary>
        /// Gets the (relevant) bodysize of a pawn or it's genital. 
        /// "Null" as genital input is meant for a fallback, or for oral. It will return the pawns bodysize. 
        /// </summary>
        private static float GetBodySize(Pawn pawn, Hediff genital = null)
        {
            if (pawn == null) return 0;

            if (genital == null) return pawn.BodySize;

            if(rjw.Genital_Helper.is_sex_part(genital)){
                CompHediffBodyPart CompHediff = genital.TryGetComp<rjw.CompHediffBodyPart>();
                if (CompHediff != null)
                {
                    return CompHediff.SizeOwner;
                }
            }
            // Fallback!
            return 0.0f;
        }


        private static BodyPartRecord GetRandomAnalBodyPartRecord(Pawn pawn, bool InnerOrgansPossible = false)
        {
            if (pawn == null) return null;
            if (InnerOrgansPossible && (new System.Random()).NextDouble() < 1 - BASE_CHANCE_FOR_INNER_DAMAGE)
                return rjw.Genital_Helper.get_torsoBPR(pawn);

            return Genital_Helper.get_anusBPR(pawn);
        }

        private static BodyPartRecord GetRandomOralBodyPartRecord(Pawn pawn, bool InnerOrgansPossible = false)
        {
            if (pawn == null) return null;
            if (InnerOrgansPossible && (new System.Random()).NextDouble() < 1 - BASE_CHANCE_FOR_INNER_DAMAGE)
                return rjw.Genital_Helper.get_torsoBPR(pawn);

            return Genital_Helper.get_mouthBPR(pawn);
        }

        private static BodyPartRecord GetRandomGenitalBodyPartRecord(Pawn pawn, bool InnerOrgansPossible = false)
        {
            if (pawn == null) return null;
            if (InnerOrgansPossible && (new System.Random()).NextDouble() < 1 - BASE_CHANCE_FOR_INNER_DAMAGE)
                return rjw.Genital_Helper.get_torsoBPR(pawn);

            return Genital_Helper.get_genitalsBPR(pawn);
        }
    }

}
