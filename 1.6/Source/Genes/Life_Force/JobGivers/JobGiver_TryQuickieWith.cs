using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using rjw;
using rjw.Modules.Attraction;
using RJWSexperience;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.AI.Group;
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
				//Dont interrupt player
				if (!(((jobs != null) ? jobs.curJob : null) != null && jobs.curJob.playerForced))
                {
					float willingness = TargetWillingness(pawn, target);
					if (Rand.Chance(willingness))
                    {
						Job newJob =JobMaker.MakeJob(xxx.quick_sex, target);
						

						return newJob;
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
						//ModLog.Message(string.Format(" find_pawn_to_fuck({0}): lover has important job ({1}), skipping", pawn_name, target.jobs.curJob.def));
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
			float willingness = AttractionUtility.Evaluate(target,pawn);
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
						//Succubus has a small chance to seduce even if target is in relationship
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

		public static float JoinChance(Pawn pawn ,Pawn target)
        {

			float chance = 0.1f;

			//Sex satisfaction, how good the target is at sex
			chance *= xxx.get_sex_satisfaction(target); 
			
			//Succubus mood
			if (pawn.needs != null && pawn.needs.mood != null)
            {
				chance *= pawn.needs.mood.CurLevelPercentage + 0.5f; 
			}
			
			//Size of genitals
			bool size_matters = true; //To be placed in modsettings
			if (size_matters)
            {
				//The larger the penis to greater the chance
				if (RelationsUtility.AttractedToGender(pawn, Gender.Male))
				{
					chance *= GetGenitalSize(target, true) + 0.5f;
				}

				//The tighter the vagine the greater the chance, a size above 1 is considered as 1
				if (RelationsUtility.AttractedToGender(pawn, Gender.Female))
				{
					chance *= 1f - Mathf.Min(GetGenitalSize(target, false),1f) + 0.5f;
				}
			}

			//Sex ability from sexperience
			if (ModsConfig.IsActive("rjw.sexperience"))
            {
				chance *= RJWSexperience.PawnExtensions.GetSexStat(pawn);
            }
			return Mathf.Max(chance,0f);
        }

		//Gets the size of the largest penis or the tightest vagina
		public static float GetGenitalSize(Pawn pawn, bool penis_else_vagina)
        {
			List<Hediff> genitals = rjw.PawnExtensions.GetGenitalsList(pawn);
			if(!genitals.NullOrEmpty())
            {
				if (penis_else_vagina)
				{
					List<Hediff> penises = genitals.Where(genital => Genital_Helper.is_penis(genital)).ToList();
					{
						if (!penises.NullOrEmpty())
						{
							return penises.Max(genital => genital.Severity);
						}
					}
				}
				else
                {
					List<Hediff> vaginas = genitals.Where(genital => Genital_Helper.is_vagina(genital)).ToList();
					{
						if (!vaginas.NullOrEmpty())
						{
							return vaginas.Min(genital => genital.Severity);
						}
					}
				}
			}
			return 0f;
			
		}
	}
}
