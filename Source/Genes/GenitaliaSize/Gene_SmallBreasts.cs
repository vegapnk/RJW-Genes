namespace RJW_Genes
{
    public class Gene_SmallBreasts : RJW_Gene
    {

        public override void PostMake()
        {
            base.PostMake();

            SizeAdjuster.AdjustAllBreastSizes(pawn, 0.0f, 0.5f);
        }
        
        public override void PostAdd()
        {
            base.PostAdd();
            SizeAdjuster.AdjustAllBreastSizes(pawn, 0.0f, 0.5f);
        }
    }
}
