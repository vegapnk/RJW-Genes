using System;
using System.Collections.Generic;
using System.Linq;
using rjw;
using Verse;

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
		}

		public static List<RaceGeneDef> ValidRaceGeneDefs(Pawn pawn)
		{
			PawnKindDef kindDef = pawn.kindDef;
			if (kindDef == null)
			{
				RJW_Genes.ModLog.Warning($"Error looking up PawnKindDef for {pawn.Name} - Could not lookup Animal Inheritance Genes");
				return null;
			}
			
			string raceName = kindDef.race.defName;
			string pawnKindName = kindDef.defName;
			//Wild animals have no name, so we will use pawnkindname instead
			string pawnName = pawn.Name != null ? pawn.Name.ToStringFull : pawnKindName;

			RaceGroupDef raceGroupDef = GetRaceGroupDef(kindDef);

            RJW_Genes.ModLog.Debug($"Looking up Animal-Inheritable Genes for {pawnName} with KindDef {kindDef.defName},RaceName {raceName}, PawnKind {pawnKindName} and RaceGroup {raceGroupDef.defName}");

			IEnumerable<RaceGeneDef> allDefs = DefDatabase<RaceGeneDef>.AllDefs;
			List<RaceGeneDef> pawnKindDefs = allDefs.Where(delegate (RaceGeneDef group)
			{
				List<string> pawnKindNames = group.pawnKindNames;
				return pawnKindNames != null && pawnKindNames.Contains(pawnKindName);
			}).ToList<RaceGeneDef>();
			if (pawnKindDefs.Count() > 0)
            {
				DebugPrintRaceGeneDefs("PawnKindDefs", pawnName,pawnKindDefs);
				return pawnKindDefs;
			}
			RJW_Genes.ModLog.Debug($"Did not find PawnKindDefs for {pawnName}");

			List<RaceGeneDef> raceKindDefs = allDefs.Where(delegate (RaceGeneDef group)
			{
				List<string> raceNames = group.raceNames;
				return raceNames != null && raceNames.Contains(raceName);
			}).ToList<RaceGeneDef>();
			if (raceKindDefs.Count() > 0)
			{
				DebugPrintRaceGeneDefs("PawnKindDefs", pawnName, raceKindDefs);
				return raceKindDefs;
			}
            RJW_Genes.ModLog.Debug($"Did not find RaceKindDefs for {pawnName}");

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
            {
				DebugPrintRaceGeneDefs("RaceKindDefs", pawnName, raceGroupDefs);
				return raceGroupDefs;
			}
            RJW_Genes.ModLog.Debug($"Did not find RaceGroupDefs for {pawnName}");

			RJW_Genes.ModLog.Message($"Did not find any Genes inheritable for {pawnName}");
			return new List<RaceGeneDef>();
		}

		private static void DebugPrintRaceGeneDefs(String header,String identifier,List<RaceGeneDef> defs)
        {
			if (RJW_Genes.RJW_Genes_Settings.rjw_genes_detailed_debug)
            {
				var defString = "[";
				foreach (RaceGeneDef raceGeneDef in defs)
					defString += $"({raceGeneDef.priority}:{raceGeneDef.defName} - {raceGeneDef.genes.Count} Genes)";
				defString += "]";
				RJW_Genes.ModLog.Message($"Found the following {header}-Genes for {identifier}: {defString}");
			}
		}


		/// <summary>
		/// These two Functions are Duplicates of private functions in RJW RaceGroupDef_Helper, used to get a racegroupdef from a kindDef.
		/// If the RaceGroupDef_Helper class is made accessable directly, these functions can be removed.
		/// </summary>
		/// <param name="kindDef"></param>
		/// <returns></returns>
        private static RaceGroupDef GetRaceGroupDef(PawnKindDef kindDef)
        {
            var raceName = kindDef.race.defName;
            var pawnKindName = kindDef.defName;
            var groups = DefDatabase<RaceGroupDef>.AllDefs;

            var kindMatches = groups.Where(group => group.pawnKindNames?.Contains(pawnKindName) ?? false).ToList();
            var raceMatches = groups.Where(group => group.raceNames?.Contains(raceName) ?? false).ToList();
            var count = kindMatches.Count() + raceMatches.Count();
            if (count == 0)
            {
                //ModLog.Message($"Pawn named '{pawn.Name}' matched no RaceGroupDef. If you want to create a matching RaceGroupDef you can use the raceName '{raceName}' or the pawnKindName '{pawnKindName}'.");
                return null;
            }
            else if (count == 1)
            {
                // ModLog.Message($"Pawn named '{pawn.Name}' matched 1 RaceGroupDef.");
                return kindMatches.Concat(raceMatches).Single();
            }
            else
            {
                // ModLog.Message($"Pawn named '{pawn.Name}' matched {count} RaceGroupDefs.");

                // If there are multiple RaceGroupDef matches, choose one of them.
                // First prefer defs NOT defined in rjw.
                // Then prefer a match by kind over a match by race.
                return kindMatches.FirstOrDefault(match => !IsThisMod(match))
                    ?? raceMatches.FirstOrDefault(match => !IsThisMod(match))
                    ?? kindMatches.FirstOrDefault()
                    ?? raceMatches.FirstOrDefault();
            }
        }

        private static bool IsThisMod(Def def)
        {
            var rjwContent = LoadedModManager.RunningMods.Single(pack => pack.Name == "RimJobWorld");
            return rjwContent.AllDefs.Contains(def);
        }
    } 
}
