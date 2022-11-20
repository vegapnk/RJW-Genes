using Verse;
using rjw;
using RimWorld;

namespace RJW_Genes
{
    public class Gene_NoBreasts : RJW_Gene
    {

        internal Hediff removed_breasts;

        // TODO: This gene only works if another Gene was set specifying the genitalia. 
        // If it is added later, it still works, but on creation it needs a different 
        // TODO: If all Genitalia are removed by genes, RJW adds some to the pawns at spawn. IDEA: Add male-nipples ?
        public override void PostMake()
        {
            base.PostMake();
            
            // Breasts are removed for female pawns!
            if (GenderUtility.IsFemale(pawn) && removed_breasts == null)
            {
                RemoveButStoreBreasts();
            }
        }
        
        public override void PostAdd()
        {
            base.PostAdd();

            // Breasts are removed for female pawns!
            if (GenderUtility.IsFemale(pawn) && removed_breasts == null)
            {
                RemoveButStoreBreasts();
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();
            if(removed_breasts != null)    
                pawn.health.AddHediff(removed_breasts);
        }

        internal void RemoveButStoreBreasts()
        {
            var partBPR = Genital_Helper.get_breastsBPR(pawn);
            Hediff breastsToRemove = Genital_Helper.get_AllPartsHediffList(pawn).FindLast(x => GenitaliaUtility.IsBreasts(x));

            if(breastsToRemove != null)
            {
                removed_breasts = breastsToRemove;
                pawn.health.RemoveHediff(breastsToRemove);
            }
        }

    }
}
