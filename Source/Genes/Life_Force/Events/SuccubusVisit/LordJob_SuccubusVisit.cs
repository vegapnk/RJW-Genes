using System;
using System.Collections.Generic;
using Verse;
using Verse.AI.Group;
using RimWorld;
using rjw;
namespace RJW_Genes
{
    //Based on LordJob_VisitColony
    public class LordJob_SuccubusVisit : LordJob
    {
        public LordJob_SuccubusVisit() { }
        public LordJob_SuccubusVisit(Pawn target)
        {
            this.target = target;
        }

        //
        //Stategraph has lordtoils which say what a pawn should be doing
        //Transitions say when active lordtoil for pawn should change
        //
        public override StateGraph CreateGraph()
        {
            StateGraph stateGraph = new StateGraph();

            //Flirt
            LordToil_Flirt lordToil_Flirt = new LordToil_Flirt(this.target, 7f);
            stateGraph.AddToil(lordToil_Flirt);
            stateGraph.StartingToil = lordToil_Flirt;

            //Leave
            LordToil_ExitMapRandom lordToil_ExitMapRandom = new LordToil_ExitMapRandom();
            stateGraph.AddToil(lordToil_ExitMapRandom);
            LordToil_ExitMapAndDefendSelf lordToil_ExitMapAndDefendSelf = new LordToil_ExitMapAndDefendSelf();
            stateGraph.AddToil(lordToil_ExitMapAndDefendSelf);

            //Leave after some time
            Transition transition1 = new Transition(lordToil_Flirt, lordToil_ExitMapRandom, false, true);
            int tickLimit;
            if (this.durationTicks != null)
            {
                tickLimit = this.durationTicks.Value;
            }
            else
            {
                tickLimit = Rand.Range(60000, 180000); //~1-3 days
            }
            transition1.AddTrigger(new Trigger_TicksPassed(tickLimit));
            transition1.AddPreAction(new TransitionAction_Custom(new Action(this.SuccubiLeave))); //Join or leave colony
            stateGraph.AddTransition(transition1);

            //If they become hostile
            Transition transition3 = new Transition(lordToil_Flirt, lordToil_ExitMapAndDefendSelf, false, true);
            transition3.AddSource(lordToil_ExitMapRandom); //Not sure what this does 
            transition3.AddTrigger(new Trigger_BecamePlayerEnemy());
            transition3.AddTrigger(new Trigger_PawnKilled());
            transition3.AddPostAction(new TransitionAction_EndAllJobs());
            stateGraph.AddTransition(transition3, false);

            Transition transition4 = new Transition(lordToil_ExitMapRandom, lordToil_ExitMapAndDefendSelf, false, true);
            transition4.AddSource(lordToil_Flirt); //Not sure what this does 
            transition4.AddTrigger(new Trigger_PawnHarmed(1f, true, Faction.OfPlayer));
            stateGraph.AddTransition(transition4, false);

            return stateGraph;
        }

        //add toggleable gizmo to allow playes to have colonists sex the succubus into joining your colony
        //comfort pawn? cooldown?
        public override IEnumerable<Gizmo> GetPawnGizmos(Pawn p)
        {
            return base.GetPawnGizmos(p);
        }

        public override void ExposeData()
        {
            Scribe_Values.Look<int?>(ref this.durationTicks, "durationTicks", null, false);
            Scribe_References.Look<Pawn>(ref this.target, "target", false);
        }

        public void SuccubiLeave()
        {
            foreach (Pawn pawn in this.lord.ownedPawns)
            {
                if (colonyJoiners.Contains(pawn))
                {
                    RecruitUtility.Recruit(pawn, Faction.OfPlayer);
                    Find.LetterStack.ReceiveLetter("rjw_genes_succubus_joins_letter_label".Translate(), string.Format("rjw_genes_succubus_joins_letter_description".Translate(), xxx.get_pawnname(pawn)), RimWorld.LetterDefOf.PositiveEvent, pawn, null, null, null, null);
                }
                else
                {
                    Messages.Message("SuccubusLeaving".Translate(xxx.get_pawnname(pawn)), pawn, MessageTypeDefOf.NeutralEvent, true);
                }
            }

        }

        public Pawn target;
        private int? durationTicks;
        public List<Pawn> colonyJoiners = new List<Pawn>();
    }
}