using Verse;

namespace RJW_Genes
{
    public class Gene_SmallBreasts : Gene_GenitaliaResizingGene
    {
        public override void Resize()
        {
            if (GenitaliaUtility.ShouldHaveBreasts(this.pawn))
                SizeAdjuster.AdjustAllBreastSizes(pawn, 0.0f, 0.5f);
        }
    }
}
