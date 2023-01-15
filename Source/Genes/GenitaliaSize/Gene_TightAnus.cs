namespace RJW_Genes
{
    public class Gene_TightAnus : Gene_GenitaliaResizingGene
    {
        public override void Resize()
        {
            SizeAdjuster.AdjustAllAnusSizes(pawn, 0.0f, 0.5f);
        }
    }
}