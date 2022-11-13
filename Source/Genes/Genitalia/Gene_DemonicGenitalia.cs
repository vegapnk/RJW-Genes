using Verse;
using RimWorld;
using rjw;

namespace RJW_Genes
{
    public class Gene_DemonicGenitalia : Gene
    {
        public override void PostMake()
        {
            base.PostMake();
            Sexualizer.sexualize_pawn(pawn);
            GenitaliaChanger.changeGenitalia(this.pawn,Genital_Helper.demon_penis,Genital_Helper.demon_vagina,Genital_Helper.demon_anus);
        }

        public override void PostAdd()
        {
            base.PostMake();
            GenitaliaChanger.changeGenitalia(this.pawn, Genital_Helper.demon_penis, Genital_Helper.demon_vagina, Genital_Helper.demon_anus);
        }
    }

}
