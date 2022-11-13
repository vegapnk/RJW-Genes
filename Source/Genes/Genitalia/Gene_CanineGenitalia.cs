using Verse;
using rjw;

namespace RJW_Genes
{
    public class Gene_CanineGenitalia : Gene
    {
        public override void PostMake()
        {
            base.PostMake();
            Sexualizer.sexualize_pawn(pawn);
            GenitaliaChanger.changeGenitalia(this.pawn,Genital_Helper.canine_penis,Genital_Helper.canine_vagina,Genital_Helper.generic_anus);
        }

        public override void PostAdd()
        {
            base.PostMake();
            GenitaliaChanger.changeGenitalia(this.pawn, Genital_Helper.canine_penis, Genital_Helper.canine_vagina, Genital_Helper.generic_anus);
        }
    }

}
