namespace RJW_Genes
{
    public class Gene_BigMaleGenitalia : Gene_GenitaliaResizingGene
    {
        public override void Resize()
        {
            SizeAdjuster.AdjustAllPenisSizes(pawn, 0.5f, 1.0f);
        }
    }
}