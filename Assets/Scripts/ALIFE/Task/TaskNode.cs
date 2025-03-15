using System.Collections.Generic;

namespace WinterUniverse
{
    public class TaskNode
    {
        private TaskNode _parent;
        private float _cost;
        private Dictionary<string, bool> _states;
        private ActionBase _action;

        public TaskNode Parent => _parent;
        public float Cost => _cost;
        public Dictionary<string, bool> States => _states;
        public ActionBase Action => _action;

        public TaskNode(TaskNode newParent, float newCost, Dictionary<string, bool> states, ActionBase newAction)
        {
            _parent = newParent;
            _cost = newCost;
            _states = new(states);
            _action = newAction;
        }

        public TaskNode(Dictionary<string, bool> states)
        {
            _states = new(states);
        }
    }
}