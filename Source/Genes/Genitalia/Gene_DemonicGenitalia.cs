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
            if (GenitaliaUtility.PawnStillNeedsGenitalia(pawn))
                Sexualizer.sexualize_pawn(pawn);

            GenitaliaChanger.ChangeGenitalia(this.pawn,Genital_Helper.demon_penis,Genital_Helper.demon_vagina,Genital_Helper.demon_anus);
        }

        public override void PostAdd()
        {
            base.PostMake();
            GenitaliaChanger.ChangeGenitalia(this.pawn, Genital_Helper.demon_penis, Genital_Helper.demon_vagina, Genital_Helper.demon_anus);
        }
    }

}
