using Verse;
namespace RJW_Genes
{
    public class Gene_BigBreasts : Gene_GenitaliaResizingGene
    {
        public override void Resize()
        {
            if (pawn.gender == Gender.Female)
                SizeAdjuster.AdjustAllBreastSizes(pawn, 0.5f, 1.0f);
        }
    }
}