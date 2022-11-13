using Verse;
using rjw;

namespace RJW_Genes
{
    public class Gene_OvipositorGenitalia : Gene
    {
        public override void PostMake()
        {
            base.PostMake();
            Sexualizer.sexualize_pawn(pawn);
            GenitaliaChanger.changeGenitalia(this.pawn,Genital_Helper.ovipositorM,Genital_Helper.ovipositorF,Genital_Helper.insect_anus);
        }

        public override void PostAdd()
        {
            base.PostMake();
            GenitaliaChanger.changeGenitalia(this.pawn, Genital_Helper.ovipositorM, Genital_Helper.ovipositorF, Genital_Helper.insect_anus);
        }
    }

}
