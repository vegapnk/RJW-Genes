using Verse;
using rjw;

namespace RJW_Genes
{
    public class Gene_DragonGenitalia : RJW_Gene
    {
        public override void PostMake()
        {
            base.PostMake();

            GenitaliaChanger.ChangeGenitalia(this.pawn,Genital_Helper.dragon_penis,Genital_Helper.dragon_vagina,Genital_Helper.generic_anus);
        }

        public override void PostAdd()
        {
            base.PostAdd();
            GenitaliaChanger.ChangeGenitalia(this.pawn, Genital_Helper.dragon_penis, Genital_Helper.dragon_vagina, Genital_Helper.generic_anus);
        }
    }

}
