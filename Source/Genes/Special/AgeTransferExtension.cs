using Verse;

namespace RJW_Genes
{
    public class AgeTransferExtension : DefModExtension
    {
        /// <summary>
        /// Amount by which the Biological Age Ticks will be changed.
        /// </summary>
        public int ageTickChange;

        /// <summary>
        /// Minimum Age for youthing to take place - pawns cannot end up underaged. 
        /// </summary>
        public int minAgeInYears;
    }
}