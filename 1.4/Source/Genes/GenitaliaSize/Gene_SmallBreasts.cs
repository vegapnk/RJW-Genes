using Verse;

namespace RJW_Genes
{
    public class Gene_SmallBreasts : Gene_GenitaliaResizingGene
    {
        public override void Resize()
        {
            SizeAdjuster.AdjustAllBreastSizes(pawn, 0.0f, 0.5f);
        }
    }
}
