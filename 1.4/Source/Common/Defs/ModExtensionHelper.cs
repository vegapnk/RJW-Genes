using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    internal class ModExtensionHelper
    {

        public static int GetDistanceFromModExtension(GeneDef defOf, int fallback)
        {
            DistanceExtension distanceExt = defOf.GetModExtension<DistanceExtension>();

            int potentialDistance = distanceExt?.distance ?? fallback;

            if (potentialDistance > 0)
            {
                return potentialDistance;
            } else {
                ModLog.Warning($"Retrieved a bad distance ({potentialDistance}) reading the RJW_Genes.DistanceExtension for {defOf.defName}");    
                return 1; 
            }
        }


        public static int GetTickIntervalFromModExtension(GeneDef defOf, int fallback)
        {
            TickIntervalExtension tickExt = defOf.GetModExtension<TickIntervalExtension>();

            int potentialTickInterval = tickExt?.tickInterval ?? fallback;

            if (potentialTickInterval > 0)
            {
                return potentialTickInterval;
            }
            else
            {
                ModLog.Warning($"Retrieved a bad distance ({potentialTickInterval}) reading the RJW_Genes.DistanceExtension for {defOf.defName}");
                return 1;
            }
        }


    }
}
