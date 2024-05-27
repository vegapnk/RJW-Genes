using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using Verse;
using rjw;
using RJW_Genes;
using rjw.Modules.Interactions.Enums;

namespace RJW_BGS
{
    [HarmonyPatch(typeof(PregnancyHelper))]
    public class PatchPregnancyHelper
    {
        // Token: 0x0600000F RID: 15
        [HarmonyPostfix]
        [HarmonyPatch("impregnate")]
        private static void ImpregnatePostfix(ref SexProps props)
        {
            
            if (RJWSettings.DevMode) rjw.ModLog.Message("Rimjobworld::impregnate(" + props.sexType + "):: " + xxx.get_pawnname(props.pawn) + " + " + xxx.get_pawnname(props.partner) + ":");

            //"mech" pregnancy
            if (props.sexType == xxx.rjwSextype.MechImplant)
            {
                return;
            }

            Pawn giver = props.pawn; // orgasmer
            Pawn receiver = props.partner;
            List<Hediff> pawnparts = giver.GetGenitalsList();
            List<Hediff> partnerparts = receiver.GetGenitalsList();
            var interaction = rjw.Modules.Interactions.Helpers.InteractionHelper.GetWithExtension(props.dictionaryKey);
            if (receiver.genes == null)
            {
                return;
            }

            if (!(props.sexType == xxx.rjwSextype.Anal && receiver.genes.HasActiveGene(RJW_Genes.GeneDefOf.rjw_genes_fertile_anus)))
                return;

            //"normal" and "beastial" pregnancy
            if (RJWSettings.DevMode) RJW_Genes.ModLog.Message(" 'normal' pregnancy checks");

            //interaction stuff if for handling futa/see who penetrates who in interaction
            if (!props.isReceiver &&
                interaction.DominantHasTag(GenitalTag.CanPenetrate) &&
                interaction.SubmissiveHasFamily(GenitalFamily.Anus))
            {
                if (RJWSettings.DevMode) RJW_Genes.ModLog.Message(" impregnate - by initiator");
            }
            else if (props.isReceiver && props.isRevese &&
                interaction.DominantHasFamily(GenitalFamily.Anus) &&
                interaction.SubmissiveHasTag(GenitalTag.CanPenetrate))
            {
                if (RJWSettings.DevMode) RJW_Genes.ModLog.Message(" impregnate - by receiver (reverse)");
            }
            else
            {
                if (RJWSettings.DevMode) RJW_Genes.ModLog.Message(" no valid interaction tags/family");
                return;
            }

            if (!rjw.Modules.Interactions.Helpers.PartHelper.FindParts(giver, GenitalTag.CanFertilize).Any())
            {
                if (RJWSettings.DevMode) RJW_Genes.ModLog.Message(xxx.get_pawnname(giver) + " has no parts to Fertilize with");
                return;
            }

            if (CanImpregnate2(giver, receiver, props.sexType))
            {
                PregnancyHelper.DoImpregnate(giver, receiver);
            }
        }

        private static bool CanImpregnate2(Pawn fucker, Pawn fucked, xxx.rjwSextype sexType)
        {

                if (fucker == null || fucked == null)
                {
                return false;

            }
            if (RJWSettings.DevMode)
                {
                    rjw.ModLog.Message(string.Concat(new string[]
                    {
                    "Rimjobworld::CanImpregnate checks (",
                    sexType.ToString(),
                    "):: ",
                    xxx.get_pawnname(fucker),
                    " + ",
                    xxx.get_pawnname(fucked),
                    ":"
                    }));
                }
                if (sexType == xxx.rjwSextype.MechImplant && !RJWPregnancySettings.mechanoid_pregnancy_enabled)
                {
                    if (RJWSettings.DevMode)
                    {
                    RJW_Genes.ModLog.Message(" mechanoid 'pregnancy' disabled");
                    }
                return false;

            }
            if (sexType != xxx.rjwSextype.Vaginal && sexType != xxx.rjwSextype.DoublePenetration && !(sexType == xxx.rjwSextype.Anal && fucked.genes.HasActiveGene(RJW_Genes.GeneDefOf.rjw_genes_fertile_anus)))
                {
                    if (RJWSettings.DevMode)
                    {
                    RJW_Genes.ModLog.Message(" sextype cannot result in pregnancy");
                    }
                return false;

            }
            if (AndroidsCompatibility.IsAndroid(fucker) && AndroidsCompatibility.IsAndroid(fucked))
                {
                    if (RJWSettings.DevMode)
                    {
                    RJW_Genes.ModLog.Message(xxx.get_pawnname(fucked) + " androids cant breed/reproduce androids");
                    }
                return false;

            }
            if ((fucker.IsUnsexyRobot() || fucked.IsUnsexyRobot()) && sexType != xxx.rjwSextype.MechImplant)
                {
                    if (RJWSettings.DevMode)
                    {
                    RJW_Genes.ModLog.Message(" unsexy robot cant be pregnant");
                    }
                return false;

            }
            if (!fucker.RaceHasPregnancy())
                {
                    if (RJWSettings.DevMode)
                    {
                    RJW_Genes.ModLog.Message(xxx.get_pawnname(fucked) + " filtered race that cant be pregnant");
                    }
                return false;

            }
            if (!fucked.RaceHasPregnancy())
                {
                    if (RJWSettings.DevMode)
                    {
                    RJW_Genes.ModLog.Message(xxx.get_pawnname(fucker) + " filtered race that cant impregnate");
                    }
                return false;

            }
            if (fucked.IsPregnant(false))
                {
                    if (RJWSettings.DevMode)
                    {
                    RJW_Genes.ModLog.Message(" already pregnant.");
                    }
                return false;

            }
            List<Hediff_InsectEgg> source = new List<Hediff_InsectEgg>();
                fucked.health.hediffSet.GetHediffs<Hediff_InsectEgg>(ref source, null);
                if (source.Count > 1)
                {
                    if (RJWSettings.DevMode)
                    {
                    RJW_Genes.ModLog.Message(xxx.get_pawnname(fucked) + " cant get pregnant while eggs inside");
                    }
                return false;

            }
            List<Hediff> genitalsList = fucker.GetGenitalsList();
                List<Hediff> genitalsList2 = fucked.GetGenitalsList();
            if (!Genital_Helper.has_penis_fertile(fucker, genitalsList)  && !Genital_Helper.has_penis_fertile(fucked, genitalsList2))
            {
                if (RJWSettings.DevMode)
                {
                    RJW_Genes.ModLog.Message(" missing genitals for impregnation"+ Genital_Helper.has_penis_fertile(fucker, genitalsList)+"  "+ Genital_Helper.has_anus(fucked, genitalsList2)+"  "+ Genital_Helper.has_penis_fertile(fucked, genitalsList2)+"   "+ Genital_Helper.has_anus(fucker, genitalsList));
                }
                return false;
            }
            if (fucker.health.capacities.GetLevel(xxx.reproduction) <= 0f || fucked.health.capacities.GetLevel(xxx.reproduction) <= 0f)
                {
                    if (RJWSettings.DevMode)
                    {
                    RJW_Genes.ModLog.Message(" one (or both) pawn(s) infertile");
                    }
                return false;

            }
            if (xxx.is_human(fucked) && xxx.is_human(fucker) && (RJWPregnancySettings.humanlike_impregnation_chance == 0 || !RJWPregnancySettings.humanlike_pregnancy_enabled))
                {
                    if (RJWSettings.DevMode)
                    {
                    RJW_Genes.ModLog.Message(" human pregnancy chance set to 0% or pregnancy disabled.");
                    }
                return false;

            }
            if (((xxx.is_animal(fucker) && xxx.is_human(fucked)) || (xxx.is_human(fucker) && xxx.is_animal(fucked))) && !RJWPregnancySettings.bestial_pregnancy_enabled)
                {
                    if (RJWSettings.DevMode)
                    {
                    RJW_Genes.ModLog.Message(" bestiality pregnancy chance set to 0% or pregnancy disabled.");
                    }
                return false;

            }
            if (xxx.is_animal(fucked) && xxx.is_animal(fucker) && (RJWPregnancySettings.animal_impregnation_chance == 0 || !RJWPregnancySettings.animal_pregnancy_enabled))
                {
                    if (RJWSettings.DevMode)
                    {
                    RJW_Genes.ModLog.Message(" animal-animal pregnancy chance set to 0% or pregnancy disabled.");
                    }
                return false;

            }
            if (fucker.def.defName != fucked.def.defName && RJWPregnancySettings.interspecies_impregnation_modifier <= 0f && !RJWPregnancySettings.complex_interspecies)
                {
                    if (RJWSettings.DevMode)
                    {
                    RJW_Genes.ModLog.Message(" interspecies pregnancy disabled.");
                    }
                return false;

            }
            if (fucked.RaceProps.gestationPeriodDays <= 0f && fucked.TryGetComp<CompEggLayer>() == null)
                {
                    if (RJWSettings.DevMode)
                    {
                    RJW_Genes.ModLog.Message(xxx.get_pawnname(fucked) + " mother.RaceProps.gestationPeriodDays is " + fucked.RaceProps.gestationPeriodDays.ToString() + " cant impregnate");
                    }
                    return false;
                }

                return true;
            }
        
    }
}
