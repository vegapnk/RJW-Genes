using rjw;

namespace RJW_Genes
{
    // bleh plural attributive noun in keeping with naming convention
    public class Gene_GenitaliaType : RJW_Gene
    {
        public override void PostMake()
        {
            base.PostMake();
           
        }

        public override void PostAdd()
        {
            base.PostAdd();
            Apply();
        }

        public virtual void Apply()
        {
            if (this.Active)
            {
                GenitaliaTypeExtension genitals = def.GetModExtension<GenitaliaTypeExtension>();
                if (genitals == null && RJW_Genes_Settings.rjw_genes_detailed_debug)
                {
                    ModLog.Error($"Gene {def} failed to change genitals - Need a modExtension with Class=\"{typeof(GenitaliaTypeExtension).FullName}\".");
                    return;
                }
                GenitaliaChanger.ChangeGenitalia(pawn, genitals.penis, genitals.vagina, genitals.anus, genitals.breasts);
            }
        }
    }
}