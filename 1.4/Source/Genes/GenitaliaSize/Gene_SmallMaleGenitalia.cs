namespace RJW_Genes
{
    public class Gene_SmallMaleGenitalia : Gene_GenitaliaResizingGene
    {
        public override void Resize()
        {
            SizeAdjuster.AdjustAllPenisSizes(pawn, 0.0f, 0.5f);
        }
    }
}