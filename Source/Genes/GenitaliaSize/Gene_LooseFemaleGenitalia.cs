namespace RJW_Genes
{
    public class Gene_LooseFemaleGenitalia : RJW_Gene
    {

        public override void PostMake()
        {
            base.PostMake();

            SizeAdjuster.AdjustAllVaginaSizes(pawn, 0.5f, 1.0f);
        }
        
        public override void PostAdd()
        {
            base.PostAdd();
            SizeAdjuster.AdjustAllVaginaSizes(pawn, 0.5f, 1.0f);
        }



    }
}
