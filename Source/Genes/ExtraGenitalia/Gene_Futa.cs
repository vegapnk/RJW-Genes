using Verse;
using rjw;
using RimWorld;

namespace RJW_Genes
{
    public class Gene_Futa : RJW_Gene
    {

        internal Hediff additional_genital;

        public override void PostMake()
        {
            base.PostMake();

            if (GenderUtility.IsFemale(pawn) && additional_genital == null)
            {
                createAndAddPenis();
            }
            if (GenderUtility.IsMale(pawn) && additional_genital == null)
            {
                CreateAndAddVagina();
            }
        }
        
        public override void PostAdd()
        {
            base.PostAdd();

            if (pawn.gender == Gender.Female && additional_genital == null)
            {
                createAndAddPenis();
            }
            if (pawn.gender == Gender.Male && additional_genital == null)
            {
                CreateAndAddVagina();
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();
            if(additional_genital != null)    
                pawn.health.RemoveHediff(additional_genital);
        }

        //TODO: Extract createAndAddXXX to extra class
        internal void createAndAddPenis()
        {
            var correctGene = GenitaliaUtility.GetGenitaliaTypeGeneForPawn(pawn);
            HediffDef penisDef = GenitaliaUtility.GetPenisForGene(correctGene);
            var partBPR = Genital_Helper.get_genitalsBPR(pawn);
            additional_genital = HediffMaker.MakeHediff(penisDef, pawn);

            var CompHediff = additional_genital.TryGetComp<rjw.CompHediffBodyPart>();
            if (CompHediff != null)
            {
                CompHediff.initComp(pawn);
                CompHediff.updatesize();
            }

            pawn.health.AddHediff(additional_genital, partBPR);
        }

        internal void CreateAndAddVagina()
        {
            var correctGene = GenitaliaUtility.GetGenitaliaTypeGeneForPawn(pawn);
            HediffDef vaginaDef = GenitaliaUtility.GetVaginaForGene(correctGene);
            var partBPR = Genital_Helper.get_genitalsBPR(pawn);
            additional_genital = HediffMaker.MakeHediff(vaginaDef, pawn);

            var CompHediff = additional_genital.TryGetComp<rjw.CompHediffBodyPart>();
            if (CompHediff != null)
            {
                CompHediff.initComp(pawn);
                CompHediff.updatesize();
            }

            pawn.health.AddHediff(additional_genital, partBPR);
        }

    }
}
