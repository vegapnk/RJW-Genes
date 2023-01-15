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
    public class RaceGeneDef_Helper
    {
      	//code based on racegroupdefinternal which has a similar function
		public static RaceGeneDef GetRaceGeneDefInternal(Pawn pawn)
        {
			List<RaceGeneDef> Valids = ValidRaceGeneDefs(pawn);
			if (Valids.Count > 0)
            {
				RaceGeneDef result = Valids.MaxBy(r => r.priority);
				return result;
			}			
			return null;
			//First check if there is a matching pawnkinddef then race, then racegroup

		}
		public static List<RaceGeneDef> ValidRaceGeneDefs(Pawn pawn)
		{
			PawnKindDef kindDef = pawn.kindDef;
			if (kindDef == null)
			{
				return null;
			}
			string raceName = kindDef.race.defName;
			string pawnKindName = kindDef.defName;
			PawnData pawnData = SaveStorage.DataStore.GetPawnData(pawn);
			RaceGroupDef raceGroupDef = pawnData.RaceSupportDef;

			IEnumerable<RaceGeneDef> allDefs = DefDatabase<RaceGeneDef>.AllDefs;
			List<RaceGeneDef> pawnKindDefs = allDefs.Where(delegate (RaceGeneDef group)
			{
				List<string> pawnKindNames = group.pawnKindNames;
				return pawnKindNames != null && pawnKindNames.Contains(pawnKindName);
			}).ToList<RaceGeneDef>();
			if (pawnKindDefs.Count() > 0)
				return pawnKindDefs;
			List<RaceGeneDef> raceKindDefs = allDefs.Where(delegate (RaceGeneDef group)
			{
				List<string> raceNames = group.raceNames;
				return raceNames != null && raceNames.Contains(raceName);
			}).ToList<RaceGeneDef>();
			if (raceKindDefs.Count() > 0)
				return raceKindDefs;
			List<RaceGeneDef> raceGroupDefs = new List<RaceGeneDef>();
			if (raceGroupDef != null)
			{
				raceGroupDefs = allDefs.Where(delegate (RaceGeneDef group)
				{
					string raceGroupDefName = group.raceGroup;
					List<string> list_raceGroupDefName = group.raceGroups;
					return (raceGroupDefName != null && raceGroupDefName == raceGroupDef.defName) 
							|| (list_raceGroupDefName != null && list_raceGroupDefName.Contains(raceGroupDef.defName));
				}).ToList<RaceGeneDef>();
			}
			if (raceGroupDefs.Count() > 0)
				return raceGroupDefs;
			return new List<RaceGeneDef>();
		}
    } 
}
