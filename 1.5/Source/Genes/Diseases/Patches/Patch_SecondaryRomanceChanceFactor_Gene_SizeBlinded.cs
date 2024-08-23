using HarmonyLib;
using RimWorld;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    /// <summary>
    /// This patch helps with the gene `rjw_genes_size_blinded`. 
    /// Within RJW the CasualSexHelper utilizes the basefunction "pawn.relations.SecondaryRomanceChanceFactor"
    /// https://gitgud.io/Ed86/rjw/-/blob/master/1.5/Source/Common/Helpers/CasualSex_Helper.cs
    /// 
    /// We check on hookup for the other pawn if they have a penis. 
    /// If yes, we modulate the romance chance based on the following: 
    /// (Severity * BodySize - 0.5) * romance_multiplier
    /// So pawns with a cock smaller than 0.5 will be penalized, while pawns with more than 0.5 will be preferred. 
    /// </summary>
    [HarmonyPatch(typeof(Pawn_RelationsTracker), "SecondaryRomanceChanceFactor")]
    public class Patch_SecondaryRomanceChanceFactor_Gene_SizeBlinded
    {

        const float romance_multiplier = 2f;

        public static void Postfix( Pawn ___pawn, Pawn otherPawn, ref float __result)
        {
            if (otherPawn == null || ___pawn == null || ___pawn.genes == null || otherPawn.genes == null)
            {
                return;
            }
            if (___pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_size_blinded) && Genital_Helper.has_penis_fertile(otherPawn) || (Genital_Helper.has_penis_infertile(otherPawn)))
            {
                Hediff biggest_cock = GenitaliaUtility.GetBiggestPenis(otherPawn);
                if (biggest_cock != null)
                {
                    float bodysize = GenitaliaUtility.GetBodySizeOfSexPart(biggest_cock);
                    // Bodysize can only be a bonus, not a minus.
                    bodysize = Math.Max(1.0f, bodysize);

                    float attraction_bonus = (biggest_cock.Severity * bodysize - 0.5f) * romance_multiplier;
                    float result_backup = __result;
                    __result += attraction_bonus;
                    // Don't make it smaller than 0, to not get issues. 
                    __result = __result < 0 ? 0.0f : __result;

                    ModLog.Debug($"Gene_SizeBlind: Modulate Romance-Chance {___pawn}-->{otherPawn} from {result_backup} by {attraction_bonus} to {__result}");
                }
            }
        }

    }
}
