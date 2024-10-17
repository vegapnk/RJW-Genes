using HarmonyLib;
using rjw;
using Verse;


namespace RJW_Genes
{

    /// <summary>
    /// Upon Transfer Fluids, the pawns store their current Food need (if applicable). 
    /// When running the postfix, all generous donours get their old food need restored if the current one is lower. 
    /// If there is any issue, the need is just set to -10 as an error value. 
    /// 
    /// Patched Method: https://gitgud.io/Ed86/rjw/-/blob/master/1.5/Source/Common/Helpers/SexUtility.cs?ref_type=heads#L992
    /// </summary>
    [HarmonyPatch(typeof(rjw.SexUtility), nameof(rjw.SexUtility.TransferFluids))]
    class Patch_TransferNutrition_Generous_Donor_PawnToPartner
    {

        static void Prefix(SexProps props, out float __state)
        {
            __state = -10.0f;
            if (props.pawn != null && props.pawn.needs != null && props.pawn.needs.food != null)
                __state = props.pawn.needs.food.CurLevel;

        }

        static void Postfix(SexProps props,float __state )
        {

            if (props.pawn != null && props.pawn.needs != null && props.pawn.needs.food != null && props.pawn.genes != null)
            {
                if (props.pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_generous_donor) && props.pawn.needs.food.CurLevel < __state)
                {
                    ModLog.Debug($"Found Generous Donor {props.pawn} Transferring Fluids - Resetting Food Need from {props.pawn.needs.food.CurLevel} to {__state}");
                    props.pawn.needs.food.CurLevel = __state;
                }
            }
        }
    }

    /// <summary>
    /// Harmony Patches only allow for one out-variable as state. 
    /// Thus I have simply a clone with the same behaviour. Why not have it easy sometimes. 
    /// </summary>
    [HarmonyPatch(typeof(rjw.SexUtility), nameof(rjw.SexUtility.TransferFluids))]
    class Patch_TransferNutrition_Generous_Donor_PartnerToPawn
    {

        static void Prefix(SexProps props,out float __state)
        {
            __state = -10.0f;
            if (props.partner != null && props.partner.needs != null && props.partner.needs.food != null)
                __state = props.partner.needs.food.CurLevel;
        }

        static void Postfix(SexProps props, float __state)
        {
            
            if (props.partner != null && props.partner.needs != null && props.partner.needs.food != null && props.partner.genes != null)
            {
                if (props.partner.genes.HasActiveGene(GeneDefOf.rjw_genes_generous_donor) && props.partner.needs.food.CurLevel < __state)
                {
                    ModLog.Debug($"Found Generous Donor {props.partner} Transferring Fluids - Resetting Food Need from {props.partner.needs.food.CurLevel} to {__state}");
                    props.partner.needs.food.CurLevel = __state;
                }
            }
        }
    }
}