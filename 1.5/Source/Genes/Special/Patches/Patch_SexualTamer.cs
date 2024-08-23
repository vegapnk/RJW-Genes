using HarmonyLib;
using RimWorld;
using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using static RJWSexperience.RsDefOf;

namespace RJW_Genes
{

    [HarmonyPatch(typeof(SexUtility), nameof(SexUtility.SatisfyPersonal))]
    public static class Patch_SexualTamer
    {
        
        public static void Postfix(SexProps props)
        {
            // ShortCuts: Exit Early if Pawn or Partner are null (can happen with Masturbation or other nieche-cases)
            if (props == null || props.pawn == null || !props.hasPartner() || props.partner == null)
                return;
            // Exit for non Animals or Animal on Animal
            if (!(props.pawn.IsAnimal() || props.partner.IsAnimal() ) )
                return;
            Pawn animal =  props.pawn.IsAnimal() ? props.pawn : props.partner;
            Pawn human = props.pawn.IsAnimal() ? props.partner : props.pawn;

            // Another Short Sanity Check
            if (animal == null || human == null ) return;
            if (human.genes == null) return;

            if (human.genes.HasActiveGene(GeneDefOf.rjw_genes_sex_tamer))
            {
                // Case 1: Wild Animal - Try to Tame
                if (animal.AnimalOrWildMan() && animal.Faction == null)
                {
                    if(RJW_Genes_Settings.rjw_genes_detailed_debug)
                        ModLog.Message($"{human} is a sextamer with bestiality on wild animal {animal} - trying to recruit");
                    human.interactions.TryInteractWith(animal, InteractionDefOf.TameAttempt);
                }
                // Case 2: Colony Animal - Try to Train 
                else if (human.Faction != null && animal.Faction == human.Faction && animal.training != null)
                {
                    if (RJW_Genes_Settings.rjw_genes_detailed_debug)
                        ModLog.Message($"{human} is a sextamer with bestiality on colony animal {animal} - trying to train");
                    if (animal.training == null) return;
                    var trainable = animal.training.NextTrainableToTrain();
                    if (trainable == null) return;
                    animal.training.Train(trainable, human);
                }
            }
        }

    }


}
