using Verse;
using RimWorld;
using rjw;

#pragma warning disable IDE0044, CS0649 // Private fields set when def is loaded

namespace RJW_Genes
{
    public class GenitaliaTypeExtension : DefModExtension
    {
        private HediffDef_PartBase penis;
        public HediffDef_PartBase Penis => penis ?? (HediffDef_PartBase) Genital_Helper.average_penis;

        private HediffDef_PartBase vagina;
        public HediffDef_PartBase Vagina => vagina ?? (HediffDef_PartBase) Genital_Helper.average_vagina;

        private HediffDef_PartBase anus;
        public HediffDef_PartBase Anus => anus ?? (HediffDef_PartBase) Genital_Helper.average_anus;
    }
}
