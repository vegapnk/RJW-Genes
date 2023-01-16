using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
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
				ModLog.Warning($"Error looking up PawnKindDef for {pawn.Name} - Could not lookup Animal Inheritance Genes");
				return null;
			}
			string raceName = kindDef.race.defName;
			string pawnKindName = kindDef.defName;
			PawnData pawnData = SaveStorage.DataStore.GetPawnData(pawn);
			RaceGroupDef raceGroupDef = pawnData.RaceSupportDef;

			if (RJW_BGSSettings.rjw_bgs_detailed_debug)
				ModLog.Message($"Looking up Animal-Inheritable Genes for {pawn.Name} with KindDef {kindDef.defName},RaceName {raceName}, PawnKind {pawnKindName} and RaceGroup {raceGroupDef.defName}");

			IEnumerable<RaceGeneDef> allDefs = DefDatabase<RaceGeneDef>.AllDefs;
			List<RaceGeneDef> pawnKindDefs = allDefs.Where(delegate (RaceGeneDef group)
			{
				List<string> pawnKindNames = group.pawnKindNames;
				return pawnKindNames != null && pawnKindNames.Contains(pawnKindName);
			}).ToList<RaceGeneDef>();
			if (pawnKindDefs.Count() > 0)
            {
				DebugPrintRaceGeneDefs("PawnKindDefs",pawn.Name.ToStringFull,pawnKindDefs);
				return pawnKindDefs;
			}
			else if (RJW_BGSSettings.rjw_bgs_detailed_debug)
				ModLog.Message($"Did not find PawnKindDefs for {pawn.Name.ToStringFull}");

			List<RaceGeneDef> raceKindDefs = allDefs.Where(delegate (RaceGeneDef group)
			{
				List<string> raceNames = group.raceNames;
				return raceNames != null && raceNames.Contains(raceName);
			}).ToList<RaceGeneDef>();
			if (raceKindDefs.Count() > 0)
			{
				DebugPrintRaceGeneDefs("PawnKindDefs", pawn.Name.ToStringFull, raceKindDefs);
				return raceKindDefs;
			}
			else if (RJW_BGSSettings.rjw_bgs_detailed_debug)
				ModLog.Message($"Did not find RaceKindDefs for {pawn.Name.ToStringFull}");

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
				DebugPrintRaceGeneDefs("RaceKindDefs", pawn.Name.ToStringFull, raceGroupDefs);
				return raceGroupDefs;
			}
			else if (RJW_BGSSettings.rjw_bgs_detailed_debug)
				ModLog.Message($"Did not find RaceGroupDefs for {pawn.Name.ToStringFull}");

			ModLog.Message($"Did not find any Genes inheritable for {pawn.Name.ToStringFull}");
			return new List<RaceGeneDef>();
		}

		private static void DebugPrintRaceGeneDefs(String header,String identifier,List<RaceGeneDef> defs)
        {
			if (RJW_BGSSettings.rjw_bgs_detailed_debug)
            {
				var defString = "[";
				foreach (RaceGeneDef raceGeneDef in defs)
					defString += $"({raceGeneDef.priority}:{raceGeneDef.defName} - {raceGeneDef.genes.Count} Genes)";
				defString += "]";
				ModLog.Message($"Found the following {header}-Genes for {identifier}: {defString}");
			}
		}
    } 
}
