namespace RJW_Genes
{
    public class Gene_LooseAnus : RJW_Gene
    {

        public override void PostMake()
        {
            base.PostMake();

            SizeAdjuster.AdjustAllAnusSizes(pawn, 0.5f, 1.0f);
        }
        
        public override void PostAdd()
        {
            base.PostAdd();
            SizeAdjuster.AdjustAllAnusSizes(pawn, 0.5f, 1.0f);
        }



    }
}
