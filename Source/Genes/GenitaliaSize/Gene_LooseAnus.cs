using Verse;

namespace RJW_Genes
{
    public class Gene_LooseAnus : Gene_GenitaliaResizingGene
    {
        public override void Resize()
        {
            SizeAdjuster.AdjustAllAnusSizes(pawn, 0.5f, 1.0f);
        }
    }
}
