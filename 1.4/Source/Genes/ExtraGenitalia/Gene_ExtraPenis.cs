using Verse;
using rjw;
using RimWorld;
using System.Linq;

namespace RJW_Genes
{
    public class Gene_ExtraPenis : RJW_Gene
    {

        internal Hediff additional_penis;

        public override void PostMake()
        {
            base.PostMake();

            // Some sources add Genes before they fire, e.g. Character Editor
            // This should harden the gene, to solve #19
            if (HasAlreadyTwoPenis())
            {
                return;
            }

            // Penis are only added for male pawns!
            if (GenderUtility.IsMale(pawn) && additional_penis == null)
            {
                CreateAndAddPenis();
            }
        }
        
        public override void PostAdd()
        {
            base.PostAdd();

            // Some sources add Genes before they fire, e.g. Character Editor
            // This should harden the gene, to solve #19
            if (HasAlreadyTwoPenis())
            {
                return;
            }

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

            var CompHediff = additional_penis.TryGetComp<rjw.HediffComp_SexPart>();
            if (CompHediff != null)
            {
                CompHediff.Init(pawn);
                CompHediff.UpdateSeverity();
            }

            pawn.health.AddHediff(additional_penis, partBPR);
        }


        internal bool HasAlreadyTwoPenis()
        {
            if (pawn == null)
                return false;

            var possible_breasts =
                Genital_Helper.get_AllPartsHediffList(pawn).Where(t => Genital_Helper.is_penis(t));

            return possible_breasts.Count() >= 2;
        }

    }
}
