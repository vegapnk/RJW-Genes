using Verse;
using RimWorld;
using rjw;

namespace RJW_Genes
{
    public class Gene_ReptilianGenitalia : RJW_Gene
    {
        public override void PostMake()
        {
            base.PostMake();

            GenitaliaChanger.ChangeGenitalia(this.pawn,Genital_Helper.hemipenis, Genital_Helper.average_vagina, Genital_Helper.average_anus);
        }

        public override void PostAdd()
        {
            base.PostAdd();
            GenitaliaChanger.ChangeGenitalia(this.pawn, Genital_Helper.hemipenis, Genital_Helper.average_vagina, Genital_Helper.average_anus);
        }
    }

}
