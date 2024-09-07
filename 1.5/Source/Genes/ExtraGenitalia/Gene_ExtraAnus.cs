using Verse;
using rjw;
using RimWorld;
using System.Linq;

namespace RJW_Genes
{
    public class Gene_ExtraAnus : RJW_Gene
    {

        internal Hediff additional_anus;

        public override void PostMake()
        {
            base.PostMake();

            // Some sources add Genes before they fire, e.g. Character Editor
            // This should harden the gene, to solve #19
            if (HasAlreadyTwoAnus())
            {
                return;
            }

            if (additional_anus == null)
            {
                CreateAndAddAnus();
            }
        }
        
        public override void PostAdd()
        {
            base.PostAdd();

            // Some sources add Genes before they fire, e.g. Character Editor
            // This should harden the gene, to solve #19
            if (HasAlreadyTwoAnus())
            {
                return;
            }

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

            var CompHediff = additional_anus.TryGetComp<rjw.HediffComp_SexPart>();
            if (CompHediff != null)
            {
                CompHediff.Init(pawn);
                CompHediff.UpdateSeverity();
            }

            pawn.health.AddHediff(additional_anus, partBPR);
        }

        internal bool HasAlreadyTwoAnus()
        {
            if (pawn == null)
                return false;

            var possible_breasts =
                Genital_Helper.get_AllPartsHediffList(pawn).Where(t => Genital_Helper.is_anus(t));

            return possible_breasts.Count() >= 2;
        }
    }
}
