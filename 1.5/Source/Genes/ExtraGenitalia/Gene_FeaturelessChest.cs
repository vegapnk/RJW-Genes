using Verse;
using rjw;

namespace RJW_Genes
{
    public class Gene_FeaturelessChest : RJW_Gene
    {
        internal Hediff removed_breasts;
        internal Hediff added_nipples;
        public override void PostMake()
        {
            base.PostMake();

            if (removed_breasts == null)
            {
                RemoveButStoreBreasts();
                AddFeaturelessBreast();
            }
        }

        public override void PostAdd()
        {
            base.PostAdd();

            if (removed_breasts == null)
            {
                RemoveButStoreBreasts();
                AddFeaturelessBreast();
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();
            if (added_nipples != null)
                pawn.health.RemoveHediff(added_nipples);
            if (removed_breasts != null)
                pawn.health.AddHediff(removed_breasts);
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

        internal void AddFeaturelessBreast()
        {
            var partBPR = Genital_Helper.get_breastsBPR(pawn);
            added_nipples = pawn.health.AddHediff(Genital_Helper.featureless_chest, partBPR);
        }
        
    }
}
