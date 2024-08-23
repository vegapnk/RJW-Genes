namespace RJW_Genes
{
    public class Gene_NoCum : RJW_Gene
    {
        bool has_been_fired = false;


        public override void PostMake()
        {
            base.PostMake();

            CumUtility.MultiplyFluidAmountBy(pawn, 0f);
            has_been_fired = true;
        }

        public override void PostAdd()
        {
            base.PostAdd();
            if (!has_been_fired) { 
                CumUtility.MultiplyFluidAmountBy(pawn, 0f); 
                has_been_fired = true;
            }
        }


        public override void PostRemove()
        {
            // Cum Removal does not do at the moment :/ I would need to safe the old cum amount but I don't want to at the moment
            base.PostAdd();

        }

    }
}
