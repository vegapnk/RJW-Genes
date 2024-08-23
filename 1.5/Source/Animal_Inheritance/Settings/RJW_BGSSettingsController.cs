using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;

namespace RJW_BGS
{
    public class RJW_BGSSettingsController : Mod
    {
        public RJW_BGSSettingsController(ModContentPack content) : base(content)
        {
            base.GetSettings<RJW_BGSSettings>();
        }

        public override string SettingsCategory()
        {
            return "RJW Genes Animal Gene Inheritance";
        }
        public override void DoSettingsWindowContents(Rect inRect)
        {
            RJW_BGSSettings.DoWindowContents(inRect);
        }
    }
}
