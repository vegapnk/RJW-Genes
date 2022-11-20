using Verse;
using rjw;
using RimWorld;

namespace RJW_Genes
{
    public class Gene_NoVagina : RJW_Gene
    {

        internal Hediff removed_vagina;

        // TODO: This gene only works if another Gene was set specifying the genitalia. 
        // If it is added later, it still works, but on creation it needs a different 
        // TODO: If all Genitalia are removed by genes, RJW adds some to the pawns at spawn
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
            if(removed_vagina != null)    
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
            }
        }

    }
}
