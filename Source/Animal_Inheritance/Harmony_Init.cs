using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Verse;

namespace RJW_BGS
{
	[StaticConstructorOnStartup]
	internal static class HarmonyInit
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000029A4 File Offset: 0x00000BA4
		static HarmonyInit()
		{
			Harmony harmony = new Harmony("RJW_BGS");
			harmony.PatchAll();
		}
	}
}
