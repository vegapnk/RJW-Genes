namespace RJW_Genes
{
    public class Gene_BigMaleGenitalia : RJW_Gene
    {

        public override void PostMake()
        {
            base.PostMake();

            SizeAdjuster.AdjustAllPenisSizes(pawn,0.5f,1.0f);
        }
        
        public override void PostAdd()
        {
            base.PostAdd();
            SizeAdjuster.AdjustAllPenisSizes(pawn, 0.5f, 1.0f);
        }

    }
}
