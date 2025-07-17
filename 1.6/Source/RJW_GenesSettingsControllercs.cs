using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;
namespace RJW_Genes
{
    public class RJW_GenesSettingsControllercs : Mod
    {
        public RJW_GenesSettingsControllercs(ModContentPack content) : base(content)
        {
            base.GetSettings<RJW_GenesSettings>();
        }

        public override string SettingsCategory()
        {
            return "RJW Genes";
        }
        public override void DoSettingsWindowContents(Rect inRect)
        {
            RJW_GenesSettings.DoWindowContents(inRect);
        }
    }
}
