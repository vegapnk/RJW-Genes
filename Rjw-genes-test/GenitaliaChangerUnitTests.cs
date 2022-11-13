using Xunit;


using RJW_Genes;
using Verse;

namespace Rjw_genes_test
{
    public class GenitaliaChangerUnitTests
    {
        [Fact]
        public void TestChangeGenitalia()
        {
            Pawn example = Verse.PawnGenerator.GeneratePawn(new PawnGenerationRequest());
        }
    }
}