﻿using Verse;
using rjw;


namespace RJW_Genes
{
    public class Gene_MaleOnly : RJW_Gene
    {
        public override void PostMake()
        {
            base.PostMake();

            AdjustPawnToMale();
            // Here we call Sexualization after the Sex-Change
            if (GenitaliaUtility.PawnStillNeedsGenitalia(pawn))
                Sexualizer.sexualize_pawn(pawn);
        }

        public override void PostAdd()
        {
            base.PostMake();
            AdjustPawnToMale();
        }

        private void AdjustPawnToMale()
        {
            if (pawn.gender == Gender.Male)
                return;
            else
            {
                GenderHelper.ChangeSex(pawn, () => {

                    pawn.gender = Gender.Male;
                    GenitaliaChanger.RemoveAllGenitalia(pawn);
                    Sexualizer.sexualize_pawn(pawn);
                });
                GenderUtility.AdjustBodyToTargetGender(pawn, Gender.Male);
            }
            foreach (Gene g in pawn.genes.GenesListForReading)
            {
                if (g.def.defName == "rjw_genes_hydrolic_genitalia")
                {
                    g.PostAdd();
                }
                if (g.def.defName == "rjw_genes_bionic_genitalia")
                {
                    g.PostAdd();
                    return;
                }
            }
        }

        public override void Notify_OnPawnGeneration()
        {
            base.Notify_OnPawnGeneration();
            // If this is Pawn generation, then we can assume that the pawn was never any gender other than male, so they shouldn't have sex change thoughts. (Issue #32)
            GenderUtility.RemoveAllSexChangeThoughts(pawn);
        }
    }
}
