using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rjw;
using Verse;

namespace RJW_Genes
{
    internal static class Patch_sexualize_pawn
    {
        /// <summary>
        /// Harmony Patch for RJW.Sexualizer.sexualize_pawn, Simply checks to see if the pawn already has genitals and skips the function entirely if the pawn has already got genitals of some sort, 
        /// may cause issues if pawn has all the  'no genitals' Genes? 
        /// </summary>
        internal static bool PreFix(Pawn pawn ) {
            //if (GenitaliaUtility.PawnStillNeedsGenitalia(pawn))
            if (!(Genital_Helper.has_genitals(pawn) || Genital_Helper.has_anus(pawn) || Genital_Helper.has_breasts(pawn)))
            {
                return true;
            }

            //DEBUG Info.
            string genitalList = "";
            foreach (Hediff genital in pawn.GetGenitalsList())
            {
                genitalList += genital.Label + ",";
            }
            ModLog.Debug($"RJW_Genes is currently pre-patching sexualize_pawn, and blocks it running if it detects the pawn already has genitals.");
            ModLog.Debug($"Pawn Already has some genitals, {genitalList}");
            return false;
        }
    }
}
