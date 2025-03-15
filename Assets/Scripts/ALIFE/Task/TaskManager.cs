using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public static class TaskManager
    {
        public static Queue<ActionBase> GetPlan(List<ActionBase> allActions, Dictionary<string, bool> states, Dictionary<string, bool> conditions)
        {
            if (GoalAchieved(conditions, states))// already compared pawn and world states to goal conditions
            {
                return null;
            }
            List<ActionBase> usableActions = new();
            foreach (ActionBase action in allActions)
            {
                usableActions.Add(action);
            }
            List<TaskNode> leaves = new();
            TaskNode startNode = new(states);
            bool success = BuildGraph(startNode, leaves, usableActions, conditions);
            if (!success)
            {
                return null;
            }
            TaskNode cheapestNode = null;
            foreach (TaskNode leaf in leaves)
            {
                if (cheapestNode == null)
                {
                    cheapestNode = leaf;
                }
                else if (leaf.Cost < cheapestNode.Cost)
                {
                    cheapestNode = leaf;
                }
            }
            List<ActionBase> result = new();
            TaskNode n = cheapestNode;
            while (n != null)
            {
                if (n.Action != null)
                {
                    result.Insert(0, n.Action);
                }
                n = n.Parent;
            }
            Queue<ActionBase> queue = new();
            foreach (ActionBase a in result)
            {
                queue.Enqueue(a);
            }
            return queue;
        }

        private static bool BuildGraph(TaskNode parent, List<TaskNode> leaves, List<ActionBase> usableActions, Dictionary<string, bool> goalConditions)
        {
            bool foundPath = false;
            foreach (ActionBase action in usableActions)
            {
                if (action.IsAchievable(parent.States))
                {
                    Dictionary<string, bool> currentStates = new();
                    foreach (KeyValuePair<string, bool> state in parent.States)
                    {
                        currentStates.Add(state.Key, state.Value);
                    }
                    foreach (KeyValuePair<string, bool> effect in action.Effects)
                    {
                        if (currentStates.ContainsKey(effect.Key))
                        {
                            currentStates[effect.Key] = effect.Value;
                        }
                        else
                        {
                            currentStates.Add(effect.Key, effect.Value);
                        }
                    }
                    TaskNode node = new(parent, parent.Cost + action.Config.Cost, currentStates, action);
                    if (GoalAchieved(goalConditions, currentStates))
                    {
                        leaves.Add(node);
                        foundPath = true;
                    }
                    else
                    {
                        List<ActionBase> subset = ActionSubset(usableActions, action);
                        bool found = BuildGraph(node, leaves, subset, goalConditions);
                        if (found)
                        {
                            foundPath = true;
                        }
                    }
                }
            }
            return foundPath;
        }

        private static List<ActionBase> ActionSubset(List<ActionBase> usableActions, ActionBase currentAction)
        {
            List<ActionBase> subset = new();
            foreach (ActionBase action in usableActions)
            {
                if (action.gameObject != currentAction.gameObject)// rework?
                {
                    subset.Add(action);
                }
            }
            return subset;
        }

        private static bool GoalAchieved(Dictionary<string, bool> goalConditions, Dictionary<string, bool> states)
        {
            foreach (KeyValuePair<string, bool> condition in goalConditions)
            {
                if (!states.ContainsKey(condition.Key))
                {
                    return false;
                }
                if (states[condition.Key] != condition.Value)
                {
                    return false;
                }
            }
            return true;
        }
    }
}