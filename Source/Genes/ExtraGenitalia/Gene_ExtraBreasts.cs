using Verse;
using rjw;
using RimWorld;

namespace RJW_Genes
{
    public class Gene_ExtraBreasts : Gene
    {

        internal Hediff additional_breasts;

        public override void PostMake()
        {
            base.PostMake();
            if (GenitaliaUtility.PawnStillNeedsGenitalia(pawn))
                Sexualizer.sexualize_pawn(pawn);

            // Penis are only added for female pawns!
            if (pawn.gender == Gender.Female && additional_breasts == null)
            {
                createAndAddPenis();
            }
        }
        
        public override void PostAdd()
        {
            base.PostAdd();

            // Penis are only added for female pawns!
            if (pawn.gender == Gender.Female && additional_breasts == null)
            {
                createAndAddPenis();
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();
            if(additional_breasts != null)    
                pawn.health.RemoveHediff(additional_breasts);
        }

        internal void createAndAddPenis()
        {
            var correctGene = GenitaliaUtility.GetGenitaliaTypeGeneForPawn(pawn);
            var breastDef = GenitaliaUtility.GetBreastsForGene(correctGene);
            var partBPR = Genital_Helper.get_breastsBPR(pawn);
            additional_breasts = HediffMaker.MakeHediff(breastDef, pawn);

            var CompHediff = additional_breasts.TryGetComp<rjw.CompHediffBodyPart>();
            if (CompHediff != null)
            {
                CompHediff.initComp(pawn);
                CompHediff.updatesize();
            }

            pawn.health.AddHediff(additional_breasts, partBPR);
        }

    }
}
