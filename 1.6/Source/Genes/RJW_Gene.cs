using rjw;
using Verse;

namespace RJW_Genes
{
    public class RJW_Gene : Gene
    {

        public override void PostMake()
        {
            base.PostMake();

            if (GenitaliaUtility.PawnStillNeedsGenitalia(pawn))
                Sexualizer.sexualize_pawn(pawn);
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
