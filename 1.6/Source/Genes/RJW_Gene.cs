using rjw;
using Verse;

namespace RJW_Genes
{
    public class RJW_Gene : Gene
    {
        /// <summary>
        /// PostMake is called after the gene is first in instanciated by Rimworld.Pawn_Genetracker , this is done just prior to the gene being added to the pawn. 
        /// </summary>
        public override void PostMake()
        {
            base.PostMake();
        }

        /// <summary>
        /// The add function is what alters the Pawn when the gene is added, PostAdd is called at the end of the AddGene function in Rimworld.Pawn_Genetracker
        /// </summary>
        public override void PostAdd()
        {
            if (pawn.kindDef == null) return;   //Added to catch Rimworld creating statues of pawns.
            if (GenitaliaUtility.PawnStillNeedsGenitalia(pawn))
            {
                Sexualizer.sexualize_pawn(pawn);
            }
            base.PostAdd();

        }


        /// <summary>
        /// Executed via PawnGenerator.GenerateGenes at Pawn generation
        /// Allows for execution of code that should only happen during PawnGeneration
        /// 
        /// This has an accompanying patch `Patch_AddNotifyOnGeneration.cs`.
        /// </summary>
        public virtual void Notify_OnPawnGeneration()
        {
        }
    }
}
