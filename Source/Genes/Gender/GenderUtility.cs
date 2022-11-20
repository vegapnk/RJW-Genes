using Verse;
using rjw;
using RimWorld;
using System.Collections;
using System.Linq;
using System;

namespace RJW_Genes
{
    public class GenderUtility
    {
        /// <summary>
        /// Returns if a Pawn is female (Gender==Female) or if it should be (Gene==FemaleOnly)
        /// This is used as a small helper, as the genes might fire in different orders.
        /// </summary>
        public static bool IsFemale(Pawn pawn)
        {
            return
                pawn.gender == Gender.Female || pawn.genes.GenesListForReading.Any(gene => gene.def.defName.EqualsIgnoreCase(GeneDefOf.rjw_genes_female_only.defName));
        }

        /// <summary>
        /// Returns if a Pawn is male (Gender==Male) or if it should be (Gene==MaleOnly)
        /// This is used as a small helper, as the genes might fire in different orders.
        /// </summary>
        public static bool IsMale(Pawn pawn)
        {
            return
                pawn.gender == Gender.Male || pawn.genes.GenesListForReading.Any(gene => gene.def.defName.EqualsIgnoreCase(GeneDefOf.rjw_genes_male_only.defName));
        }

        /// <summary>
        /// Adjusts the Body Type to match the given target gender 
        /// (for male and female only, baby,child and hulks don't change)
        /// </summary>
        /// <param name="pawn"></param>
        /// <param name="targetGender"></param>
        public static void AdjustBodyToTargetGender(Pawn pawn, Gender targetGender)
        {
            if (pawn == null)
                return;
            if (pawn.story.bodyType == BodyTypeDefOf.Baby || pawn.story.bodyType == BodyTypeDefOf.Hulk || pawn.story.bodyType == BodyTypeDefOf.Child)
                return;

            if (targetGender == Gender.Male)
            {
                pawn.story.bodyType = BodyTypeDefOf.Male;
            }
            else if (targetGender == Gender.Female)
            {
                pawn.story.bodyType = BodyTypeDefOf.Female; 
                pawn.style.beardDef = BeardDefOf.NoBeard;
            }

            // Re-Choose heads if it is wrong gender
            if (pawn.story.headType.gender == Gender.None || pawn.story.headType.gender == targetGender)
            {
                // Do nothing, Gender of Heat is Neutral or matches
            }
            else
            {
                // Below line tries to get (and set) an available head from the backstory, if it returns true everything worked if it returns false we log it
                if(! pawn.story.TryGetRandomHeadFromSet(DefDatabase<HeadTypeDef>.AllDefs.Where((Func<HeadTypeDef, bool>)(x => x.randomChosen))))
                {
                    Log.Message("Failed to retrieve a correct-gender head for the pawn " + pawn.Name);
                };
            } 

            // Force Redraw at the spot
            pawn.Drawer.renderer.graphics.SetAllGraphicsDirty();
        }
    }
}
