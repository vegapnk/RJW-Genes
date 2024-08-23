using Verse;
namespace RJW_Genes
{
    public class Gene_BigBreasts : Gene_GenitaliaResizingGene
    {
        public override void Resize()
        {
            if (GenitaliaUtility.ShouldHaveBreasts(this.pawn))
                SizeAdjuster.AdjustAllBreastSizes(pawn, 0.5f, 1.0f);
        }
    }
}