using rjw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
    public static class DiseaseHelper
    {

        /// <summary>
        /// Checks for a pawn if it is immune against a disease.
        /// </summary>
        /// <param name="pawn">The pawn for which immunity is checked</param>
        /// <param name="disease">The genetic disease that is checked against</param>
        /// <returns>True if the pawn is immune, false if the pawn can be infected by it.</returns>
        public static bool IsImmuneAgainstGeneticDisease(Pawn pawn, GeneDef disease)
        {
            // Case 1: Something is null / not working, return Immune (to have less follow up effects)
            if (pawn == null || pawn.genes == null) return true;
            if (disease == null) return true;
            // Case 1.B: Dead people can spread, but not receive, diseases.
            if (pawn.Dead) return true;

            // Case 2: The pawn has general genetic immunity to diseases
            if (GeneUtility.HasGeneNullCheck(pawn, GeneDefOf.rjw_genes_genetic_disease_immunity))
                return true;

            // Case 3: The pawn already has the genetic disease
            if (GeneUtility.HasGeneNullCheck(pawn, disease))
                return true;

            // Case 4: Check all genes if one of them has the Immunity Extension that covers the GeneDef
            List<Gene> genes = pawn.genes.GenesListForReading;
            genes = genes.Where(x => pawn.genes.HasActiveGene(x.def)).ToList();

            foreach (Gene gene in genes)
            {
                ImmunityAgainstGenesExtension ext = gene.def.GetModExtension<ImmunityAgainstGenesExtension>();
                if (ext != null) { 
                    foreach (string defname in ext.givesImmunityAgainst)
                        if (disease.defName == defname)
                            return true;
                }
            }

            // Case 5: Nothing special happens, so return false (not immune)
            return false;
        }

        /// <summary>
        /// Returns all active Genes with the `GeneticDiseaseExtension`.
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns>List of all active Genes with the `GeneticDiseaseExtension` in pawn</returns>
        public static List<GeneDef> GetGeneticDiseaseGenes(Pawn pawn)
        {
            if (pawn != null && pawn.genes != null)
            {
                return pawn.genes
                    .GenesListForReading
                    .ConvertAll(gene => gene.def)
                    .Where(genedef => pawn.genes.HasActiveGene(genedef))
                    .Where(IsGeneticDiseaseGene)
                    .ToList();
            }

            return new List<GeneDef>() { };
        }

        /// <summary>
        /// Checks if the performed sex was penetrative. 
        /// Condom check is not done here!
        /// </summary>
        /// <param name="props">The sexprops </param>
        /// <returns></returns>
        public static bool IsPenetrativeSex(SexProps props)
        {
            if (props == null) return false;

            return props.sexType ==
                xxx.rjwSextype.Vaginal
                || props.sexType == xxx.rjwSextype.Anal
                || props.sexType == xxx.rjwSextype.Oral
                || props.sexType == xxx.rjwSextype.DoublePenetration
                || props.sexType == xxx.rjwSextype.Fellatio
                || props.sexType == xxx.rjwSextype.Sixtynine;
        }

        public static bool IsGeneticDiseaseGene(GeneDef geneDef)
        {
            if (geneDef == null) return false;
            GeneticDiseaseExtension diseaseExt = geneDef.GetModExtension<GeneticDiseaseExtension>();
            return diseaseExt != null;
        }

        public static float LookupDiseaseInfectionChance(GeneDef geneDef)
        {
            if (IsGeneticDiseaseGene(geneDef))
            {
                GeneticDiseaseExtension diseaseExt = geneDef.GetModExtension<GeneticDiseaseExtension>();
                return diseaseExt != null ? diseaseExt.infectionChance : 0.0f;
            }
            else
                return 0.0f;
        }
    }
}
