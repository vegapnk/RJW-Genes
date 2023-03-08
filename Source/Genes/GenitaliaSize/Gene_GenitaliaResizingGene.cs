namespace RJW_Genes
{
    /// <summary>
    /// Parent Gene for Genitalia Resizing. All Resizing genes should inherit for this class. 
    /// 
    /// This helps with some functions (e.g. "hasGenitaliaResizingGenes(pawn)") but also to fire genitalia resizing later in life for Pawns. 
    /// (No Children with huge ding dongs, and I don't want kids with tight anuses I am not that degenerate)
    /// </summary>
    public abstract class Gene_GenitaliaResizingGene : RJW_Gene
    {

        public const int RESIZING_AGE = 20;
        public bool WasApplied { get; set; }

        public override void PostMake()
        {
            base.PostMake();
            if (pawn.ageTracker.AgeBiologicalYears >= RESIZING_AGE)
            {
                Resize();
                WasApplied = true;
            }
        }

        public override void PostAdd()
        {
            base.PostAdd();
            if (pawn.ageTracker.AgeBiologicalYears >= RESIZING_AGE)
            {
                Resize();
                WasApplied = true;
            }
        }

        /// <summary>
        /// Used to resize the pawns genitalia. 
        /// All Logic should be put here: 
        /// 1. Filters for Gender
        /// 2. Filters for Genitalia Existance
        /// 3. Selection of right Genitalia 
        /// 4. Adjustment of Size
        /// 
        /// I kept it intentionally broad, so that e.g. the Penis Resize can resize multiple penises and also for futas, 
        /// while the breast-gene is female only. 
        /// </summary>
        public abstract void Resize();

    }
}