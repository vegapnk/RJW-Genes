using Verse;
using RimWorld;
using rjw;

namespace RJW_Genes
{
    public class Gene_DemonicGenitaliaTentacle : RJW_Gene
    {
        public override void PostMake()
        {
            base.PostMake();

            GenitaliaChanger.ChangeGenitalia(this.pawn,Genital_Helper.demonT_penis,Genital_Helper.demon_vagina,Genital_Helper.demon_anus);
        }

        public override void PostAdd()
        {
            base.PostAdd();
            GenitaliaChanger.ChangeGenitalia(this.pawn, Genital_Helper.demonT_penis, Genital_Helper.demon_vagina, Genital_Helper.demon_anus);
        }
    }

}
