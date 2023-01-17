using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rjw;
using RimWorld;
using Verse;
using Verse.AI;
namespace RJW_Genes
{
    public class JobGiver_TryQuickieWith : ThinkNode_JobGiver
	{
		protected override Job TryGiveJob(Pawn pawn)
		{
			Pawn target = pawn.mindState.duty.focus.Pawn;
			Pawn_JobTracker jobs = target.jobs;
			string pawn_name = xxx.get_pawnname(pawn);
			string target_name = xxx.get_pawnname(target);
			//can reserve eachother
			if (pawn.CanReserveAndReach(target, PathEndMode.InteractionCell, Danger.Some) && target.CanReserve(pawn, 1, 0, null, false))				
			{
				//target is not busy
				if (!(((jobs != null) ? jobs.curJob : null) != null && (jobs.curJob.playerForced || !CasualSex_Helper.quickieAllowedJobs.Contains(jobs.curJob.def))))
                {
					float willingness = TargetWillingness(pawn, target);
					if (Rand.Chance(willingness))
                    {
						return JobMaker.MakeJob(xxx.quick_sex, target);
					}
					else
                    {
						if (RJWSettings.DebugLogJoinInBed) //change this when we have our own settigns
						{
							ModLog.Message(string.Format("{0} was not interested in having sex with {1}: ({2} chance)", pawn_name, target_name, willingness));
						}
					}
                }
				else
                {
					if (RJWSettings.DebugLogJoinInBed) //change this when we have our own settigns
					{
						ModLog.Message(string.Format(" find_pawn_to_fuck({0}): lover has important job ({1}), skipping", pawn_name, target.jobs.curJob.def));
					}
				}
			}
			else
            {
				if (RJWSettings.DebugLogJoinInBed) //change this when we have our own settigns
				{
					ModLog.Message(" (" + pawn_name + "): cannot reach or reserve " + target_name);
				}
			}
			return null;
		}
		public static float TargetWillingness(Pawn pawn, Pawn target)
        {
			string pawn_name = xxx.get_pawnname(pawn);
			float willingness = SexAppraiser.would_fuck(target,pawn);
			bool nymph = xxx.is_nympho(target);
			bool loverelation = LovePartnerRelationUtility.LovePartnerRelationExists(pawn, target);
			if (nymph || loverelation)
            {
				willingness *= 2;
            }
			if (xxx.HasNonPolyPartner(pawn, false) && !loverelation)
			{
				if (RJWHookupSettings.NymphosCanCheat && nymph && xxx.is_frustrated(pawn))
				{
					if (RJWSettings.DebugLogJoinInBed)
					{
						ModLog.Message(" find_partner(" + pawn_name + "): I'm a nympho and I'm so frustrated that I'm going to cheat");
					}
				}
				else
				{
					if (!pawn.health.hediffSet.HasHediff(HediffDef.Named("AlcoholHigh"), false))
					{
						if (RJWSettings.DebugLogJoinInBed)
						{
							ModLog.Message(" find_partner(" + pawn_name + "): I interested in banging but that's cheating");
						}
						//Succubus has a small chance to seduce even if target is in relationship, maybe setting
						willingness *= 0.1f;
					}
                    else
                    {
						if (RJWSettings.DebugLogJoinInBed)
						{
							ModLog.Message(" find_partner(" + pawn_name + "): I want to bang and im too drunk to care if its cheating");
						}
						//No change
					}		
				}
			}
			return willingness;
        }
	}
}
