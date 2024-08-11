using HarmonyLib;
using RimWorld;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace RJW_Genes
{

    /*
     * Once per day (or slightly different per configuration) checks if the pawn changes gender. 
     * At the triggered tick, there is a random chance to change gender. 
     * This will swap genitalia, appearance and breasts. 
     * 
     * For some situations, the pawn better not change genitalia, e.g. while having vaginal sex. This would throw errors.
     * For these cases a check is implemented, and if there was a block the change happens a bit later when "unblocked". 
     * 
     * TODO: Keep pregnancies. 
     * The pregnancies remove their things on Tick at the end, which kills it for male pawns.
     * This seems to be an upstream RJW thing, but needs a bit of investigation.
     */
    public class Gene_GenderFluid : RJW_Gene
    {

        //public const int CHANGE_INTERVAL_FALLBACK = 1000; // Test value for Quick Trials
        const int CHANGE_INTERVAL_FALLBACK = 60000; // 60k == 1 day
        const float SWITCH_CHANCE_FALLBACK = 0.25f;

        int change_interval;
        float switch_chance;

        List<Hediff> storedBreasts = new List<Hediff>();

        private bool sexChangeWasBlocked = false;

        public Gene_GenderFluid() : base() {
            TickBasedChanceExtension tickbasedChanceExt = GeneDefOf.rjw_genes_gender_fluid.GetModExtension<TickBasedChanceExtension>();
            change_interval = tickbasedChanceExt?.tickInterval ?? CHANGE_INTERVAL_FALLBACK;
            switch_chance = tickbasedChanceExt?.eventChance ?? SWITCH_CHANCE_FALLBACK;
        }

        public override void Tick()
        {
            base.Tick();

            // Case 1: We had a blocked SexChange, now Pawn is free, apply sexchange a bit delayed.
            if (pawn.IsHashIntervalTick(1500) && sexChangeWasBlocked && !SexChangeBlocked(pawn)){
                ChangeSex();
                sexChangeWasBlocked = false;
            }
            // Case 2: Check every interval if the Chance triggers
            else if (pawn.IsHashIntervalTick(change_interval) && (new Random()).NextDouble() < switch_chance)
            {
                
                // Case 2.A) SexChange was blocked, postpone it
                if (SexChangeBlocked(pawn))
                {
                    sexChangeWasBlocked |= true;
                    return;
                } 
                // Case 2.B) Nothing blocking, change the sex.
                else { ChangeSex();}
            }

        }

        private void ChangeSex()
        {
            if (rjw.Genital_Helper.is_futa(pawn))
            {
                // Handle Futa Pawns - Keep Genitalia as is, just change RW Gender
                pawn.gender = pawn.gender == Gender.Male? Gender.Female: Gender.Male;
            }
            // Handle Non-Futas - Change Genitalia and Store breasts. 
            else
            {
                if (pawn.gender == Gender.Female)
                {
                    if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"genderfluid pawn {pawn} is changing from female to male");
                    SwitchToMale();
                }
                else if (pawn.gender == Gender.Male)
                {
                    if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"genderfluid pawn {pawn} is changing from male to female");
                    SwitchToFemale();
                }
            }
            GenderUtility.RemoveAllSexChangeThoughts(pawn);
        }

        private void SwitchToFemale()
        {
            // Change Drawing 
            GenderUtility.AdjustBodyToTargetGender(pawn, Verse.Gender.Female);
            // Change Gender 
            pawn.gender = Verse.Gender.Female;

            // Switch Penisses to Vaginas
            var genitalsToRemove = pawn.GetGenitalsList().FindAll(g => Genital_Helper.is_penis(g) || Genital_Helper.is_vagina(g));
            foreach (var genital  in genitalsToRemove)
            {
                var genitaliaHediffDef = GenitaliaUtility.GetVaginaForGene(GenitaliaUtility.GetGenitaliaTypeGeneForPawn(pawn));
                float size = genital.Severity;
                pawn.health.RemoveHediff(genital);

                var newVagina = HediffMaker.MakeHediff(genitaliaHediffDef, pawn, Genital_Helper.get_genitalsBPR(pawn));
                pawn.health.AddHediff(newVagina);
                newVagina.Severity = size;
            }

            SwitchBreasts();
        }

        private void SwitchToMale()
        {
            // Change Drawing 
            GenderUtility.AdjustBodyToTargetGender(pawn, Verse.Gender.Male);
            // Change Gender 
            pawn.gender = Verse.Gender.Male;

            // Switch Vaginas to Penisses
            var genitalsToRemove = pawn.GetGenitalsList().FindAll(g => Genital_Helper.is_penis(g) || Genital_Helper.is_vagina(g));
            foreach (var genital in genitalsToRemove)
            {
                var genitaliaHediffDef = GenitaliaUtility.GetPenisForGene(GenitaliaUtility.GetGenitaliaTypeGeneForPawn(pawn));
                float size = genital.Severity;
                pawn.health.RemoveHediff(genital);

                var newPenis = HediffMaker.MakeHediff(genitaliaHediffDef, pawn, Genital_Helper.get_genitalsBPR(pawn));
                pawn.health.AddHediff(newPenis);
                newPenis.Severity = size;
            }

            SwitchBreasts();

        }


        private void SwitchBreasts()
        {
            List<Hediff> current_breasts  = pawn.GetBreastList();

            // Stored_Breasts can be empty when the pawn first ever switches gender!
            if (storedBreasts.NullOrEmpty())
            {
                foreach (var breasts in current_breasts)
                {
                    // Is Male, and does not have the "no breast gene"
                    if (pawn.gender == Gender.Male && !GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_no_breasts))
                    {
                        storedBreasts.Add(CreateNewBreasts());
                    } 
                    else if (pawn.gender == Gender.Female && !GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_featureless_chest) )
                    {
                        storedBreasts.Add(CreateNewBreasts());
                    }
                }
            }


            foreach (var breast in current_breasts)
            {
                pawn.health.RemoveHediff(breast);
            }
            foreach (var breast in storedBreasts)
            {
                pawn.health.AddHediff(breast);
            }

            storedBreasts.Clear();
            storedBreasts.AddRange(current_breasts);
        }


        internal Hediff CreateNewBreasts()
        {
            var correctGene = GenitaliaUtility.GetGenitaliaTypeGeneForPawn(pawn);
            var breastDef = GenitaliaUtility.GetBreastsForGene(correctGene);
            var partBPR = Genital_Helper.get_breastsBPR(pawn);
            var additional_breasts = HediffMaker.MakeHediff(breastDef, pawn,partBPR);

            var CompHediff = additional_breasts.TryGetComp<rjw.HediffComp_SexPart>();
            if (CompHediff != null)
            {
                CompHediff.Init(pawn);
                CompHediff.UpdateSeverity();
            }

            return additional_breasts;
        }

        /// <summary>
        /// Checks the pawn if it has any of the vagina-related hediffs (e.g. stretched) and removes them.
        /// Anal Soreness, Stretching etc. remains. 
        /// </summary>
       


        /// <summary>
        /// There are some actions that block sex change, 
        /// being drafted or having sex. 
        /// </summary>
        /// <param name="pawn">The pawn that want to sexchange.</param>
        /// <returns>False if the SexChange is applicable, True if there needs to be a wait timer.</returns>
        internal bool SexChangeBlocked(Pawn pawn)
        {
            // DEVNOTE: This list might extend on new cases, thus the explicit method.
            return pawn == null
                || pawn.health.Dead
                || (pawn.jobs.curDriver is JobDriver_Masturbate)
                || (pawn.jobs.curDriver is JobDriver_Sex)
                || (pawn.jobs.curDriver is JobDriver_SexBaseReciever)
                || (pawn.jobs.curDriver is JobDriver_SexBaseInitiator)
                || (pawn.jobs.curDriver is JobDriver_JoinInBed)
                
                || (pawn.jobs.curDriver is JobDriver_SexQuick)
                || (pawn.jobs.curDriver is JobDriver_SexBaseRecieverQuickie)

                || (pawn.jobs.curDriver is JobDriver_Knotted)
                || (pawn.jobs.curDriver is JobDriver_Mate)
                || (pawn.jobs.curDriver is JobDriver_Mating)
                || (pawn.jobs.curDriver is JobDriver_Breeding)

                || (pawn.jobs.curDriver is JobDriver_Rape)
                || (pawn.jobs.curDriver is JobDriver_SexBaseRecieverRaped)
                || (pawn.jobs.curDriver is JobDriver_RandomRape)
                || (pawn.jobs.curDriver is JobDriver_RapeComfortPawn)
                || (pawn.jobs.curDriver is JobDriver_RapeEnemy)
                || pawn.jobs.curDriver is JobDriver_Lovin

                // This is a heavy check, but this is necessary because sometimes the pawns go somewhere to have sex and then they start despite missing genitalia!
                || (pawn.jobs.curDriver is JobDriver_Goto)

                || pawn.Drafted;
        }
    }
}
