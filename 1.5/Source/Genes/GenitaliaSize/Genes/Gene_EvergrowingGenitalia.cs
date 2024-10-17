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
                if (CompHediff.baseSize <= 1.00f)
                    CompHediff.baseSize += 0.10f;
                else
                {
                    if (CompHediff.bodySizeOverride <= 1.0) CompHediff.bodySizeOverride = 1.0f;
                    CompHediff.bodySizeOverride += 0.05f;
                }
                CompHediff.UpdateSeverity();

                if (CompHediff.bodySizeOverride > 3.0f)
                {
                    // Add Mental Hediff 
                    HandleGenitaliaSizeThoughts(pawn);
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
                if (CompHediff.baseSize <= 1.00f)
                    CompHediff.baseSize += 0.10f;
                else
                {
                    if (CompHediff.bodySizeOverride <= 1.0) CompHediff.bodySizeOverride = 1.0f;
                    CompHediff.bodySizeOverride += 0.05f;
                }
                    //CompHediff.ForceSize(CompHediff.Size + 0.05f);
                    //CompHediff.originalOwnerSize += 0.05f;
                CompHediff.UpdateSeverity();

                if (CompHediff.bodySizeOverride > 3.0f)
                {
                    // Add Mental Hediff 
                    HandleGenitaliaSizeThoughts(pawn);
                }
                // Increase Fluid
                if (CompHediff != null)
                    CompHediff.partFluidFactor *= 1.025f;
            }
        }

        private void HandleGenitaliaSizeThoughts(Pawn pawn)
        {
            Hediff sizeThought = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.rjw_genes_evergrowth_sideeffect);

            if (sizeThought != null)
            {
                sizeThought.Severity += 0.025f;
            }
            else
            {
                sizeThought = HediffMaker.MakeHediff(HediffDefOf.rjw_genes_evergrowth_sideeffect, pawn);
                sizeThought.Severity = 0.1f;
                pawn.health.AddHediff(sizeThought);

                if (!xxx.is_nympho(pawn))
                {
                    pawn.story.traits.GainTrait(new Trait(xxx.nymphomaniac));
                }
            }
        }

    }
}