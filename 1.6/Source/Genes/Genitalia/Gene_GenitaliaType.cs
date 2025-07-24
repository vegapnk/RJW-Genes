using rjw;

namespace RJW_Genes
{
    public class Gene_GenitaliaType : RJW_Gene
    {
        public override void PostMake()
        {
            base.PostMake();
        }

        public override void PostAdd()
        {
            if (pawn.kindDef == null) return;   //Added to catch Rimworld creating statues of pawns.
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