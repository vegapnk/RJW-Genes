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
        //Code copied from rjw, as their code was internal and I need the dictionary to know which genes to add to the pawn
        public static void Racegroupdictbuilder()
        {
            foreach (PawnKindDef pawnKindDef2 in from pawnKindDef in DefDatabase<PawnKindDef>.AllDefs
                                                 where pawnKindDef.race.race != null
                                                 select pawnKindDef)
            {
                RaceGroupDef raceGroupDef = null;
				bool temp = TryGetRaceGroupDef(pawnKindDef2, out raceGroupDef);
            }
		}

		public static bool TryGetRaceGroupDef(PawnKindDef pawnKindDef, out RaceGroupDef raceGroupDef)
		{
			
			if (RaceGroupByPawnKind.TryGetValue(pawnKindDef, out raceGroupDef))
			{
				return raceGroupDef != null;
			}
			raceGroupDef = GetRaceGroupDefInternal(pawnKindDef);
			RaceGroupByPawnKind.Add(pawnKindDef, raceGroupDef);
			return raceGroupDef != null;
		}

		//slightly modified code so it also works racegroupdefs
		public static RaceGroupDef GetRaceGroupDefInternal(PawnKindDef kindDef)
		{
			string raceName = kindDef.race.defName;
			string pawnKindName = kindDef.defName;
			IEnumerable<RaceGroupDef> allDefs = DefDatabase<RaceGroupDef>.AllDefs;
			List<RaceGroupDef> list = allDefs.Where(delegate (RaceGroupDef group)
			{
				List<string> pawnKindNames = group.pawnKindNames;
				return pawnKindNames != null && pawnKindNames.Contains(pawnKindName);
			}).ToList<RaceGroupDef>();
			List<RaceGroupDef> list2 = allDefs.Where(delegate (RaceGroupDef group)
			{
				List<string> raceNames = group.raceNames;
				return raceNames != null && raceNames.Contains(raceName);
			}).ToList<RaceGroupDef>();
			int num = list.Count<RaceGroupDef>() + list2.Count<RaceGroupDef>();
			if (num == 0)
			{
				return null;
			}
			if (num == 1)
			{
				return list.Concat(list2).Single<RaceGroupDef>();
			}
			RaceGroupDef result;
			if ((result = list.FirstOrDefault((RaceGroupDef match) => !IsThisMod(match))) == null)
			{
				if ((result = list2.FirstOrDefault((RaceGroupDef match) => !IsThisMod(match))) == null)
				{
					result = (list.FirstOrDefault<RaceGroupDef>() ?? list2.FirstOrDefault<RaceGroupDef>());
				}
			}
			return result;
		}

		public static RaceGeneDef GetRaceGenDefInternal(PawnKindDef kindDef)
        {
			if (kindDef == null)
            {
				return null;
            }
			string raceName = kindDef.race.defName;
			string pawnKindName = kindDef.defName;
			RaceGroupDef raceGroupDef = GetRaceGroupDef(kindDef);
			//string raceGroupName = GetRaceGroupDef(kindDef).defName;
			IEnumerable<RaceGeneDef> allDefs = DefDatabase<RaceGeneDef>.AllDefs;
			Log.Message(allDefs.Count<RaceGeneDef>().ToString());
			List<RaceGeneDef> list = allDefs.Where(delegate (RaceGeneDef group)
			{
				List<string> pawnKindNames = group.pawnKindNames;
				return pawnKindNames != null && pawnKindNames.Contains(pawnKindName);
			}).ToList<RaceGeneDef>();
			List<RaceGeneDef> list2 = allDefs.Where(delegate (RaceGeneDef group)
			{
				List<string> raceNames = group.raceNames;
				return raceNames != null && raceNames.Contains(raceName);
			}).ToList<RaceGeneDef>();
			List<RaceGeneDef> list3 = new List<RaceGeneDef>();
			if (raceGroupDef != null)
			{
				Log.Message("found a raceGroupDef");
				Log.Message(raceGroupDef.defName);
				foreach (RaceGeneDef rgd in allDefs)
                {
					Log.Message(rgd.defName);
                }
				list3 = allDefs.Where(delegate (RaceGeneDef group)
				{
					String raceGroupDefName = group.raceGroup;
					return raceGroupDefName != null && raceGroupDefName == raceGroupDef.defName;
				}).ToList<RaceGeneDef>();
			}
			RaceGeneDef result = null;
			//First check if there is a matching pawnkinddef then race, then racegroup
			if (list.Any())
			{
				result = list.RandomElement();
			}
			else if (list2.Any() && result == null)
			{
				result = list2.RandomElement();
			}
			else if (list3.Any() && result == null)
			{
				result = list3.RandomElement();
			}
			else
            {
				result = null;
            }
			return result;


		}

		private static RaceGroupDef GetRaceGroupDef(PawnKindDef kindDef)
        {
			RaceGroupDef raceGroupDef = null;
			bool temp = TryGetRaceGroupDef(kindDef, out raceGroupDef);
			return raceGroupDef;
		}

		private static bool IsThisMod(Def def)
		{
			return LoadedModManager.RunningMods.Single((ModContentPack pack) => pack.Name == "RimJobWorld").AllDefs.Contains(def);
		}

		private static readonly IDictionary<PawnKindDef, RaceGroupDef> RaceGroupByPawnKind = new Dictionary<PawnKindDef, RaceGroupDef>();
    }

   
}
