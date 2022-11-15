using Verse;
using rjw;
using System;

namespace RJW_Genes
{
    public class Gene_BigBreasts : Gene
    {

        public override void PostMake()
        {
            base.PostMake();
            if (GenitaliaUtility.PawnStillNeedsGenitalia(pawn))
                Sexualizer.sexualize_pawn(pawn);

            if (pawn.gender == Gender.Female)
                SizeAdjuster.AdjustAllBreastSizes(pawn,0.5f,1.0f);
        }
        
        public override void PostAdd()
        {
            base.PostAdd();
            if (pawn.gender == Gender.Female)
                SizeAdjuster.AdjustAllBreastSizes(pawn, 0.5f, 1.0f);
        }


    }
}
