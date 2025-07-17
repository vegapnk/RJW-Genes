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
		protected override Job TryGiveJob(Pawn rapist)
		{
			if (!can_rape(rapist, false))
			{
				return null;
			}
			Pawn rapevictim = this.FindVictim(rapist);
			if (rapevictim == null)
			{
				return null;
			}
			return JobMaker.MakeJob(JobDefOf.rjw_genes_lifeforce_randomrape, rapevictim);
		}

		//same as xxx.canrape from rjw, but without last requirements.
		public static bool can_rape(Pawn potentialRapist, bool forced = false)
		{
			return RJWSettings.rape_enabled && (xxx.is_mechanoid(potentialRapist) || ((xxx.can_fuck(potentialRapist) || 
				(!xxx.is_male(potentialRapist) && xxx.get_vulnerability(potentialRapist) < RJWSettings.nonFutaWomenRaping_MaxVulnerability && 
				xxx.can_be_fucked(potentialRapist))) && (!xxx.is_human(potentialRapist) || ((potentialRapist.ageTracker.Growth >= 1f || potentialRapist.ageTracker.CurLifeStage.reproductive)))));
		}
	}
}
