using rjw;
using Verse;

namespace RJW_Genes
{
    public class RJW_Gene : Gene
    {

        public override void PostMake()
        {
            base.PostMake();
            if (GenitaliaUtility.PawnStillNeedsGenitalia(pawn))
                Sexualizer.sexualize_pawn(pawn);
        }
    }
}
