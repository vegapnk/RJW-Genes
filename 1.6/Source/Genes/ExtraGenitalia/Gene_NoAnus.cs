using Verse;
using rjw;
using RimWorld;

namespace RJW_Genes
{
    public class Gene_NoAnus : RJW_Gene
    {

        internal Hediff removed_anus;

        public override void PostMake()
        {
            base.PostMake();

            if (removed_anus == null)
            {
                RemoveButStoreAnus();
            }
        }
        
        public override void PostAdd()
        {
            if (pawn.kindDef == null) return;   //Added to catch Rimworld creating statues of pawns.
            base.PostAdd();

            if (removed_anus == null)
            {
                RemoveButStoreAnus();
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();
            if(removed_anus != null)    
                pawn.health.AddHediff(removed_anus);
        }

        internal void RemoveButStoreAnus()
        {
            var partBPR = Genital_Helper.get_anusBPR(pawn);
            Hediff anusToRemove = Genital_Helper.get_AllPartsHediffList(pawn).FindLast(x => GenitaliaChanger.IsAnus(x));

            if(anusToRemove != null)
            {
                removed_anus = anusToRemove;
                pawn.health.RemoveHediff(anusToRemove);
            }
        }

    }
}
