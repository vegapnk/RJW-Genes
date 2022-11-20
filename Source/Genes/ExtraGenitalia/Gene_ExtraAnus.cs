using Verse;
using rjw;
using RimWorld;

namespace RJW_Genes
{
    public class Gene_ExtraAnus : RJW_Gene
    {

        internal Hediff additional_anus;

        public override void PostMake()
        {
            base.PostMake();

            if (additional_anus == null)
            {
                CreateAndAddAnus();
            }
        }
        
        public override void PostAdd()
        {
            base.PostAdd();

            if (additional_anus == null)
            {
                CreateAndAddAnus();
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();
            if(additional_anus != null)    
                pawn.health.RemoveHediff(additional_anus);
        }

        internal void CreateAndAddAnus()
        {
            var correctGene = GenitaliaUtility.GetGenitaliaTypeGeneForPawn(pawn);
            var anusDef = GenitaliaUtility.GetAnusForGene(correctGene);
            var partBPR = Genital_Helper.get_anusBPR(pawn);
            additional_anus = HediffMaker.MakeHediff(anusDef, pawn);

            var CompHediff = additional_anus.TryGetComp<rjw.CompHediffBodyPart>();
            if (CompHediff != null)
            {
                CompHediff.initComp(pawn);
                CompHediff.updatesize();
            }

            pawn.health.AddHediff(additional_anus, partBPR);
        }

    }
}
