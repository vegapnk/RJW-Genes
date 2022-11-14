using Verse;
using rjw;

namespace RJW_Genes
{
    public class Gene_HumanGenitalia : Gene
    {
        public override void PostMake()
        {
            base.PostMake();
            if (GenitaliaUtility.PawnStillNeedsGenitalia(pawn))
                Sexualizer.sexualize_pawn(pawn);

            GenitaliaChanger.ChangeGenitalia(this.pawn,Genital_Helper.average_penis,Genital_Helper.average_vagina,Genital_Helper.average_anus);
        }

        public override void PostAdd()
        {
            base.PostAdd();
            GenitaliaChanger.ChangeGenitalia(this.pawn, Genital_Helper.average_penis, Genital_Helper.average_vagina, Genital_Helper.average_anus);
        }

    }

}
