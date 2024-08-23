using Verse;

namespace RJW_Genes
{
    public class Gene_LooseFemaleGenitalia : Gene_GenitaliaResizingGene
    {
        public override void Resize()
        {
            SizeAdjuster.AdjustAllVaginaSizes(pawn, 0.5f, 1.0f);
        }
    }
}
