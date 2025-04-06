using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Lean.Pool.LeanGameObjectPool;

namespace WinterUniverse
{
    public class NPCController : OwnerController
    {
        private Queue<ActionBase> _actionQueue;

        [SerializeField] private float _proccessingPlanDelay = 1f;

        public ActionBase CurrentAction { get; private set; }
        public GoalHolder CurrentGoal { get; private set; }
        public List<ActionBase> Actions { get; private set; }
        public Dictionary<GoalHolder, int> Goals { get; private set; }

        public void LoadData(FactionConfig faction, Vector3 spawnPosition, Quaternion spawnRotation)
        {
            Pawn.Faction.ChangeConfig(faction);
            Pawn.transform.SetPositionAndRotation(spawnPosition, spawnRotation);
            Goals.Clear();
            foreach (GoalCreator creator in faction.Goals[Random.Range(0, faction.Goals.Count)].GoalsToAdd)// pick random goals
            {
                Goals.Add(new(creator.Config), creator.Priority);
            }
            var sortedGoals = from entry in Goals orderby entry.Value descending select entry;
            Goals = new(sortedGoals);
        }

        public override void InitializeComponent()
        {
            base.InitializeComponent();
            Actions = new();
            Goals = new();
            ActionBase[] actions = GetComponentsInChildren<ActionBase>();
            foreach (ActionBase action in actions)
            {
                Actions.Add(action);
                action.InitializeComponent();
            }
            LoadData(GameManager.StaticInstance.ConfigsManager.Factions[Random.Range(0, GameManager.StaticInstance.ConfigsManager.Factions.Count)], Vector3.zero, Quaternion.identity);// test
        }

        public override void LateUpdateComponent()
        {
            base.LateUpdateComponent();
            if (CurrentAction != null)
            {
                if (CurrentAction.CanAbort())
                {
                    CurrentAction.OnAbort();
                    ResetPlan();
                }
                else if (CurrentAction.CanComplete())
                {
                    CurrentAction.OnComplete();
                    CurrentAction = null;
                }
                else
                {
                    CurrentAction.UpdateComponent();
                }
                return;
            }
            if (_actionQueue != null)
            {
                if (_actionQueue.Count > 0)
                {
                    CurrentAction = _actionQueue.Dequeue();
                    if (CurrentAction.CanStart())
                    {
                        CurrentAction.OnStart();
                    }
                    else
                    {
                        ResetPlan();
                    }
                }
                else if (CurrentGoal != null)
                {
                    if (!CurrentGoal.Config.Repeatable)
                    {
                        Goals.Remove(CurrentGoal);
                    }
                    ResetPlan();
                }
                return;
            }
            else
            {
                foreach (KeyValuePair<GoalHolder, int> goal in Goals)
                {
                    _actionQueue = TaskManager.GetPlan(Actions, Pawn.Status.StateHolder.States, goal.Key.RequiredStates);
                    if (_actionQueue != null)
                    {
                        CurrentGoal = goal.Key;
                        string planText = $"Plan for [{CurrentGoal.Config.DisplayName}] is:";
                        foreach (ActionBase a in _actionQueue)
                        {
                            planText += $" {a.Config.ID}, ";
                        }
                        planText += "END";
                        CurrentAction = _actionQueue.Dequeue();
                        if (CurrentAction.CanStart())
                        {
                            Debug.Log(planText);
                            CurrentAction.OnStart();
                            break;
                        }
                        else
                        {
                            ResetPlan();
                        }
                    }
                }
            }
        }

        private void ResetPlan()
        {
            CurrentAction = null;
            CurrentGoal = null;
            _actionQueue = null;
        }

        public override void OnPawnChased(OwnerController owner)
        {
            // start battle
        }

        public override void OnSettlementEntered(Settlement settlement)
        {
            // heal // trade //
        }
    }
}