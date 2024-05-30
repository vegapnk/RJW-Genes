using Verse;
using RimWorld;
using rjw;
using System.Collections.Generic;
using rjw.Modules.Quirks;

namespace RJW_Genes
{
    public class QuirkPatcher
    {
        public static void CountSatisfiedPostfix(ref int __result, SexProps props)
        {
            Pawn pawn = props.pawn;
            Pawn partner = props.partner;
            int count= 0;
            List<Quirk> listquirk= new List<Quirk>();

            foreach(Gene g in partner.genes.GenesListForReading) {
                if (partner.genes.HasActiveGene(g.def))
                {
                    listquirk.Add(g.def?.GetModExtension<QirkExtension>().Satisfiedquirk);
                }
            }

            foreach (Quirk q in listquirk)
            {
                if (pawn.Has(q)){
                    count++;
                }
            }
            __result = __result + count;
            return;
        }

    }
}
