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
        public static void CountSatisfiedPostfix(ref int __result, SexProps props)
        {
            Pawn pawn = props.pawn;
            Pawn partner = props.partner;
            int count = 0;
            List<string> listquirk = new List<string>();
            string s;
            foreach (Gene g in partner.genes.GenesListForReading)
            {
                if (partner.genes.HasActiveGene(g.def))
                {
                    s = null;
                    s = g.def?.GetModExtension<QirkExtension>()?.Satisfiedquirk;
                    if (!string.IsNullOrEmpty(s))
                    {
                        listquirk.Add(s);
                    }
                }
            }
            
            foreach (Quirk q in Quirk.All)
            {
                if (pawn.Has(q))
                {

                    foreach (string s2 in listquirk)
                    {
                        if (!string.IsNullOrEmpty(s2))
                            if (q.LocaliztionKey==s2)
                            {
                                count++;
                            }
                    }
                }
            }
            __result = __result + count;
            return;
        }

    }
}
