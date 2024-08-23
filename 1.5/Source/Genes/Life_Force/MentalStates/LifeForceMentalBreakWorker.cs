using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using RimWorld;
using Verse.AI;

namespace RJW_Genes
{
    public class LifeForceMentalBreakWorker : MentalBreakWorker
	{
		public override bool BreakCanOccur(Pawn pawn)
		{
			if (pawn.Spawned && base.BreakCanOccur(pawn))
			{
				if (!GeneUtility.HasLifeForce(pawn))
                {
					return false;
                }
				Gene_LifeForce gene = pawn.genes.GetFirstGeneOfType<Gene_LifeForce>();
				if( gene.Resource.Value <= 0)
                {
					return true;
                }
			}
			return false;
		}
	}
}
