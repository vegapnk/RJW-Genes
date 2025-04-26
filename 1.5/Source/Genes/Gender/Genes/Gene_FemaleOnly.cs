﻿using Verse;
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

            GenderUtility.RemoveSexChangeThoughtsIfTooYoung(this.pawn);
        }

        public override void PostAdd()
        {
            base.PostMake();
            AdjustPawnToFemale();
            GenderUtility.RemoveSexChangeThoughtsIfTooYoung(this.pawn);
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
            foreach(Gene g in pawn.genes.GenesListForReading)
            {
                if(g.def.defName== "rjw_genes_hydraulic_genitalia")
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
            // If this is Pawn generation, then we can assume that the pawn was never any gender other than female, so they shouldn't have sex change thoughts. (Issue #32)
            if (pawn != null)
                GenderUtility.RemoveAllSexChangeThoughts(pawn);
        }
    }
}
