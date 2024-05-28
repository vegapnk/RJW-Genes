using Verse;

namespace RJW_Genes
{
    public static class RJW_GenesLogger
    {
        public static void Message(string message)
        {
            Log.Message("[INFO][RJW_Genes] - " + message);
        }

        public static void Warning(string message)
        {
            Log.Message("[WARN][RJW_Genes] - " + message);
        }

        public static void Error(string message)
        {
            Log.Message("[ ERR][RJW_Genes] - " + message);
        }

        public static void MessageGroupHead(string message)
        {
            Log.Message("[INFO][RJW_Genes]╦═ " + message);
        }
        public static void MessageGroupBody(string message)
        {
            Log.Message("[INFO][RJW_Genes]╠═══ " + message);
        }
        public static void MessageGroupFoot(string message)
        {
            Log.Message("[INFO][RJW_Genes]╚═══ " + message);
        }
    }
}
