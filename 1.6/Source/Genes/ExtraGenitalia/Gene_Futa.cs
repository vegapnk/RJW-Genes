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
        }
        
        public override void PostAdd()
        {
            if (pawn.kindDef == null) return;   //Added to catch Rimworld creating statues of pawns.
            base.PostAdd();

            // If the Pawn is already a Futa, do not do anything. Can Happen by Base-RJW Spawn Chance or potentially races / other mods. 
            if (IsAlreadyFuta(pawn))
            {
                return;
            }

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

            var CompHediff = additional_genital.TryGetComp<rjw.HediffComp_SexPart>();
            if (CompHediff != null)
            {
                CompHediff.Init(pawn);
                CompHediff.UpdateSeverity();
            }

            pawn.health.AddHediff(additional_genital, partBPR);
        }

        internal void CreateAndAddVagina()
        {
            var correctGene = GenitaliaUtility.GetGenitaliaTypeGeneForPawn(pawn);
            HediffDef vaginaDef = GenitaliaUtility.GetVaginaForGene(correctGene);
            var partBPR = Genital_Helper.get_genitalsBPR(pawn);
            additional_genital = HediffMaker.MakeHediff(vaginaDef, pawn);

            var CompHediff = additional_genital.TryGetComp<rjw.HediffComp_SexPart>();
            if (CompHediff != null)
            {
                CompHediff.Init(pawn);
                CompHediff.UpdateSeverity();
            }

            pawn.health.AddHediff(additional_genital, partBPR);
        }

        private static bool IsAlreadyFuta(Pawn pawn)
        {
            if (pawn == null)
                return false;
            if (!Genital_Helper.has_genitals(pawn))
                return false;
            return 
                (Genital_Helper.has_penis_fertile(pawn) || Genital_Helper.has_penis_infertile(pawn)) 
                && Genital_Helper.has_vagina(pawn) ;
        }
    }
}
