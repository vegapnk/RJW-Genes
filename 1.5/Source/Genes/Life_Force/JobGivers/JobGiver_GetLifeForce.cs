using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.AI;
using RimWorld;
using rjw;

namespace RJW_Genes
{
    public class JobGiver_GetLifeForce : ThinkNode_JobGiver
	{
		protected override Job TryGiveJob(Pawn pawn)
		{
			Pawn_GeneTracker genes = pawn.genes;
			Gene_LifeForce gene_lifeforce = (genes != null) ? genes.GetFirstGeneOfType<Gene_LifeForce>() : null;
			if (gene_lifeforce == null)
			{
				return null;
			}
			if (!gene_lifeforce.ShouldConsumeLifeForceNow())
			{
				return null;
			}
			

			if (ModsConfig.IsActive("rjw.sexperience") && gene_lifeforce.StoredCumAllowed && genes.HasActiveGene(GeneDefOf.rjw_genes_cum_eater))
            {
				Thing gatheredCum = this.GetStoredCum(pawn);
				if (gatheredCum == null)
				{
					return null;
				}
				IngestionOutcomeDoer_LifeForceOffset ingestionOutcomeDoer = (IngestionOutcomeDoer_LifeForceOffset)gatheredCum.def.ingestible.outcomeDoers.First((IngestionOutcomeDoer x) => x is IngestionOutcomeDoer_LifeForceOffset);
				if (ingestionOutcomeDoer == null)
                {
					return null;
                }
				int num = Mathf.RoundToInt(((gene_lifeforce.targetValue - gene_lifeforce.Value) * 100 + 10) / IngestionOutcomeDoer_LifeForceOffset.DEFAULT_FERTILIN_PER_UNIT);
				if (gatheredCum != null && num > 0)
				{
					Job job = JobMaker.MakeJob(RimWorld.JobDefOf.Ingest, gatheredCum);
					job.count = Mathf.Min(gatheredCum.stackCount, num);
					job.ingestTotalCount = true;
					return job;
				}
			}
			return null;
		}

		//From JobGiver_GetHemogen, dont know exactly what this influences
		public override float GetPriority(Pawn pawn)
		{
			if (!ModsConfig.BiotechActive)
			{
				return 0f;
			}
			Pawn_GeneTracker genes = pawn.genes;
			if (((genes != null) ? genes.GetFirstGeneOfType<Gene_LifeForce>() : null) == null)
			{
				return 0f;
			}
			return 9.1f;
		}

		private Thing GetStoredCum(Pawn pawn)
		{
			if (!ModsConfig.IsActive("vegapnk.cumpilation"))
				return null;

			Thing carriedThing = pawn.carryTracker.CarriedThing;
			ThingDef cumThingDef = Cumpilation.DefOfs.Cumpilation_Cum;

			if (cumThingDef == null) { return null; }

			if (carriedThing != null && carriedThing.def == cumThingDef)
			{
				return carriedThing;
			}
			for (int i = 0; i < pawn.inventory.innerContainer.Count; i++)
			{
				if (pawn.inventory.innerContainer[i].def == cumThingDef)
				{
					return pawn.inventory.innerContainer[i];
				}
			}
			return GenClosest.ClosestThing_Global_Reachable(pawn.Position, pawn.Map, pawn.Map.listerThings.ThingsOfDef(cumThingDef), PathEndMode.OnCell, TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false, false, false), 9999f, (Thing t) => pawn.CanReserve(t, 1, -1, null, false) && !t.IsForbidden(pawn), null);
		}
	}
}
