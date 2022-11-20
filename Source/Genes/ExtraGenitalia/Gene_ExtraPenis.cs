using Verse;
using rjw;
using RimWorld;

namespace RJW_Genes
{
    public class Gene_ExtraPenis : RJW_Gene
    {

        internal Hediff additional_penis;

        public override void PostMake()
        {
            base.PostMake();

            // Penis are only added for male pawns!
            if (GenderUtility.IsMale(pawn) && additional_penis == null)
            {
                CreateAndAddPenis();
            }
        }
        
        public override void PostAdd()
        {
            base.PostAdd();

            // Penis are only added for male pawns!
            if (GenderUtility.IsMale(pawn) && additional_penis == null)
            {
                CreateAndAddPenis();
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();
            if(additional_penis != null)    
                pawn.health.RemoveHediff(additional_penis);
        }

        internal void CreateAndAddPenis()
        {
            var correctGene = GenitaliaUtility.GetGenitaliaTypeGeneForPawn(pawn);
            HediffDef penisDef = GenitaliaUtility.GetPenisForGene(correctGene);
            var partBPR = Genital_Helper.get_genitalsBPR(pawn);
            additional_penis = HediffMaker.MakeHediff(penisDef, pawn);

            var CompHediff = additional_penis.TryGetComp<rjw.CompHediffBodyPart>();
            if (CompHediff != null)
            {
                CompHediff.initComp(pawn);
                CompHediff.updatesize();
            }

            pawn.health.AddHediff(additional_penis, partBPR);
        }

    }
}
