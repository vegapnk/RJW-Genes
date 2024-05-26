using Verse;
using rjw;

namespace RJW_Genes
{
    public class Gene_GhostGenitalia : RJW_Gene
    {
        public override void PostMake()
        {
            base.PostMake();

            GenitaliaChanger.ChangeGenitalia(this.pawn, Genital_Helper_2.GhostPenis, Genital_Helper_2.GhostVagina, Genital_Helper.average_anus);
        }

        public override void PostAdd()
        {
            base.PostAdd();
            GenitaliaChanger.ChangeGenitalia(this.pawn, Genital_Helper_2.GhostPenis, Genital_Helper_2.GhostVagina, Genital_Helper.average_anus);
        }
    }

}
