namespace RJW_Genes
{
    public class Gene_TightFemaleGenitalia : Gene_GenitaliaResizingGene
    {
        public override void Resize()
        {
            SizeAdjuster.AdjustAllVaginaSizes(pawn, 0.0f, 0.5f);
        }
    }
}