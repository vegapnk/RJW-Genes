namespace RJW_Genes
{
    public class Gene_SmallMaleGenitalia : RJW_Gene
    {

        public override void PostMake()
        {
            base.PostMake();

            SizeAdjuster.AdjustAllPenisSizes(pawn,0.0f,0.5f);
        }
        
        public override void PostAdd()
        {
            base.PostAdd();
            SizeAdjuster.AdjustAllPenisSizes(pawn, 0.0f, 0.5f);
        }

    }
}
