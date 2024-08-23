using Verse;
using rjw;

namespace RJW_Genes
{
    public class Gene_OvipositorGenitalia : RJW_Gene
    {
        public override void PostMake()
        {
            base.PostMake();

            GenitaliaChanger.ChangeGenitalia(this.pawn,Genital_Helper.ovipositorM,Genital_Helper.ovipositorF,Genital_Helper.insect_anus);
        }

        public override void PostAdd()
        {
            base.PostAdd();
            GenitaliaChanger.ChangeGenitalia(this.pawn, Genital_Helper.ovipositorM, Genital_Helper.ovipositorF, Genital_Helper.insect_anus);
        }
    }

}
