using Verse;
using UnityEngine;

namespace RJW_Genes
{
    public class RJW_Genes_SettingsController : Mod
    {
        public RJW_Genes_SettingsController(ModContentPack content) : base(content)
        {
            base.GetSettings<RJW_Genes_Settings>();
        }

        public override string SettingsCategory()
        {
            return "RJW Genes - General";
        }
        public override void DoSettingsWindowContents(Rect inRect)
        {
            RJW_Genes_Settings.DoWindowContents(inRect);
        }
    }
}
