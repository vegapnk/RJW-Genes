namespace RJW_Genes
{
    public class Gene_ChangeCumAmount : RJW_Gene
    {
        bool has_been_fired = false;


        public override void PostMake()
        {
            base.PostMake();

            float multipier = CumUtility.LookupCumMultiplier(this);
            CumUtility.MultiplyFluidAmountBy(pawn, multipier);
            has_been_fired = true;
        }

        public override void PostAdd()
        {
            base.PostAdd();
            if (!has_been_fired)
            {
                float multipier = CumUtility.LookupCumMultiplier(this);
                CumUtility.MultiplyFluidAmountBy(pawn, multipier);
                has_been_fired = true;
            }
        }

        public override void PostRemove()
        {
            base.PostAdd();

            if (has_been_fired)
            {
                float multipier = CumUtility.LookupCumMultiplier(this);
                CumUtility.MultiplyFluidAmountBy(pawn, 1/ multipier);
                has_been_fired = false;
            }
        }

    }
}
