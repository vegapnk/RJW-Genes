namespace RJW_Genes
{
    public class Gene_TightFemaleGenitalia : RJW_Gene
    {

        public override void PostMake()
        {
            base.PostMake();

            SizeAdjuster.AdjustAllVaginaSizes(pawn, 0.0f, 0.5f);
        }
        
        public override void PostAdd()
        {
            base.PostAdd();
            SizeAdjuster.AdjustAllVaginaSizes(pawn, 0.0f, 0.5f);
        }

    }
}
