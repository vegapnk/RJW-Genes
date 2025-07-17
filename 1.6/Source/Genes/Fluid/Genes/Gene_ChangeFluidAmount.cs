namespace RJW_Genes
{
    public class Gene_ChangeFluidAmount : RJW_Gene
    {
        bool has_been_fired = false;


        public override void PostMake()
        {
            base.PostMake();

            float multipier = FluidUtility.LookupFluidMultiplier(this);
            FluidUtility.MultiplyFluidAmountBy(pawn, multipier);
            has_been_fired = true;
        }

        public override void PostAdd()
        {
            base.PostAdd();
            if (!has_been_fired)
            {
                float multipier = FluidUtility.LookupFluidMultiplier(this);
                FluidUtility.MultiplyFluidAmountBy(pawn, multipier);
                has_been_fired = true;
            }
        }

        public override void PostRemove()
        {
            base.PostAdd();

            if (has_been_fired)
            {
                float multipier = FluidUtility.LookupFluidMultiplier(this);
                FluidUtility.MultiplyFluidAmountBy(pawn, 1/ multipier);
                has_been_fired = false;
            }
        }

    }
}
