using rjw;
using Verse;

namespace RJW_Genes
{

    /// <summary>
    /// Removes breasts for female (and trap, futa) pawns and adds Udders. 
    /// Wished for in Issue #27.
    /// 
    /// TODO: Currently, the sexualizer over-writes the added udders and just adds another pair of breasts!
    /// I commented out the gene in .xml for now. 
    /// </summary>
    public class Gene_UdderBreasts : RJW_Gene
    {

        Hediff removed_breasts;
        Hediff added_udders;

        public override void PostMake()
        {
            base.PostMake();

            // Breasts are replaced for female,trap and futa pawns
            if ( removed_breasts == null 
                && (GenderUtility.IsFemale(pawn) || GenderHelper.GetSex(pawn) == GenderHelper.Sex.futa || GenderHelper.GetSex(pawn) == GenderHelper.Sex.trap)
                )
            {
                RemoveButStoreBreasts();
                AddUdders();
            }

        }

        public override void PostAdd()
        {
            base.PostAdd();

            // Breasts are replaced for female,trap and futa pawns
            if (removed_breasts == null
                && (GenderUtility.IsFemale(pawn) || GenderHelper.GetSex(pawn) == GenderHelper.Sex.futa || GenderHelper.GetSex(pawn) == GenderHelper.Sex.trap)
                ) 
            { 
                RemoveButStoreBreasts();
                AddUdders();
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();
            // Re-Add the old breasts
            if (removed_breasts != null)
                pawn.health.AddHediff(removed_breasts);
            if (added_udders != null)
                pawn.health.RemoveHediff(added_udders);
        }

        internal void RemoveButStoreBreasts()
        {
            var partBPR = Genital_Helper.get_breastsBPR(pawn);
            Hediff breastsToRemove = Genital_Helper.get_AllPartsHediffList(pawn).FindLast(x => GenitaliaUtility.IsBreasts(x));

            if (breastsToRemove != null)
            {
                removed_breasts = breastsToRemove;
                pawn.health.RemoveHediff(breastsToRemove);
            }
        }

        internal void AddUdders()
        {
            BodyPartRecord bpr = Genital_Helper.get_uddersBPR(pawn);
            added_udders = pawn.health.AddHediff(Genital_Helper.udder_breasts, bpr);
        }
    }

}