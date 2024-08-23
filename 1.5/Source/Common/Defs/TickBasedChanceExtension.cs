using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    public class TickBasedChanceExtension : TickIntervalExtension
    {
        /// <summary>
        /// Set to 1 for "always", set to 0 for "never". 
        /// Everything else is a bit statistics, but e.g. when set to .5 the chances grow per day from [50%, 75%, 82.25%, ...]
        /// </summary>
        public float eventChance;
    }
}
