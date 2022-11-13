using Verse;
using rjw;

namespace RJW_Genes
{
    public class Gene_HumanGenitalia : Gene
    {
        public override void PostMake()
        {
            base.PostMake();
            Sexualizer.sexualize_pawn(pawn);
            GenitaliaChanger.changeGenitalia(this.pawn,Genital_Helper.average_penis,Genital_Helper.average_vagina,Genital_Helper.average_anus);
        }

        public override void PostAdd()
        {
            base.PostAdd();
            GenitaliaChanger.changeGenitalia(this.pawn, Genital_Helper.average_penis, Genital_Helper.average_vagina, Genital_Helper.average_anus);
        }

    }

}
