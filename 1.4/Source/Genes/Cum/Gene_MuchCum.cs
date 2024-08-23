namespace RJW_Genes
{
    public class Gene_MuchCum : RJW_Gene
    {
        bool has_been_fired = false;

        float multiplier_much_cum = 3f;

        public override void PostMake()
        {
            base.PostMake();

            CumUtility.MultiplyFluidAmountBy(pawn, multiplier_much_cum);
            has_been_fired = true;
        }

        public override void PostAdd()
        {
            base.PostAdd();
            if (!has_been_fired) { 
                CumUtility.MultiplyFluidAmountBy(pawn, multiplier_much_cum); 
                has_been_fired = true;
            }
        }


        public override void PostRemove()
        {
            base.PostAdd();

            if (has_been_fired)
            {
                CumUtility.MultiplyFluidAmountBy(pawn, 1/multiplier_much_cum);
                has_been_fired = false;
            }
        }

    }
}
