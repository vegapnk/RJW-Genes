using Verse;

namespace RJW_Genes
{
    internal class ModLog
	{
		public static string ModId => "RJW-Genes";

		/// <summary>
		/// Logs the given message with [SaveStorage.ModId] appended.
		/// </summary>
		public static void Error(string message)
		{
			Log.Error($"[{ModId}] {message}");
		}

		/// <summary>
		/// Logs the given message with [SaveStorage.ModId] appended.
		/// </summary>
		public static void Message(string message)
		{
			Log.Message($"[{ModId}] {message}");
		}

		/// <summary>
		/// Logs the given message with [SaveStorage.ModId] appended.
		/// </summary>
		public static void Warning(string message)
		{
			Log.Warning($"[{ModId}] {message}");
		}

		public static void Debug(string message)
		{
			if (RJW_Genes_Settings.rjw_genes_detailed_debug)
			{
				Log.Message($"[{ModId}][debug] {message}");
			}
		}
	}
}
