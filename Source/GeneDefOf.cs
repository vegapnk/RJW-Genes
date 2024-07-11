using RimWorld;
using Verse;
using Verse.AI;
namespace RJW_Genes
{
	[DefOf]
	public static class GeneDefOf
	{
		public static readonly GeneCategoryDef rjw_genes_genitalia_type;
		public static readonly GeneCategoryDef rjw_genes_genitalia_size;
		public static readonly GeneCategoryDef rjw_genes_gender;
		public static readonly GeneCategoryDef rjw_genes_breeding;
		public static readonly GeneCategoryDef rjw_genes_damage;
		public static readonly GeneCategoryDef rjw_genes_special;

		// Base Genitalia Types
		public static readonly GeneDef rjw_genes_equine_genitalia;
		public static readonly GeneDef rjw_genes_demonic_genitalia;
		public static readonly GeneDef rjw_genes_dragon_genitalia;
		public static readonly GeneDef rjw_genes_slime_genitalia;
		public static readonly GeneDef rjw_genes_ovipositor_genitalia;
		public static readonly GeneDef rjw_genes_feline_genitalia;
		public static readonly GeneDef rjw_genes_canine_genitalia;
        public static readonly GeneDef rjw_genes_demonicT_genitalia;
        public static readonly GeneDef rjw_genes_crocodilian_genitalia;
        public static readonly GeneDef rjw_genes_racoon_genitalia;
        public static readonly GeneDef rjw_genes_reptilian_genitalia;
        public static readonly GeneDef rjw_genes_Ghost_genitalia;
        public static readonly GeneDef rjw_genes_Golem_genitalia;
        public static readonly GeneDef rjw_genes_Marine_genitalia;
        public static readonly GeneDef rjw_genes_Necro_genitalia;
        public static readonly GeneDef rjw_genes_Needle_genitalia;
        public static readonly GeneDef rjw_genes_Orc_genitalia;
        public static readonly GeneDef rjw_genes_Pig_genitalia;
        public static readonly GeneDef rjw_genes_Tentacle_genitalia;
        public static readonly GeneDef rjw_genes_human_genitalia;

        // Extra Genitalia 
        public static readonly GeneDef rjw_genes_extra_penis;
		public static readonly GeneDef rjw_genes_no_penis;
		public static readonly GeneDef rjw_genes_extra_vagina;
		public static readonly GeneDef rjw_genes_no_vagina;
		public static readonly GeneDef rjw_genes_extra_breasts;
		public static readonly GeneDef rjw_genes_no_breasts;
		public static readonly GeneDef rjw_genes_extra_anus;
		public static readonly GeneDef rjw_genes_no_anus;
		public static readonly GeneDef rjw_genes_futa;
        public static readonly GeneDef rjw_genes_femboy;
        public static readonly GeneDef rjw_genes_featureless_chest; 
		public static readonly GeneDef rjw_genes_udder;

        // Genitalia Sizes
        public static readonly GeneDef rjw_genes_big_male_genitalia;
		public static readonly GeneDef rjw_genes_small_male_genitalia;
		public static readonly GeneDef rjw_genes_loose_female_genitalia;
		public static readonly GeneDef rjw_genes_tight_female_genitalia;
		public static readonly GeneDef rjw_genes_big_breasts;
		public static readonly GeneDef rjw_genes_small_breasts;
		public static readonly GeneDef rjw_genes_loose_anus;
		public static readonly GeneDef rjw_genes_tight_anus;
		public static readonly GeneDef rjw_genes_evergrowth;

		// Gender 
		public static readonly GeneDef rjw_genes_female_only;
		public static readonly GeneDef rjw_genes_male_only;
		public static readonly GeneDef rjw_genes_gender_fluid;

		// Breeding
		public static readonly GeneDef rjw_genes_mechbreeder;
		public static readonly GeneDef rjw_genes_zoophile;
        public static readonly GeneDef rjw_genes_fertile_anus;
		public static readonly GeneDef rjw_genes_mating_call;
		public static readonly GeneDef rjw_genes_fervent_ovipositor;
		public static readonly GeneDef rjw_genes_insectbreeder;
		public static readonly GeneDef rjw_genes_insectincubator;
		public static readonly GeneDef rjw_genes_hardwired_progenity;
		public static readonly GeneDef rjw_genes_blocked_masturbation;

        // Cum 
        public static readonly GeneDef rjw_genes_no_cum;
		public static readonly GeneDef rjw_genes_much_cum;
		public static readonly GeneDef rjw_genes_very_much_cum;
		[MayRequire("LustLicentia.RJWLabs")] public static readonly GeneDef rjw_genes_likes_cumflation;
		[MayRequire("LustLicentia.RJWLabs")] public static readonly GeneDef rjw_genes_cumflation_immunity;
		[MayRequire("LustLicentia.RJWLabs")] public static readonly GeneDef rjw_genes_generous_donor;
        

        // Reproduction 
        public static readonly GeneDef rjw_genes_hypersexual;
		public static readonly GeneDef rjw_genes_rapist;
		public static readonly GeneDef rjw_genes_homosexual;
		public static readonly GeneDef rjw_genes_bisexual;
		public static readonly GeneDef rjw_genes_no_sex_need;
        public static readonly GeneDef LitteredBirths;

        // Damage & Side Effects 
        [MayRequire("LustLicentia.RJWLabs")] public static readonly GeneDef rjw_genes_elasticity;
		public static readonly GeneDef rjw_genes_unbreakable;

		// Special
		public static readonly GeneDef rjw_genes_orgasm_rush;
		public static readonly GeneDef rjw_genes_youth_fountain;
		public static readonly GeneDef rjw_genes_sex_age_drain;
		public static readonly GeneDef rjw_genes_aphrodisiac_pheromones;
		public static readonly GeneDef rjw_genes_sexual_mytosis;
		public static readonly GeneDef rjw_genes_hormonal_saliva;
		public static readonly GeneDef rjw_genes_cocoonweaver;
        public static readonly GeneDef rjw_genes_sex_tamer;
		public static readonly GeneDef rjw_genes_sexual_genetic_swap;
		public static readonly GeneDef rjw_genes_sexual_genetic_thief;
		public static readonly GeneDef rjw_genes_pregnancy_overwrite;
		public static readonly GeneDef rjw_genes_feminizer;
		public static readonly GeneDef rjw_genes_twinkifier;

        // Cosmetic
        public static readonly GeneDef rjw_genes_succubus_tail;
        public static readonly GeneDef rjw_genes_succubus_wings;

        // Life force | Fertilin
        public static readonly GeneDef rjw_genes_lifeforce;
        public static readonly GeneDef rjw_genes_pussyhealing;
        public static readonly GeneDef rjw_genes_lifeforce_drain;
        public static readonly GeneDef rjw_genes_cum_eater;
        public static readonly GeneDef rjw_genes_fertilin_absorber;
        public static readonly GeneDef rjw_genes_drainer;
        public static readonly GeneDef rjw_genes_seduce;
        public static readonly GeneDef rjw_genes_paralysingkiss;
        public static readonly GeneDef rjw_genes_cockeater;
        public static readonly GeneDef rjw_genes_lifeforce_empath;

		// Diseases
		public static readonly GeneDef rjw_genes_genetic_disease_immunity;
		public static readonly GeneDef rjw_genes_minor_vulnerability;
        public static readonly GeneDef rjw_genes_major_vulnerability;
		public static readonly GeneDef rjw_genes_fluctual_sexual_needs;
		public static readonly GeneDef rjw_genes_size_blinded;
		public static readonly GeneDef rjw_genes_infectious_low_fertility;
        public static readonly GeneDef rjw_genes_infectious_increased_sex_need;
        public static readonly GeneDef rjw_genes_infectious_bisexuality;
        public static readonly GeneDef rjw_genes_infectious_homosexuality;
		public static readonly GeneDef rjw_genes_infectious_hypersexuality;
        public static readonly GeneDef rjw_genes_stretcher;

        //Other Defs
        public static readonly XenotypeDef rjw_genes_succubus;
        public static readonly DutyDef rjw_genes_flirt;
        public static readonly MentalBreakDef rjw_genes_lifeforce_randomrape;
        [MayRequire("resplice.xotr.charmweavers")] public static GeneDef RS_LoveFeed = DefDatabase<GeneDef>.GetNamed("RS_LoveFeed", false);
        [MayRequire("resplice.xotr.charmweavers")] public static GeneDef RS_MultiPregnancy = DefDatabase<GeneDef>.GetNamed("RS_MultiPregnancy", false);
        [MayRequire("redmattis.bigsmall.core")] public static GeneDef VU_VampireLover = DefDatabase<GeneDef>.GetNamed("VU_VampireLover", false);
        [MayRequire("vanillaracesexpanded.highmate")] public static GeneDef VRE_LovinDependency = DefDatabase<GeneDef>.GetNamed("VRE_LovinDependency", false);



    }

}
