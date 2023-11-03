﻿using HarmonyLib;
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
    public class Patch_HormonalSaliva
    {
        // TODO: Reduce to 0.02 after debug. 
        const float SIZE_INCREMENT_FALLBACK = 0.02f;
        const float MAX_BODY_SIZE_FALLBACK = 2.5f;
        const float CUM_MULTIPLIER_FALLBACK = 1.05f;

        public static void Postfix(SexProps props)
        {
            if (props == null || props.pawn == null || props.partner == null || props.partner.IsAnimal())
            {
                return;
            }

            Pawn pawn = props.pawn;
            Pawn partner = props.partner;


            if (GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_hormonal_saliva) && (props.sexType == xxx.rjwSextype.Oral || props.sexType == xxx.rjwSextype.Sixtynine || props.sexType == xxx.rjwSextype.Fellatio))
            {
                GrowPenisses(partner);
            }

            if (GeneUtility.HasGeneNullCheck(partner, GeneDefOf.rjw_genes_hormonal_saliva) && (props.sexType == xxx.rjwSextype.Oral || props.sexType == xxx.rjwSextype.Sixtynine || props.sexType == xxx.rjwSextype.Fellatio))
            {
                GrowPenisses(pawn);
            }

        }

        private static void GrowPenisses(Pawn pawn)
        {

            HormonalSalivaExtension salivaExt = GeneDefOf.rjw_genes_hormonal_saliva.GetModExtension<HormonalSalivaExtension>();

            float size_increment = salivaExt?.sizeIncrement ?? SIZE_INCREMENT_FALLBACK;
            float maximum_body_size = salivaExt?.maxBodySize ?? MAX_BODY_SIZE_FALLBACK;
            float cum_multiplier = salivaExt?.cumMultiplier ?? CUM_MULTIPLIER_FALLBACK;


            List<Hediff> AllPenisses = Genital_Helper.get_AllPartsHediffList(pawn).FindAll(x => Genital_Helper.is_penis(x));
            foreach (Hediff penis in AllPenisses)
            {
                CompHediffBodyPart CompHediff = penis.TryGetComp<rjw.CompHediffBodyPart>();
                if (penis.Severity < 1.00)
                {
                    penis.Severity = Math.Min(1.01f, penis.Severity + size_increment);
                }
                else
                {
                    if (CompHediff != null && CompHediff.SizeOwner <= maximum_body_size)
                    {
                        CompHediff.SizeOwner += size_increment;
                    }
                }

                // Increase Fluid
                if (CompHediff != null)
                    CompHediff.FluidAmmount *= cum_multiplier;
            }
        }

    }
}
