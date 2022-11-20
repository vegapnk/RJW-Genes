using Verse;
namespace RJW_Genes
{
    public class Gene_BigBreasts : RJW_Gene
    {

        public override void PostMake()
        {
            base.PostMake();

            if (pawn.gender == Gender.Female)
                SizeAdjuster.AdjustAllBreastSizes(pawn,0.5f,1.0f);
        }
        
        public override void PostAdd()
        {
            base.PostAdd();
            if (pawn.gender == Gender.Female)
                SizeAdjuster.AdjustAllBreastSizes(pawn, 0.5f, 1.0f);
        }


    }
}
