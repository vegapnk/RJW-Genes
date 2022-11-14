using RimWorld;
using Verse;

namespace RJW_Genes
{
	[DefOf]
	public static class GeneDefOf
	{
		[MayRequireBiotech] public static readonly GeneCategoryDef rjw_genes_genitalia;

		// Base Genitalia Types
		[MayRequireBiotech] public static readonly GeneDef rjw_genes_human_genitalia;
		[MayRequireBiotech] public static readonly GeneDef rjw_genes_equine_genitalia;
		[MayRequireBiotech] public static readonly GeneDef rjw_genes_demonic_genitalia;
		[MayRequireBiotech] public static readonly GeneDef rjw_genes_dragon_genitalia;
		[MayRequireBiotech] public static readonly GeneDef rjw_genes_slime_genitalia;
		[MayRequireBiotech] public static readonly GeneDef rjw_genes_ovipositor_genitalia;
		[MayRequireBiotech] public static readonly GeneDef rjw_genes_feline_genitalia;
		[MayRequireBiotech] public static readonly GeneDef rjw_genes_canine_genitalia;

		// Extra Genitalia 
		[MayRequireBiotech] public static readonly GeneDef rjw_genes_extra_penis;
		[MayRequireBiotech] public static readonly GeneDef rjw_genes_no_penis;
	}
}
