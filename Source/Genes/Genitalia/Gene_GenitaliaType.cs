using rjw;

namespace RJW_Genes
{
    // bleh plural attributive noun in keeping with naming convention
    public class Gene_GenitaliaType : RJW_Gene
    {
        public override void PostMake()
        {
            base.PostMake();
            Apply();
        }

        public override void PostAdd()
        {
            base.PostAdd();
            Apply();
        }

        protected virtual void Apply()
        {
            GenitaliaTypeExtension genitals = def.GetModExtension<GenitaliaTypeExtension>();
            if (genitals == null && RJW_Genes_Settings.rjw_genes_detailed_debug)
            {
                ModLog.Error($"Gene {def} failed to change genitals - Need a modExtension with Class=\"{typeof(GenitaliaTypeExtension).FullName}\".");
                return;
            }
            GenitaliaChanger.ChangeGenitalia(pawn, genitals.Penis, genitals.Vagina, genitals.Anus);
        }
    }
}