using System;
using System.Collections.Generic;
using rjw;
using rjw.Modules.Interactions.Contexts;
using rjw.Modules.Interactions.Enums;
using rjw.Modules.Interactions.Rules.PartKindUsageRules;
using rjw.Modules.Shared;
using Verse;

namespace RJW_Genes.Interactions
{
    //Summary//
    //Set custom preferences for pawn. Gets integrated into rjw by AddtoIPartPreferenceRule in First
    //Depending on the level of lifeforce increase the chance for using the mouth.
    public class GenesPartKindUsageRule : IPartPreferenceRule
    {
        public IEnumerable<Weighted<LewdablePartKind>> ModifiersForDominant(InteractionContext context)
        {
            Pawn pawn = context.Internals.Dominant.Pawn;
            Gene_LifeForce gene = pawn.genes.GetFirstGeneOfType<Gene_LifeForce>();
            if (gene != null)
            {
                float weight = 2f;
                if (gene.Value < gene.MinLevelForAlert)
                {
                    weight *= 10;
                }
                else if (gene.Value < gene.targetValue)
                {
                    weight *= 2.5f;
                }
                if (pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_cum_eater))
                {
                    yield return new Weighted<LewdablePartKind>(weight, LewdablePartKind.Mouth);
                    yield return new Weighted<LewdablePartKind>(weight, LewdablePartKind.Beak);
                }

                if (pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_fertilin_absorber))
                {
                    yield return new Weighted<LewdablePartKind>(weight, LewdablePartKind.Vagina);
                    yield return new Weighted<LewdablePartKind>(weight, LewdablePartKind.Anus);
                }
            }
            yield break;
        }

        public IEnumerable<Weighted<LewdablePartKind>> ModifiersForSubmissive(InteractionContext context)
        {
            Pawn pawn = context.Internals.Dominant.Pawn;
            Gene_LifeForce gene = pawn.genes.GetFirstGeneOfType<Gene_LifeForce>();
            if (gene != null)
            {
                float weight = 2f;
                if (gene.Value < gene.MinLevelForAlert)
                {
                    weight *= 10;
                }
                else if (gene.Value < gene.targetValue)
                {
                    weight *= 2.5f;
                }
                yield return new Weighted<LewdablePartKind>(weight, LewdablePartKind.Mouth);
                yield return new Weighted<LewdablePartKind>(weight, LewdablePartKind.Beak);

                if (pawn.genes.HasActiveGene(GeneDefOf.rjw_genes_fertilin_absorber))
                {
                    yield return new Weighted<LewdablePartKind>(weight, LewdablePartKind.Vagina);
                    yield return new Weighted<LewdablePartKind>(weight, LewdablePartKind.Anus);
                }
            }
            yield break;
        }
    }
}