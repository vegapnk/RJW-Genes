using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    public class HormonalSalivaExtension : DefModExtension
    {
        /// <summary>
        /// How much the genitalia will growth per interaction.
        /// This is applied "flat", so if you have penis 0.5 and growthRate 0.05 it goes to 0.55, 0.60, 0.65 etc. 
        /// </summary>
        public float sizeIncrement;
        /// <summary>
        /// Upper Limit for the body size - default should be 2-3
        /// </summary>
        public float maxBodySize;
        /// <summary>
        /// How much more cum the pawn will make.
        /// This is applied as multiplication, so if you have cum 20 and multiplier 1.1 you will have 22,24,27 etc. 
        /// This leads to exponential growth, so try to keep it kinda low. 
        /// </summary>
        public float cumMultiplier;
    }
}
