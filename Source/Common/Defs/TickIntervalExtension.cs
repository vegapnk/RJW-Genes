using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    /// <summary>
    /// General DefModExtension to cover various genes that need to tick regularly.
    /// Defining it like this allows to adjust things in XML. 
    /// </summary>
    public class TickIntervalExtension : DefModExtension
    {
        public int tickInterval;
    }
}
