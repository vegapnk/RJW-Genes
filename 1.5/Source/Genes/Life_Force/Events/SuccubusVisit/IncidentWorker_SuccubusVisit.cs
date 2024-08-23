using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using RimWorld;
using rjw;
namespace RJW_Genes
{
    public class IncidentWorker_SuccubusDreamVisit : IncidentWorker
    {
        //This incidint will only fire if there is a pawn asleep and sexneed is lower than 0.25
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }
            Map map = (Map)parms.target;
            if (!map.mapTemperature.SeasonAcceptableFor(ThingDefOf.Human))
            {
                return false;
            }
            if (!RJW_Genes_Settings.rjw_genes_sexdemon_visit_incubi && !RJW_Genes_Settings.rjw_genes_sexdemon_visit_succubi)
            {
                return false;
            }

            foreach (Pawn pawn in map.mapPawns.FreeColonistsAndPrisonersSpawned)
            {
                if (pawn.jobs.curDriver.asleep && xxx.need_some_sex(pawn) > 1f)
                {
                    return true;
                }
            }
            return false;

        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            List<Pawn> victims = ValidVictims(map).ToList();
            if (victims.NullOrEmpty())
            {
                return false;
            }
            Faction faction;
            if (!this.TryFindFormerFaction(out faction))
            {
                return false;
            }
            int pawn_amount = RJW_Genes_Settings.rjw_genes_sexdemon_visit_groups ? Rand.Range(1, victims.Count) : 1;
            List<Pawn> new_sexdemons = new List<Pawn>();
            for (int i = 0; i < pawn_amount; i++)
            {
                Pawn victim = victims.RandomElement();
                IntVec3 loc = victim.Position;

                PawnKindDef pawnKindDef;
                Gender gender;
                if (victim.gender == Gender.Male || !RJW_Genes_Settings.rjw_genes_sexdemon_visit_incubi)
                {

                }
                if ((Rand.Bool && RJW_Genes_Settings.rjw_genes_sexdemon_visit_succubi) || !RJW_Genes_Settings.rjw_genes_sexdemon_visit_incubi)
                {
                    pawnKindDef = PawnKindDef.Named("rjw_genes_succubus");
                    gender = Gender.Female;
                }
                else
                {
                    pawnKindDef = PawnKindDef.Named("rjw_genes_incubus");
                    gender = Gender.Male;
                }

                //Spawn succubus at pawn
                Pawn sexdemon = PawnGenerator.GeneratePawn(new PawnGenerationRequest(pawnKindDef, faction, PawnGenerationContext.NonPlayer, -1,
                    false, false, false, true, false, 1f, false, true, false, true, true, false, false, false, false, 0f, 0f, null, 1f, null, null,
                    null, null, null, null, null, gender, null, null, null, null, false, false, false, false, null, null, null, null, null, 0f,
                    DevelopmentalStage.Adult, null, null, null, false));
                sexdemon.SetFaction(null, null);
                GenSpawn.Spawn(sexdemon, loc, map, WipeMode.Vanish);
                List<Pawn> sexdemons = new List<Pawn> { sexdemon };
                new_sexdemons.Add(sexdemon);


                LordMaker.MakeNewLord(Faction.OfPlayer, this.CreateLordJob(parms, sexdemon, victim), map, sexdemons);

                //Make succubus rape victim.
                if (RJWSettings.rape_enabled)
                {
                    //follow rjw rules
                    if (SexAppraiser.would_fuck(sexdemon, victim) > 0f)
                    {
                        sexdemon.pather.StopDead();
                        sexdemon.jobs.StopAll();
                        Job newJob = JobMaker.MakeJob(xxx.RapeRandom, victim);
                        sexdemon.jobs.StartJob(newJob, JobCondition.InterruptForced, null, false, true, null, null, false, false, null, false, true);
                    }

                }
            }
            Find.LetterStack.ReceiveLetter("rjw_genes_sexdemon_visit_incident_label".Translate(), "rjw_genes_sexdemon_visit_incident_description".Translate(), RimWorld.LetterDefOf.PositiveEvent, new_sexdemons, null, null, null, null);

            return true;
        }

        private IEnumerable<Pawn> ValidVictims(Map map)
        {
            foreach (Pawn pawn in map.mapPawns.FreeColonistsAndPrisonersSpawned)
            {
                if (pawn.jobs.curDriver.asleep && xxx.need_some_sex(pawn) > 1f)
                {
                    yield return pawn;
                }
            }
            yield break;
        }

        private bool TryFindFormerFaction(out Faction formerFaction)
        {
            return Find.FactionManager.TryGetRandomNonColonyHumanlikeFaction(out formerFaction, false, true, TechLevel.Undefined, false);
        }

        protected virtual LordJob_SuccubusVisit CreateLordJob(IncidentParms parms, Pawn succubus, Pawn target)
        {
            return new LordJob_SuccubusVisit(target);
        }
    }
}