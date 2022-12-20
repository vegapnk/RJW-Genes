using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rjw;
using Verse;
using RimWorld;

namespace RJW_BGS
{
    internal class RJWcopy
    {
      	//code based on racegroupdefinternal which has a similar function
		public static RaceGeneDef GetRaceGeneDefInternal(Pawn pawn)
        {
			PawnKindDef kindDef = pawn.kindDef;
			if (kindDef == null)
            {
				return null;
            }
			string raceName = kindDef.race.defName;
			string pawnKindName = kindDef.defName;
			IEnumerable<RaceGeneDef> allDefs = DefDatabase<RaceGeneDef>.AllDefs;
			PawnData pawnData = SaveStorage.DataStore.GetPawnData(pawn);
			RaceGroupDef raceGroupDef = pawnData.RaceSupportDef;
			List<RaceGeneDef> pawnKindDefs = allDefs.Where(delegate (RaceGeneDef group)
			{
				List<string> pawnKindNames = group.pawnKindNames;
				return pawnKindNames != null && pawnKindNames.Contains(pawnKindName);
			}).ToList<RaceGeneDef>();
			List<RaceGeneDef> raceKindDefs = allDefs.Where(delegate (RaceGeneDef group)
			{
				List<string> raceNames = group.raceNames;
				return raceNames != null && raceNames.Contains(raceName);
			}).ToList<RaceGeneDef>();
			List<RaceGeneDef> raceGroupDefs = new List<RaceGeneDef>();
			if (raceGroupDef != null)
			{
				raceGroupDefs = allDefs.Where(delegate (RaceGeneDef group)
				{
					String raceGroupDefName = group.raceGroup;
					return raceGroupDefName != null && raceGroupDefName == raceGroupDef.defName;
				}).ToList<RaceGeneDef>();
			}
			RaceGeneDef result = null;
			//First check if there is a matching pawnkinddef then race, then racegroup
			if (pawnKindDefs.Any())
			{
				result = pawnKindDefs.RandomElement();
			}
			else if (raceKindDefs.Any() && result == null)
			{
				result = raceKindDefs.RandomElement();
			}
			else if (raceGroupDefs.Any() && result == null)
			{
				result = raceGroupDefs.RandomElement();
			}
			else
            {
				result = null;
            }
			return result;


		}
    } 
}
