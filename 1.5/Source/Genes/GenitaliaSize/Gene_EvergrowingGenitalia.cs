using Verse;
using RimWorld;
using rjw;
using System.Collections.Generic;
using System;

namespace RJW_Genes
{
    public class Gene_EvergrowingGenitalia : RJW_Gene
    {

        const int BASE_TICKS = 60000;

        public override void Tick()
        {
            base.Tick();

            int interval = ModExtensionHelper.GetTickIntervalFromModExtension(GeneDefOf.rjw_genes_evergrowth, ModExtensionHelper.GetTickIntervalFromModExtension(this.def, BASE_TICKS));
            if (pawn.IsHashIntervalTick(interval) 
                && this.pawn.Map != null 
                && pawn.ageTracker.AgeBiologicalYears >= RJW_Genes_Settings.rjw_genes_resizing_age)
            {
                GrowPenisses();
                GrowVaginas();
            }
        }

        private void GrowPenisses()
        {
            List<Hediff> AllPenisses = Genital_Helper.get_AllPartsHediffList(pawn).FindAll(x => Genital_Helper.is_penis(x));
            foreach(Hediff penis in AllPenisses)
            {
                HediffComp_SexPart CompHediff = penis.TryGetComp<rjw.HediffComp_SexPart>();
                if (penis.Severity < 1.00)
                {
                    penis.Severity = Math.Min(1.01f, penis.Severity + 0.05f);
                } else {
                    if (CompHediff != null)
                    {
                        CompHediff.originalOwnerSize += 0.015f;
                        if (CompHediff.originalOwnerSize > 3.0f)
                        {
                            // Add Mental Hediff 
                            HandleGenitaliaSizeThoughts(pawn);
                        }
                    }
                }

                // Increase Fluid
                if (CompHediff != null)
                    CompHediff.partFluidFactor *= 1.05f;
            }
        }

        private void GrowVaginas()
        {
            List<Hediff> AllVaginas = Genital_Helper.get_AllPartsHediffList(pawn).FindAll(x => Genital_Helper.is_vagina(x));
            foreach (Hediff vagina in AllVaginas)
            {
                HediffComp_SexPart CompHediff = vagina.TryGetComp<rjw.HediffComp_SexPart>();
                if (vagina.Severity < 1.00)
                {
                    vagina.Severity = Math.Min(1.01f, vagina.Severity + 0.05f);
                }
                else
                {
                    if (CompHediff != null)
                    {
                        CompHediff.originalOwnerSize += 0.015f;
                        if (CompHediff.originalOwnerSize > 3.0f)
                        {
                            // Add Mental Hediff 
                            HandleGenitaliaSizeThoughts(pawn);
                        }
                    }
                }

                // Increase Fluid
                if (CompHediff != null)
                    CompHediff.partFluidFactor *= 1.025f;
            }
        }

        private void HandleGenitaliaSizeThoughts(Pawn pawn)
        {
            Hediff hybridsThoughts = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.rjw_genes_evergrowth_sideeffect);

            if (hybridsThoughts != null)
            {
                hybridsThoughts.Severity += 0.025f;
            }
            else
            {
                hybridsThoughts = HediffMaker.MakeHediff(HediffDefOf.rjw_genes_evergrowth_sideeffect, pawn);
                hybridsThoughts.Severity = 0.1f;
                pawn.health.AddHediff(hybridsThoughts);

                if (!xxx.is_nympho(pawn))
                {
                    pawn.story.traits.GainTrait(new Trait(xxx.nymphomaniac));
                }
            }
        }

    }
}