using Verse;
using rjw;
using RimWorld;

namespace RJW_Genes
{
    public class Gene_ExtraPenis : Gene
    {

        internal Hediff additional_penis;

        public override void PostMake()
        {
            base.PostMake();
            if (GenitaliaUtility.PawnStillNeedsGenitalia(pawn))
                Sexualizer.sexualize_pawn(pawn);

            // Penis are only added for male pawns!
            if (pawn.gender == Gender.Male && additional_penis == null)
            {
                createAndAddPenis();
            }
        }
        
        public override void PostAdd()
        {
            base.PostAdd();

            // Penis are only added for male pawns!
            if (pawn.gender == Gender.Male && additional_penis == null)
            {
                createAndAddPenis();
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();
            if(additional_penis != null)    
                pawn.health.RemoveHediff(additional_penis);
        }

        internal void createAndAddPenis()
        {
            var correctGene = GenitaliaUtility.GetGenitaliaTypeGeneForPawn(pawn);
            var penisDef = GenitaliaUtility.GetPenisForGene(correctGene);
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
