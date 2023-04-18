using Verse;
using RimWorld;
using rjw;
using RimWorld.Planet;
using System;

namespace RJW_Genes
{
	/// <summary>
	/// Spawns tame spelopedes at the caster.
    /// 
    /// TODO: Play some sound? I think this can be done with some CompProperties. 
	/// </summary>
	public class CompAbilityEffect_SpawnSpelopede : CompAbilityEffect
	{
        public CompProperties_AbilitySpawnSpelopede Props => (CompProperties_AbilitySpawnSpelopede) this.props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);


            int spelopedesToSpawn = CalculateSpelopedeAmount();
            if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message($"Using Spelopede Spawn, spawning {spelopedesToSpawn}");

            for (int i = 0; i < spelopedesToSpawn; i++) { 
                var request =  new PawnGenerationRequest(
                    this.Props.pawnKindDef, 
                    faction: this.parent.pawn.Faction, 
                    forceGenerateNewPawn: true,
                    fixedBiologicalAge: 0.0f,
                    fixedChronologicalAge: 0.0f,
                    canGeneratePawnRelations: false,
                    colonistRelationChanceFactor: 0.0f
                    );


                Pawn insect = PawnGenerator.GeneratePawn(request);
                PawnUtility.TrySpawnHatchedOrBornPawn(insect, this.parent.pawn);

                if (Props.tamed)
                {
                    insect.training.Train(TrainableDefOf.Tameness, this.parent.pawn, true);
                    insect.training.Train(TrainableDefOf.Obedience, this.parent.pawn, true);
                    insect.training.Train(TrainableDefOf.Release, this.parent.pawn, true);

                    // I could do bonding here, but I think it's nicer if the queen is not "bonded" to their offspring. 
                }
            }

            MakeDirt(1);

            if (RJW_Genes_Settings.rjw_genes_detailed_debug) ModLog.Message("Finished Spelopede Spawn");

        }

        private int CalculateSpelopedeAmount()
        {
            Pawn caster = this.parent.pawn;
            float spelopedes = Props.sensitivityMultiplier * caster.psychicEntropy.PsychicSensitivity;
            return (spelopedes > 1.49f) ? (int)Math.Round(spelopedes) : 1;
        }

        private void MakeDirt(int multiplier = 1)
        {
            Pawn caster = this.parent.pawn;

            FilthMaker.TryMakeFilth(caster.Position, caster.Map, ThingDefOf.Filth_AmnioticFluid, caster.LabelIndefinite(), count: Rand.Range(5,5*multiplier));
        }

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            if (!target.Cell.Filled(this.parent.pawn.Map) && (target.Cell.GetEdifice(this.parent.pawn.Map) == null))
                return true;
            if (throwMessages)
                Messages.Message((string)("CannotUseAbility".Translate((NamedArgument)this.parent.def.label) + ": " + "AbilityOccupiedCells".Translate()), (LookTargets)target.ToTargetInfo(this.parent.pawn.Map), MessageTypeDefOf.RejectInput, false);
            return false;
        }
    }
}
