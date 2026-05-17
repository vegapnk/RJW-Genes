using Verse;

namespace RJW_Genes
{

    /// <summary>
    /// Used by the 'On Sexualization' Patch to determine if a gene needs to be applied, and what order to do so.
    /// This Extension is applied to RJW related genes that Depend on the order they are Applied in, 
    /// using the following basic values
    /// 
    /// 10 Unused.
    /// 20 Genes that Alter the Number of sexual parts a Pawn may have. (Includes the Zero Genes?)
    /// 30 Genes that Alter the type of genitals a Pawn may have.
    /// 40 Genes that Add a specific genital type to the pawn.
    /// 50 Genes that alter the values on a pawns genitals such as size or fluid type.
    /// </summary>
#pragma warning disable CS0649  //Disable the warning for a variable that is 'unset'.
    internal class SexualizerGeneExtension : DefModExtension
    {

        public int order;

    }
#pragma warning restore CS0649
}
