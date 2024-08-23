using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using RimWorld;
namespace RJW_Genes
{
    //Based on LordToil_EscortPawn
    public class LordToil_Flirt : LordToil
    {
        public LordToil_Flirt(Pawn victim, float followRadius)
        {
            this.victim = victim;
            this.followRadius = followRadius;
        }


        public override void UpdateAllDuties()
        {
            for (int i = 0; i < this.lord.ownedPawns.Count; i++)
            {
                PawnDuty duty = new PawnDuty(GeneDefOf.rjw_genes_flirt, this.victim, this.followRadius);
                this.lord.ownedPawns[i].mindState.duty = duty;
            }
        }

        public Pawn victim;
        public float followRadius;
    }
}
