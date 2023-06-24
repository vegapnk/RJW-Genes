using Verse;
using rjw;


namespace RJW_Genes
{
    public class Gene_FemaleOnly : RJW_Gene
    {
        public override void PostMake()
        {
            base.PostMake();

            AdjustPawnToFemale();
            // Here we call Sexualization after the Sex-Change
            if (GenitaliaUtility.PawnStillNeedsGenitalia(pawn))
                Sexualizer.sexualize_pawn(pawn);
        }

        public override void PostAdd()
        {
            base.PostMake();
            AdjustPawnToFemale();
        }

        private void AdjustPawnToFemale()
        {
            // Here we really use the Gender.Female and not our helper IsFemale(pawn)
            if (pawn.gender == Gender.Female)
                return;
            else
            {
                GenderHelper.ChangeSex(pawn, () => { 
                    pawn.gender = Gender.Female;
                    GenitaliaChanger.RemoveAllGenitalia(pawn);
                    Sexualizer.sexualize_pawn(pawn);
                });
                GenderUtility.AdjustBodyToTargetGender(pawn, Gender.Female);
            }
        }

        public override void Notify_OnPawnGeneration()
        {
            base.Notify_OnPawnGeneration();
            // If this is Pawn generation, then we can assume that the pawn was never any gender other than female, so they shouldn't have sex change thoughts. (Issue #32)
            GenderUtility.RemoveAllSexChangeThoughts(pawn);
        }
    }
}
