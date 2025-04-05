using System.Linq;
using UnityEngine;

namespace WinterUniverse
{
    public class NPCController : OwnerController
    {
        private PawnController _pawn;
        //private ActionBase _currentAction;
        //private GoalHolder _currentGoal;
        //private List<ActionBase> _actions = new();
        //private Dictionary<GoalHolder, int> _goals = new();
        //private Queue<ActionBase> _actionQueue;

        //[SerializeField] private float _proccessingPlanDelay = 1f;

        //public ActionBase CurrentAction => _currentAction;
        //public GoalHolder CurrentGoal => _currentGoal;
        //public List<ActionBase> Actions => _actions;
        //public Dictionary<GoalHolder, int> Goals => _goals;

        //public void Initialize(PawnData pawnData, NPCData npcData)
        //{
        //    InitializePawn(pawnData);
        //    LoadData(npcData);
        //}

        //public void InitializePawn(PawnData data)
        //{
        //    _pawn = GameManager.StaticInstance.PrefabsManager.GetPawn(transform);
        //    _pawn.Create(data);
        //}

        //public void LoadData(NPCData data)
        //{
        //    ActionBase[] actions = GetComponentsInChildren<ActionBase>();
        //    foreach (ActionBase action in actions)
        //    {
        //        _actions.Add(action);
        //        action.Initialize();
        //    }
        //    foreach (GoalCreator creator in GameManager.StaticInstance.ConfigsManager.GetGoalHolder(data.GoalHolder).GoalsToAdd)
        //    {
        //        _goals.Add(new(creator.Config), creator.Priority);
        //    }
        //    var sortedGoals = from entry in _goals orderby entry.Value descending select entry;
        //    _goals = new(sortedGoals);
        //    StartCoroutine(ProccessPlan());
        //}

        public void OnTick(float deltaTime)
        {
            //_pawn.Input.MoveDirection = _agent.desiredVelocity.normalized;
            //if (_pawn.Combat.Target != null)
            //{
            //    _pawn.Input.LookDirection = _pawn.Combat.DirectionToTarget;
            //    _pawn.Input.LookPoint = _pawn.Combat.Target.Animator.BodyPoint.position;
            //}
            //else
            //{
            //    _pawn.Input.LookDirection = _agent.desiredVelocity.normalized;
            //    _pawn.Input.LookPoint = _pawn.Animator.BodyPoint.position + _pawn.transform.forward;
            //}
        }

        //private IEnumerator ProccessPlan()
        //{
        //    WaitForSeconds delay = new(_proccessingPlanDelay);
        //    while (true)
        //    {
        //        while (_currentAction != null)
        //        {
        //            if (_currentAction.CanAbort())
        //            {
        //                _currentAction.OnAbort();
        //                ResetPlan();
        //            }
        //            else if (_currentAction.CanComplete())
        //            {
        //                _currentAction.OnComplete();
        //                _currentAction = null;
        //            }
        //            else
        //            {
        //                _currentAction.OnUpdate(_proccessingPlanDelay);
        //            }
        //            yield return delay;
        //        }
        //        if (_actionQueue != null)
        //        {
        //            if (_actionQueue.Count > 0)
        //            {
        //                _currentAction = _actionQueue.Dequeue();
        //                if (_currentAction.CanStart())
        //                {
        //                    _currentAction.OnStart();
        //                }
        //                else
        //                {
        //                    ResetPlan();
        //                }
        //            }
        //            else if (_currentGoal != null)
        //            {
        //                if (!_currentGoal.Config.Repeatable)
        //                {
        //                    _goals.Remove(_currentGoal);
        //                }
        //                ResetPlan();
        //            }
        //            yield return delay;
        //        }
        //        while (_actionQueue == null)
        //        {
        //            foreach (KeyValuePair<GoalHolder, int> goal in _goals)
        //            {
        //                _actionQueue = TaskManager.GetPlan(_actions, _pawn.StateHolder, goal.Key.RequiredStates);
        //                if (_actionQueue != null)
        //                {
        //                    _currentGoal = goal.Key;
        //                    string planText = $"Plan for [{_currentGoal.Config.DisplayName}] is:";
        //                    foreach (ActionBase a in _actionQueue)
        //                    {
        //                        planText += $" {a.Config.DisplayName}, ";
        //                    }
        //                    planText += "END";
        //                    _currentAction = _actionQueue.Dequeue();
        //                    if (_currentAction.CanStart())
        //                    {
        //                        Debug.Log(planText);
        //                        _currentAction.OnStart();
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        ResetPlan();
        //                    }
        //                }
        //                else
        //                {
        //                    Debug.Log($"No plan for {goal.Key.Config.DisplayName}");
        //                    ResetPlan();
        //                }
        //                yield return delay;
        //            }
        //        }
        //        yield return null;
        //    }
        //}

        //private void ResetPlan()
        //{
        //    _currentAction = null;
        //    _currentGoal = null;
        //    _actionQueue = null;
        //}
    }
}