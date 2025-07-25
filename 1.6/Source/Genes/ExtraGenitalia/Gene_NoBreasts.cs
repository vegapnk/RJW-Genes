﻿using Verse;
using rjw;
using RimWorld;

namespace RJW_Genes
{
    public class Gene_NoBreasts : RJW_Gene
    {
        Hediff breastsToShrink;
        internal float oldSize = -1f;

        public override void PostMake()
        {
            base.PostMake();
            
            // Breasts are removed for female pawns!
            if (GenderUtility.IsFemale(pawn) && oldSize < 0)
            {
                RemoveButStoreBreasts();
            }
        }
        
        public override void PostAdd()
        {
            if (pawn.kindDef == null) return;   //Added to catch Rimworld creating statues of pawns.
            base.PostAdd();

            // Breasts are removed for female pawns!
            if (GenderUtility.IsFemale(pawn) && oldSize < 0)
            {
                RemoveButStoreBreasts();
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();
            // Re-Add the old breasts
            if (oldSize != -1f)
                breastsToShrink.Severity = oldSize;
            else
                breastsToShrink.Severity = 0f;
        }

        internal void RemoveButStoreBreasts()
        {
            var partBPR = Genital_Helper.get_breastsBPR(pawn);
            breastsToShrink = Genital_Helper.get_AllPartsHediffList(pawn).FindLast(x => GenitaliaUtility.IsBreasts(x));

            if(breastsToShrink != null)
            {
                oldSize = breastsToShrink.Severity;
                //pawn.health.RemoveHediff(breastsToRemove);
                breastsToShrink.Severity = 0f;
            }
        }

    }
}
