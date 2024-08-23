using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{

    /// <summary>
    /// Small Extension that covers distances for Genes.
    /// This allows to set distance in the XMLs. 
    /// The distance is measured in tiles, so a distance of ~25 means 25 tiles away from the pawn in any direction. 
    /// </summary>
    public class DistanceExtension : DefModExtension
    {
        public int distance;
    }
}