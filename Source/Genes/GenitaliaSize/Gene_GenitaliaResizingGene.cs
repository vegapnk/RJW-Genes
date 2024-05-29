using Verse;

namespace RJW_Genes
{
    /// <summary>
    /// Parent Gene for Genitalia Resizing. All Resizing genes should inherit for this class. 
    /// There is a companion-patch `Patch_ResizingOnAdulthood`.
    /// 
    /// This helps with some functions (e.g. "hasGenitaliaResizingGenes(pawn)") but also to fire genitalia resizing later in life for Pawns. 
    /// (No Children with huge ding dongs, and I don't want kids with tight anuses I am not that degenerate)
    /// 
    /// 
    /// There was an Issue (#34) that re-sized the genitalia over multiple birthdays. 
    /// Before the addition of `ExposeData`, it lost track whether the resizing was already run, 
    /// leading to a change with every birthday over multiple game starts. 
    /// </summary>
    public abstract class Gene_GenitaliaResizingGene : RJW_Gene
    {

    

        /// <summary>
        /// Whether or not the gene was already applied. 
        /// If not, it is checked on every birthday and will be applied accordingly.
        /// </summary>
        private bool resizingWasApplied = false;
        public bool ResizingWasApplied { get => resizingWasApplied; set => resizingWasApplied = value; }


        public override void PostMake()
        {
            base.PostMake();
            if (pawn.ageTracker.AgeBiologicalYears >= RJW_Genes_Settings.rjw_genes_resizing_age)
            {
                Resize();
                ResizingWasApplied = true;
            }
        }

        public override void PostAdd()
        {
            base.PostAdd();
            if (pawn.ageTracker.AgeBiologicalYears >= RJW_Genes_Settings.rjw_genes_resizing_age)
            {
                Resize();
                ResizingWasApplied = true;
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref resizingWasApplied, "wasApplied");
        }

        /// <summary>
        /// Used to resize the pawns genitalia. 
        /// All Logic should be put here: 
        /// 1. Filters for Gender
        /// 2. Filters for Genitalia Existence
        /// 3. Selection of right Genitalia 
        /// 4. Adjustment of Size
        /// 
        /// I kept it intentionally broad, so that e.g. the Penis Resize can resize multiple penises and or futas, 
        /// while the breast-gene is female only. 
        /// </summary>
        public abstract void Resize();

    }
}