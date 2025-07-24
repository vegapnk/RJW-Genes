using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJW_Genes
{
    public class Gene_ChangeFluidType : RJW_Gene
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
                GenitaliaFluidChangeExtension fluidReplacements = def.GetModExtension<GenitaliaFluidChangeExtension>();
                if (fluidReplacements == null && RJW_Genes_Settings.rjw_genes_detailed_debug)
                {
                    ModLog.Error($"Gene {def} failed to change genitals - Need a modExtension with Class=\"{typeof(GenitaliaFluidChangeExtension).FullName}\".");
                    return;
                }

                FluidUtility.ChangeFluids(pawn, fluidReplacements.penisFluidReplacement, fluidReplacements.vaginaFluidReplacement, fluidReplacements.breastFluidReplacement, fluidReplacements.otherFluidReplacement);
            }
        }
    }
}
