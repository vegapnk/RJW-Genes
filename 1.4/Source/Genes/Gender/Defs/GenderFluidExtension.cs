using Verse;

namespace RJW_Genes
{
    public class GenderFluidExtension : DefModExtension
    {
        /// <summary>
        /// Number of ticks until the change can be triggered.
        /// Just being "triggered" does not mean changing, see the changeChance below. 
        /// </summary>
        public int changeInterval;

        /// <summary>
        /// How high is the chance to change gender?
        /// Set to 1 for "always", set to 0 for "never". 
        /// Everything else is a bit statistics, but e.g. when set to .5 the chances grow per day from [50%, 75%, 82.25%, ...]
        /// </summary>
        public float changeChance;
    }
}