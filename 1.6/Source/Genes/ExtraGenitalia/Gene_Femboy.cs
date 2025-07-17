using Verse;
using rjw;
using RimWorld;

namespace RJW_Genes
{
    public class Gene_Femboy : RJW_Gene
    {
        // Token: 0x06000335 RID: 821 RVA: 0x0000401D File Offset: 0x0000221D
        public override void PostMake()
        {
            base.PostMake();
            if (GenderUtility.IsMale(this.pawn) && this.additional_genital == null)
            {
                this.CreateAndAddVagina();
            }
        }

        // Token: 0x06000336 RID: 822 RVA: 0x00004040 File Offset: 0x00002240
        public override void PostAdd()
        {
            base.PostAdd();
            if (this.pawn.gender == Gender.Male && this.additional_genital == null)
            {
                this.CreateAndAddVagina();
            }
        }

        // Token: 0x06000337 RID: 823 RVA: 0x0000EE4C File Offset: 0x0000D04C
        internal void CreateAndAddVagina()
        {
            if (this.pawn.gender != Gender.Female)
            {
                GenderHelper.ChangeSex(this.pawn, delegate ()
                {
                    this.pawn.gender = Gender.Female;
                    GenitaliaChanger.RemoveAllGenitalia(this.pawn);
                    Sexualizer.sexualize_pawn(this.pawn);
                });
                GenderUtility.AdjustBodyToTargetGender(this.pawn, Gender.Female);
            }
            BodyPartRecord bodyPartRecord = Genital_Helper.get_genitalsBPR(this.pawn);
            Hediff hediff = Genital_Helper.get_AllPartsHediffList(this.pawn).FindLast((Hediff x) => Genital_Helper.is_vagina(x));
            if (hediff != null)
            {
                this.pawn.health.RemoveHediff(hediff);
            }
            HediffDef penisForGene = GenitaliaUtility.GetPenisForGene(GenitaliaUtility.GetGenitaliaTypeGeneForPawn(this.pawn));
            BodyPartRecord part = Genital_Helper.get_genitalsBPR(this.pawn);
            this.additional_genital = HediffMaker.MakeHediff(penisForGene, this.pawn, null);
            HediffComp_SexPart hediffCompSexPart = this.additional_genital.TryGetComp<HediffComp_SexPart>();
            if (hediffCompSexPart != null)
            {
                hediffCompSexPart.Init(this.pawn, false);
                hediffCompSexPart.UpdateSeverity(0f);
            }
            this.pawn.health.AddHediff(this.additional_genital, part, null, null);
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

        // Token: 0x040001B0 RID: 432
        internal Hediff additional_genital;
    }
}
