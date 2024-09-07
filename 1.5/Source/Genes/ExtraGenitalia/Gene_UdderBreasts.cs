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

        Hediff added_udders;

        public override void PostMake()
        {
            base.PostMake();


        }

        public override void PostAdd()
        {
            base.PostAdd();

            AddUdders();
    
        }

        public override void PostRemove()
        {
            base.PostRemove();

            if (added_udders != null)
                pawn.health.RemoveHediff(added_udders);
        }


        internal void AddUdders()
        {
            HediffComp_SexPart CompHediff = null;
            BodyPartRecord bpr = Genital_Helper.get_breastsBPR(pawn);
            added_udders = pawn.health.AddHediff(Genital_Helper.udder_breasts, bpr);
            added_udders.TryGetComp<rjw.HediffComp_SexPart>();
            if (CompHediff != null)
            {
                CompHediff.Init(pawn);
                CompHediff.UpdateSeverity();
            }

        }
    }

}