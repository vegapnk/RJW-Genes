using Verse;
using rjw;

namespace RJW_Genes
{
    /// <summary>
    /// Dummy Gene that does not alter the genitalia size. Normal RJW Logic and rolled sizes are kept.
    /// </summary>
    public class Gene_NormalBreasts : Gene
    {
        
        public override void PostMake()
        {
            base.PostMake();
            if (GenitaliaUtility.PawnStillNeedsGenitalia(pawn))
                Sexualizer.sexualize_pawn(pawn);
        }

    }
}
