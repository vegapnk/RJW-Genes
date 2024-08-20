using Verse;
using rjw;
using RimWorld;

namespace RJW_Genes
{
    public class Gene_NoVagina : RJW_Gene
    {

        internal Hediff removed_vagina;
        internal Hediff missing_bodypart_hediff;
        
        public override void PostMake()
        {
            base.PostMake();

            // Vaginas are only removed for female pawns!
            if (GenderUtility.IsFemale(pawn) && removed_vagina == null)
            {
                RemoveButStoreVagina();
            }
        }
        
        public override void PostAdd()
        {
            base.PostAdd();

            // Vaginas are only removed for female pawns!
            if (GenderUtility.IsFemale(pawn) && removed_vagina == null)
            {
                RemoveButStoreVagina();
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();

            if (missing_bodypart_hediff != null)
                pawn.health.RemoveHediff(missing_bodypart_hediff);

            if (removed_vagina != null)    
                pawn.health.AddHediff(removed_vagina);

        }

        internal void RemoveButStoreVagina()
        {
            var partBPR = Genital_Helper.get_genitalsBPR(pawn);
            Hediff vaginaToRemove = Genital_Helper.get_AllPartsHediffList(pawn).FindLast(x => Genital_Helper.is_vagina(x));

            if(vaginaToRemove != null)
            {
                removed_vagina = vaginaToRemove;
                pawn.health.RemoveHediff(vaginaToRemove);

                if (missing_bodypart_hediff == null)
                    missing_bodypart_hediff = pawn.health.AddHediff(RimWorld.HediffDefOf.MissingBodyPart, partBPR);
            }
        }

    }
}
