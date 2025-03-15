using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Goal Holder", menuName = "Winter Universe/ALIFE/New Goal Holder")]
    public class GoalHolderConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private List<GoalCreator> _goalsToAdd = new();

        public string ID => _id;
        public List<GoalCreator> GoalsToAdd => _goalsToAdd;
    }
}