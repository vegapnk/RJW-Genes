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
    public class JobGiver_LifeForce_RandomRape : JobGiver_RandomRape
    {
		protected override Job TryGiveJob(Pawn pawn)
		{
			if (!can_rape(pawn, false))
			{
				return null;
			}
			Pawn pawn2 = this.find_victim(pawn, pawn.Map);
			if (pawn2 == null)
			{
				return null;
			}
			return JobMaker.MakeJob(JobDefOf.rjw_genes_lifeforce_randomrape, pawn2);
		}

		//same as xxx.canrape from rjw, but without last requirements.
		public static bool can_rape(Pawn pawn, bool forced = false)
		{
			return RJWSettings.rape_enabled && (xxx.is_mechanoid(pawn) || ((xxx.can_fuck(pawn) || 
				(!xxx.is_male(pawn) && xxx.get_vulnerability(pawn) < RJWSettings.nonFutaWomenRaping_MaxVulnerability && 
				xxx.can_be_fucked(pawn))) && (!xxx.is_human(pawn) || ((pawn.ageTracker.Growth >= 1f || pawn.ageTracker.CurLifeStage.reproductive)))));
		}
	}
}
