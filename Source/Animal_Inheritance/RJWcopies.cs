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

		
		public static RaceGroupDef GetRaceGroupDefInternal(PawnKindDef kindDef)
		{
			string raceName = kindDef.race.defName;
			string pawnKindName = kindDef.defName;
			IEnumerable<RaceGroupDef> allDefs = DefDatabase<RaceGroupDef>.AllDefs;
			List<RaceGroupDef> pawnKindDefs = allDefs.Where(delegate (RaceGroupDef group)
			{
				List<string> pawnKindNames = group.pawnKindNames;
				return pawnKindNames != null && pawnKindNames.Contains(pawnKindName);
			}).ToList<RaceGroupDef>();
			List<RaceGroupDef> raceNameDefs = allDefs.Where(delegate (RaceGroupDef group)
			{
				List<string> raceNames = group.raceNames;
				return raceNames != null && raceNames.Contains(raceName);
			}).ToList<RaceGroupDef>();

			int availableDefs = pawnKindDefs.Count<RaceGroupDef>() + raceNameDefs.Count<RaceGroupDef>();
			if (availableDefs == 0)
			{
				//Exit Early
				return null;
			}
			if (availableDefs == 1)
			{
				return pawnKindDefs.Concat(raceNameDefs).Single<RaceGroupDef>();
			}

			RaceGroupDef result;
			if ((result = pawnKindDefs.FirstOrDefault((RaceGroupDef match) => !IsThisMod(match))) == null)
			{
				if ((result = raceNameDefs.FirstOrDefault((RaceGroupDef match) => !IsThisMod(match))) == null)
				{
					result = (pawnKindDefs.FirstOrDefault<RaceGroupDef>() ?? raceNameDefs.FirstOrDefault<RaceGroupDef>());
				}
			}

			return result;
		}

		//slightly modified copy of code above so it also works for racegenedefs
		public static RaceGeneDef GetRaceGenDefInternal(PawnKindDef kindDef)
        {
			if (kindDef == null)
            {
				return null;
            }
			string raceName = kindDef.race.defName;
			string pawnKindName = kindDef.defName;
			RaceGroupDef raceGroupDef = GetRaceGroupDef(kindDef);
			IEnumerable<RaceGeneDef> allDefs = DefDatabase<RaceGeneDef>.AllDefs;
			Log.Message(allDefs.Count<RaceGeneDef>().ToString());
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
				/*
				// Log Messages for Debugging Only, prints the Genes found for this individual
				Log.Message("found a raceGroupDef");
				Log.Message(raceGroupDef.defName);
				foreach (RaceGeneDef rgd in allDefs)
                {
					Log.Message(rgd.defName);
                }
				*/
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
