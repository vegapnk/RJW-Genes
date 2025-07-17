namespace RJW_Genes
{
    public class Gene_BigMaleGenitalia : Gene_GenitaliaResizingGene
    {
        public override void Resize()
        {
            var bounds = this.GetResizingBounds();
            SizeAdjuster.AdjustAllPenisSizes(pawn, bounds.Item1, bounds.Item2);
        }
    }
}