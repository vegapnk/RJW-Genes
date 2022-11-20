namespace RJW_Genes
{
    public class Gene_TightAnus : RJW_Gene
    {

        public override void PostMake()
        {
            base.PostMake();

            SizeAdjuster.AdjustAllAnusSizes(pawn, 0.0f, 0.5f);
        }
        
        public override void PostAdd()
        {
            base.PostAdd();
            SizeAdjuster.AdjustAllAnusSizes(pawn, 0.0f, 0.5f);
        }


    }
}
