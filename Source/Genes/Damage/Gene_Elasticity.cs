using LicentiaLabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    /// <summary>
    /// This Gene adds Licentia-Labs Elasticised Hediff to a Pawn. 
    /// Important: I had a HarmonyPatch first, similar to skipping cumflation, but the Stretching Logic is called quite a lot and for both pawns actually.
    /// Hence, I think choosing the Elasticiced Hediff was good as then everything is covered by "Licentia-Logic". 
    /// </summary>
    public class Gene_Elasticity : Gene
    {

        public override void PostAdd()
        {
            base.PostAdd();
            this.pawn.health.AddHediff(Licentia.HediffDefs.Elasticised);
        }

        public override void PostRemove()
        {
            Hediff candidate = pawn.health.hediffSet.GetFirstHediffOfDef(Licentia.HediffDefs.Elasticised);
            if (candidate != null)
            {
                pawn.health.RemoveHediff(candidate);
            }
            base.PostRemove();
        }
    }
}
