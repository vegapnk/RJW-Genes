using Verse;
using rjw.Modules.Interactions;
using rjw.Modules.Interactions.Internals.Implementation;
using rjw.Modules.Interactions.Objects;

//Modified code based of RJW-AI code at https://gitgud.io/Ed86/rjw-ia/-/tree/master/
namespace RJW_Genes
{
    [StaticConstructorOnStartup]
    public class SubSuccubusTailCustomRequirementHandler : ICustomRequirementHandler
    {
        public string HandlerKey
        {
            get
            {
                return "SubSuccubusTailCustomRequirementHandler";
            }
        }

        static SubSuccubusTailCustomRequirementHandler()
        {
            Register();
        }
        public static void Register()
        {
            InteractionRequirementService.CustomRequirementHandlers.Add(new SubSuccubusTailCustomRequirementHandler());
            if (Prefs.DevMode)
            {
                Log.Message("SubSuccubusTailCustomRequirementHandler registered: ");
            }
        }

        public bool FufillRequirements(InteractionWithExtension interaction, InteractionPawn dominant, InteractionPawn submissive)
        {
            if (GeneUtility.HasGeneNullCheck(submissive.Pawn, GeneDefOf.rjw_genes_succubus_tail))
            {
                return true;
            }
            return false;
        }
    }
}

