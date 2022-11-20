using Verse;
using rjw;
using RimWorld;

namespace RJW_Genes
{
    public class Gene_ExtraVagina : RJW_Gene
    {

        internal Hediff additional_vagina;

        //TODO: This works ingame when genes are added, but if there is 
        //a gene (e.g. ovipositor) in creation it does not work as expected (only has one genital)
        //Penis works as expected


        public override void PostMake()
        {
            base.PostMake();

            // Vaginas are only added for female pawns!
            if (GenderUtility.IsFemale(pawn) && additional_vagina == null)
            {
                CreateAndAddVagina();
            }
        }
        
        public override void PostAdd()
        {
            base.PostAdd();

            // Vaginas are only added for female pawns!
            if (GenderUtility.IsFemale(pawn) && additional_vagina == null)
            {
                CreateAndAddVagina();
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();
            if(additional_vagina != null)    
                pawn.health.RemoveHediff(additional_vagina);
        }

        internal void CreateAndAddVagina()
        {
            var correctGene = GenitaliaUtility.GetGenitaliaTypeGeneForPawn(pawn);
            HediffDef vaginaDef = GenitaliaUtility.GetVaginaForGene(correctGene);
            var partBPR = Genital_Helper.get_genitalsBPR(pawn);
            additional_vagina = HediffMaker.MakeHediff(vaginaDef, pawn);

            var CompHediff = additional_vagina.TryGetComp<rjw.CompHediffBodyPart>();
            if (CompHediff != null)
            {
                CompHediff.initComp(pawn);
                CompHediff.updatesize();
            }

            pawn.health.AddHediff(additional_vagina, partBPR);
        }

    }
}
