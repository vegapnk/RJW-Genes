using Verse;
using rjw;
using RimWorld;
using System.Linq;

namespace RJW_Genes
{
    public class Gene_ExtraBreasts : RJW_Gene
    {

        internal Hediff additional_breasts;

        public override void PostMake()
        {
            base.PostMake();

            // Some sources add Genes before they fire, e.g. Character Editor
            // This should harden the gene, to solve #19
            if (HasAlreadyTwoBreasts())
            {
                return;
            }

            // Tits are only added for female pawns!
            if (GenderUtility.IsFemale(pawn) && additional_breasts == null)
            {
                CreateAndAddBreasts();
            }
        }
        
        public override void PostAdd()
        {
            base.PostAdd();

            // Some sources add Genes before they fire, e.g. Character Editor
            // This should harden the gene, to solve #19
            if (HasAlreadyTwoBreasts())
            {
                return;
            }

            // Tits are only added for female pawns!
            if (GenderUtility.IsFemale(pawn) && additional_breasts == null)
            {
                CreateAndAddBreasts();
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();
            if(additional_breasts != null)    
                pawn.health.RemoveHediff(additional_breasts);
        }

        internal void CreateAndAddBreasts()
        {
            var correctGene = GenitaliaUtility.GetGenitaliaTypeGeneForPawn(pawn);
            var breastDef = GenitaliaUtility.GetBreastsForGene(correctGene);
            var partBPR = Genital_Helper.get_breastsBPR(pawn);
            additional_breasts = HediffMaker.MakeHediff(breastDef, pawn);

            var CompHediff = additional_breasts.TryGetComp<rjw.HediffComp_SexPart>();
            if (CompHediff != null)
            {
                CompHediff.Init(pawn);
                CompHediff.UpdateSeverity();
            }

            pawn.health.AddHediff(additional_breasts, partBPR);
        }

        internal bool HasAlreadyTwoBreasts()
        {
            if (pawn == null)
                return false;

            var possible_breasts = 
                Genital_Helper.get_AllPartsHediffList(pawn).Where(t => t.def.defName.Contains("breast"));

            return possible_breasts.Count() >= 2;
        }

    }
}
