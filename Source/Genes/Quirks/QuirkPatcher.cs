using Verse;
using RimWorld;
using rjw;
using System.Collections.Generic;
using rjw.Modules.Quirks;
using System;

namespace RJW_Genes
{
    public class QuirkPatcher
    {
        /// <summary>
        /// This Patch is applied after the quirk-satisfaction and checks 
        /// a) which quirks can the sex-partner satisfy?
        /// b) which of the quirks has the pawn?
        /// 
        /// The result of the Satisfied is an integer, based on the original framework.
        /// The framework "just checks" the number of satisfied quirks - we increase this number with this postfix.
        /// </summary>
        public static void CountSatisfiedPostfix(ref int __result, SexProps props)
        {
            if (props == null) return;
            Pawn pawn = props.pawn;
            Pawn partner = props.partner;
            if (partner == null  || pawn == null) return;
            if(!pawn.IsHuman()||!partner.IsHuman()) return;
            
            List<string> potentiallySatisfiedQuirks = new List<string>();
            foreach (Gene gene in partner.genes.GenesListForReading)
            {
                if (partner.genes.HasActiveGene(gene.def))
                {
                    string satisfiable_quirk = gene.def?.GetModExtension<QirkExtension>()?.Satisfiedquirk;
                    if (!string.IsNullOrEmpty(satisfiable_quirk))
                    {
                        potentiallySatisfiedQuirks.Add(satisfiable_quirk);
                    }
                }
            }

            int QuirksSatisfiedByGenes = -1;

            foreach (Quirk quirk in Quirk.All)
            {
                if (pawn.Has(quirk))
                {
                    foreach (string satisfiableQuirk in potentiallySatisfiedQuirks)
                    {
                        if (!string.IsNullOrEmpty(satisfiableQuirk) && quirk.LocaliztionKey == satisfiableQuirk)
                        {
                            QuirksSatisfiedByGenes++;
                            Quirk.AddThought(pawn);
                        }
                    }
                }
            }

            if(QuirksSatisfiedByGenes > 0)
                __result = __result + QuirksSatisfiedByGenes;
            return;
        }

    }
}
