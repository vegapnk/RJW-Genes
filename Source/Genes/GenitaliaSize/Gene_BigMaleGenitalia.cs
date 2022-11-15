using Verse;
using rjw;
using System;

namespace RJW_Genes
{
    public class Gene_BigMaleGenitalia : Gene
    {

        public override void PostMake()
        {
            base.PostMake();
            if (GenitaliaUtility.PawnStillNeedsGenitalia(pawn))
                Sexualizer.sexualize_pawn(pawn);

            SizeAdjuster.AdjustAllPenisSizes(pawn,0.5f,1.0f);
        }
        
        public override void PostAdd()
        {
            base.PostAdd();
            SizeAdjuster.AdjustAllPenisSizes(pawn, 0.5f, 1.0f);
        }

    }
}
