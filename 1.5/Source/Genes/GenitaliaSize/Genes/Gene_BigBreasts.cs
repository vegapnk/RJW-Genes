using Verse;
namespace RJW_Genes
{
    public class Gene_BigBreasts : Gene_GenitaliaResizingGene
    {
        public override void Resize()
        {
            if (GenitaliaUtility.ShouldHaveBreasts(this.pawn))
            {
                var bounds = this.GetResizingBounds();
                SizeAdjuster.AdjustAllBreastSizes(pawn, bounds.Item1, bounds.Item2);
            }
        }
    }
}