using Verse;

namespace RJW_Genes
{
    public class Gene_LooseAnus : Gene_GenitaliaResizingGene
    {
        public override void Resize()
        {
            var bounds = this.GetResizingBounds();
            SizeAdjuster.AdjustAllAnusSizes(pawn, bounds.Item1, bounds.Item2);
        }
    }
}
