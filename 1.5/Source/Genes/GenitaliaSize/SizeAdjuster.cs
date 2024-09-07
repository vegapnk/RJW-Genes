using Verse;
using rjw;
using System;
using System.Collections.Generic;

namespace RJW_Genes
{
    public class SizeAdjuster
    {

        /// <summary>
        /// Re-Rolls the sizes for all vaginas of the pawn to be between lower and upper limit.
        /// </summary>
        /// <param name="pawn">The pawn whos vaginas are rerolled</param>
        /// <param name="lowerLimit">The minimum severity for the vagina</param>
        /// <param name="upperLimit">The maximum severity for the vagina</param>
        public static void AdjustAllVaginaSizes(Pawn pawn, float lowerLimit = 0.0f, float upperLimit = 1.0f){
            List<Hediff> AllVaginas = Genital_Helper.get_AllPartsHediffList(pawn).FindAll(x => Genital_Helper.is_vagina(x));
            ResizeAll(AllVaginas, lowerLimit, upperLimit);
        }



        /// <summary>
        /// Re-Rolls the sizes for all anus of the pawn to be between lower and upper limit.
        /// </summary>
        /// <param name="pawn">The pawn whos anus are rerolled</param>
        /// <param name="lowerLimit">The minimum severity for the anus</param>
        /// <param name="upperLimit">The maximum severity for the anus</param>
        public static void AdjustAllAnusSizes(Pawn pawn, float lowerLimit = 0.0f, float upperLimit = 1.0f)
        {
            List<Hediff> AllAnus = Genital_Helper.get_AllPartsHediffList(pawn).FindAll(x => GenitaliaChanger.IsAnus(x));
            ResizeAll(AllAnus, lowerLimit, upperLimit);
        }



        /// <summary>
        /// Re-Rolls the sizes for all penis of the pawn to be between lower and upper limit.
        /// </summary>
        /// <param name="pawn">The pawn whos penisses are rerolled</param>
        /// <param name="lowerLimit">The minimum severity for the vagina</param>
        /// <param name="upperLimit">The maximum severity for the vagina</param>
        public static void AdjustAllPenisSizes(Pawn pawn, float lowerLimit = 0.0f, float upperLimit = 1.0f)
        {
            List<Hediff> AllPenisses = Genital_Helper.get_AllPartsHediffList(pawn).FindAll(x => Genital_Helper.is_penis(x));
            ResizeAll(AllPenisses, lowerLimit, upperLimit);
        }



        /// <summary>
        /// Re-Rolls the sizes for all breasts of the pawn to be between lower and upper limit.
        /// </summary>
        /// <param name="pawn">The pawn whos breasts are rerolled</param>
        /// <param name="lowerLimit">The minimum severity for the vagina</param>
        /// <param name="upperLimit">The maximum severity for the vagina</param>
        public static void AdjustAllBreastSizes(Pawn pawn, float lowerLimit = 0.0f, float upperLimit = 1.0f)
        {
            List<Hediff> AllBreasts = Genital_Helper.get_AllPartsHediffList(pawn).FindAll(x => x.def.defName.ToLower().Contains("breasts"));
            ResizeAll(AllBreasts,lowerLimit,upperLimit);
        }


        private static void ResizeAll(IEnumerable<Hediff> toResize,float lowerLimit, float upperLimit)
        {
            foreach (var hediff in toResize)
            {
                if (hediff is ISexPartHediff casted)
                {
                    Random rnd = new Random();
                    float size = (float)(rnd.NextDouble() * (upperLimit - lowerLimit) + lowerLimit);

                    casted.GetPartComp().baseSize = size;
                    casted.GetPartComp().UpdateSeverity();
                }
            }
        }
    }
}

    

