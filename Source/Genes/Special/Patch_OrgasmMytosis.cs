using HarmonyLib;
using RimWorld;
using RimWorld.QuestGen;
using rjw;
using rjw.Modules.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RJW_Genes
{
	
	[HarmonyPatch(typeof(JobDriver_Sex), nameof(JobDriver_Sex.Roll_Orgasm_Duration_Reset))]
	public static class Patch_OrgasmMytosis
	{

		private const float SEVERITY_INCREASE_PER_ORGASM = 0.075f;

		public static void Postfix(JobDriver_Sex __instance, ref int __result)
		{
			Pawn orgasmingPawn = __instance.pawn;
            if (orgasmingPawn != null && GeneUtility.HasGeneNullCheck(orgasmingPawn, GeneDefOf.rjw_genes_sexual_mytosis) && ! orgasmingPawn.health.hediffSet.HasHediff(HediffDefOf.rjw_genes_mytosis_shock_hediff))
			{
				var mytosisHediff = GetOrgasmMytosisHediff(orgasmingPawn);
				mytosisHediff.Severity += SEVERITY_INCREASE_PER_ORGASM;

				if (mytosisHediff.Severity >= 1.0)
                {
                    orgasmingPawn.health.RemoveHediff(mytosisHediff);

                    var copy = Multiply(orgasmingPawn);

                    ApplyMytosisShock(copy);
                    ApplyMytosisShock(orgasmingPawn);

                    orgasmingPawn.Strip();
                    
                }
                else
				{
                    float orgasm_time_reduction = Math.Max(1.0f - mytosisHediff.Severity, 0.1f);
                    __result = (int)(orgasm_time_reduction * __result);
                }

			}

		}

        private static void ApplyMytosisShock(Pawn copy)
        {
            var stunA = HediffMaker.MakeHediff(HediffDefOf.rjw_genes_mytosis_shock_hediff, copy);
            stunA.Severity = 1;
            copy.health.AddHediff(stunA);
        }

        /// <summary>
        /// Helps to get the Orgasm Mytosis Hediff of a Pawn. If it does not exist, one is added. 
        /// </summary>
        /// <param name="orgasmed">The pawn that had the orgasm, for which a hediff is looked up or created.</param>
        /// <returns></returns>
        public static Hediff GetOrgasmMytosisHediff(Pawn orgasmed)
		{
			Hediff orgasmicMytosisHediff = orgasmed.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.rjw_genes_orgasmic_mytosis_hediff);
			if (orgasmicMytosisHediff == null)
			{
				orgasmicMytosisHediff = HediffMaker.MakeHediff(HediffDefOf.rjw_genes_orgasmic_mytosis_hediff, orgasmed);
				orgasmicMytosisHediff.Severity = 0;
				orgasmed.health.AddHediff(orgasmicMytosisHediff);
			}
			return orgasmicMytosisHediff;
		}

		public static Pawn Multiply(Pawn toMultiply)
		{
			if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message("Hitting Multiply of Mytosis Pawn!");

            PawnGenerationRequest request = new PawnGenerationRequest(
                kind: toMultiply.kindDef,
                faction: toMultiply.Faction,
                forceGenerateNewPawn: true,
                developmentalStages: DevelopmentalStage.Adult,
                allowDowned: true,
                canGeneratePawnRelations: false,
                colonistRelationChanceFactor: 0,
                allowFood: false,
                allowAddictions: false,
                relationWithExtraPawnChanceFactor: 0,
                forbidAnyTitle: true,
                forceNoBackstory: true,
                fixedGender: toMultiply.gender
                );

            /*
             * Devnote: Adding these will lead to deadly issues! 
				fixedBiologicalAge: toMultiply.ageTracker.AgeBiologicalTicks,
				fixedChronologicalAge: toMultiply.ageTracker.AgeChronologicalTicks,
            */

            Pawn copy = PawnGenerator.GeneratePawn(request);
            

            copy.gender = toMultiply.gender;
            copy.ageTracker = toMultiply.ageTracker;
            copy.Name = CreateCloneName(toMultiply,2);

            copy.health = CopyRelevantHediffs(copy, toMultiply);
            copy.genes = CopyGeneTracker(copy,toMultiply.genes);

            copy.ideo = toMultiply.ideo;
            copy.records = new Pawn_RecordsTracker(copy);
            copy.outfits = toMultiply.outfits;

            copy.relations = toMultiply.relations;
            copy.skills = CopySkillTracker(copy,toMultiply.skills);

            copy.equipment.DestroyAllEquipment();
            copy.apparel.DestroyAll();

            //TODO: Make a letter on birth!


            PawnUtility.TrySpawnHatchedOrBornPawn(copy, toMultiply);
            // Move the copy in front of the origin, rather than on top
            if (toMultiply.Spawned)
                if (toMultiply.CurrentBed() != null)
                {
                    copy.Position = copy.Position + new IntVec3(0, 0, 1).RotatedBy(toMultiply.CurrentBed().Rotation);
                }


            // Birthmother doesn't show as relation (See log below)
            // copy.relations.AddDirectRelation(PawnRelationDefOf.ParentBirth, toMultiply);


            copy.style = CopyStyleTracker(copy, toMultiply.style);
            copy.story = CopyStoryTracker(copy, toMultiply.story);

            copy.Draw();
            return copy;
		}

        private static Name CreateCloneName(Pawn toCopyFrom, int additions=1)
        {
            if (toCopyFrom.Name is NameTriple)
            {
                NameTriple casted = (NameTriple)toCopyFrom.Name;
                String Postfix = " " + RandomNamePostFix(additions);
                Name newName = new NameTriple(first:casted.First + Postfix, nick: casted.Nick + Postfix, last: casted.Last);
                if (newName.UsedThisGame)
                    return CreateCloneName(toCopyFrom, additions);
                return newName;
            }
            return toCopyFrom.Name;
        }

        private static Pawn_GeneTracker CopyGeneTracker(Pawn toCopyTo, Pawn_GeneTracker toCopyFrom)
        {
            var tracker = new Pawn_GeneTracker(toCopyTo);
            
            // Due to Overwrite logics, we first add Endogenes and then a second pass on xenogenes

            // Pass 1: Endogenes
            foreach (Gene gene in toCopyFrom.GenesListForReading) {
                GeneDef def = gene.def;
                if (!toCopyFrom.Xenogenes.Contains(gene))
                    tracker.AddGene(def, false);
            }

            // Pass 2: Xenogenes
            foreach (Gene gene in toCopyFrom.GenesListForReading)
            {
                GeneDef def = gene.def;
                if (toCopyFrom.Xenogenes.Contains(gene))
                    tracker.AddGene(def, true);
            }

            tracker.Reset();
            var skin = tracker.GetMelaninGene();
            var hair = tracker.GetHairColorGene();

            //ModLog.Message($"{toCopyTo} had Skin {skin.defName} and {hair.defName} as colour-genes");


            return tracker;
        }

        private static Pawn_StoryTracker CopyStoryTracker(Pawn toCopyTo, Pawn_StoryTracker toCopyFrom)
        {
            var tracker = new Pawn_StoryTracker(toCopyTo);

            tracker.Childhood = toCopyFrom.Childhood;
            tracker.Adulthood = toCopyFrom.Adulthood;

            tracker.headType = toCopyFrom.headType;
            tracker.bodyType = toCopyFrom.bodyType;
            tracker.hairDef = toCopyFrom.hairDef;
            tracker.furDef = toCopyFrom.furDef;

            tracker.traits = toCopyFrom.traits;

            tracker.skinColorOverride = toCopyFrom.skinColorOverride;
            tracker.HairColor = toCopyFrom.HairColor;


            return tracker;
        }

        private static Pawn_SkillTracker CopySkillTracker(Pawn toCopyTo, Pawn_SkillTracker toCopyFrom)
        {
            var tracker = new Pawn_SkillTracker(toCopyTo);

            tracker.skills = toCopyFrom.skills;

            return tracker;
        }

        private static Pawn_HealthTracker CopyRelevantHediffs(Pawn toCopyTo, Pawn copiedFrom)
        {
            var toCopyFrom = copiedFrom.health;
            var tracker = toCopyTo.health;
            // Step 0: Remove everything, Reset 
            tracker.RemoveAllHediffs();
            tracker.Reset();
            // Step 1: Copy ALL Hediffs 
            foreach (Hediff hed in toCopyFrom.hediffSet.hediffs)
            {
                // DevNote: There were a lot of issues around bodyparts: 
                // Some Hediffs really need to know their  bodypart, e.g. an implanted arm can either be left or right. 
                // Ignoring this will lead to many errors, mostly around nullpointers.

                BodyPartRecord originalBPR = hed.Part;
                if (originalBPR != null) { 
                    BodyPartRecord copyBPR = toCopyTo.RaceProps?.body.AllParts.Find(bpr => bpr.def == originalBPR.def);
                    if (copyBPR != null && !copyBPR.IsMissingForPawn(toCopyTo)) { 
                        Hediff copiedHediff = HediffMaker.MakeHediff(hed.def, toCopyTo, copyBPR);
                        tracker.AddHediff(copiedHediff);
                    }
                } else
                {
                    Hediff copiedHediff = HediffMaker.MakeHediff(hed.def, toCopyTo);
                    tracker.AddHediff(copiedHediff);
                }
            }
            // Step 2: Remove all Artifical Parts
            List<Hediff> hediffsToRemove = new List<Hediff>();
            foreach (Hediff hed in tracker.hediffSet.hediffs)
            {
                if (hed is Hediff_AddedPart && ((Hediff_AddedPart)hed).def.countsAsAddedPartOrImplant)
                {
                    hediffsToRemove.Add(hed);
                }
            }
            tracker.hediffSet.hediffs.RemoveAll(x => hediffsToRemove.Contains(x));

            // Step 3: Tend issues from Removal
            foreach (Hediff copiedHediff in tracker.hediffSet.hediffs)
            {
                if (copiedHediff.Bleeding)
                    copiedHediff.Tended(1.0f,1.0f);
            }
            
            return tracker;
        }

        private static Pawn_StyleTracker CopyStyleTracker(Pawn toCopyTo, Pawn_StyleTracker toCopyFrom)
        {
            var tracker = new Pawn_StyleTracker(toCopyTo);

            tracker.beardDef = toCopyFrom.beardDef;
            tracker.BodyTattoo = toCopyFrom.BodyTattoo;
            tracker.FaceTattoo = toCopyFrom.FaceTattoo;

            return tracker; 
        }

        private static String RandomNamePostFix(int numberOfParts)
        {
            List<String> additions = new List<String>()
            {
                "A","B","C","D","E","F","X","Y","Z",
                "Two",
                "Alpha","Beta","Gamma","Delta","Epsilon","Zeta","Eta","Theta","Iota","Kappa","Lambda","Mu","Nu","Xi","Omicron","Pi","Rho","Sigma","Tau","Upsilon","Phi","Chi","Psi","Omega"
            };

            additions.Shuffle();
            return String.Join(" ",additions.Take(numberOfParts));
        }
    }


}

/*
*
*Warning:
*Tried to add pawn relation ParentBirth with self, pawn=Henri
UnityEngine.StackTraceUtility:ExtractStackTrace ()
Verse.Log:Warning (string)
RimWorld.Pawn_RelationsTracker:AddDirectRelation (RimWorld.PawnRelationDef,Verse.Pawn)
RJW_Genes.Patch_OrgasmMytosis:Multiply (Verse.Pawn)
RJW_Genes.Patch_OrgasmMytosis:Postfix (rjw.JobDriver_Sex,int&)
(wrapper dynamic-method) rjw.JobDriver_Sex:rjw.JobDriver_Sex.Roll_Orgasm_Duration_Reset_Patch1 (rjw.JobDriver_Sex)
(wrapper dynamic-method) rjw.JobDriver_Sex:rjw.JobDriver_Sex.Orgasm_Patch2 (rjw.JobDriver_Sex)
(wrapper dynamic-method) rjw.JobDriver_Sex:rjw.JobDriver_Sex.SexTick_Patch1 (rjw.JobDriver_Sex,Verse.Pawn,Verse.Thing,bool,bool)
rjw.JobDriver_Rape/<>c__DisplayClass1_0:<MakeNewToils>b__6 ()
(wrapper dynamic-method) Verse.AI.JobDriver:Verse.AI.JobDriver.DriverTick_Patch0 (Verse.AI.JobDriver)
Verse.AI.Pawn_JobTracker:JobTrackerTick ()
Verse.Pawn:Tick ()
Verse.TickList:Tick ()
(wrapper dynamic-method) Verse.TickManager:Verse.TickManager.DoSingleTick_Patch2 (Verse.TickManager)
Verse.TickManager:TickManagerUpdate ()
Verse.Game:UpdatePlay ()
Verse.Root_Play:Update ()

 */