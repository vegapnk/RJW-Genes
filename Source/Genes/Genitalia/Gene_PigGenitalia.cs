using Verse;
using rjw;

namespace RJW_Genes
{
    public class Gene_PigGenitalia : RJW_Gene
    {
        public override void PostMake()
        {
            base.PostMake();

            GenitaliaChanger.ChangeGenitalia(this.pawn, Genital_Helper_2.PigPenis, Genital_Helper.average_vagina, Genital_Helper.average_anus);
        }

        public override void PostAdd()
        {
            base.PostAdd();
            GenitaliaChanger.ChangeGenitalia(this.pawn, Genital_Helper_2.PigPenis, Genital_Helper.average_vagina, Genital_Helper.average_anus);
        }
    }

}
