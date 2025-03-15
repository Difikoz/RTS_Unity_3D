using System.Collections.Generic;

namespace WinterUniverse
{
    public class GoalHolder
    {
        private GoalConfig _config;
        private Dictionary<string, bool> _requiredStates;

        public GoalConfig Config => _config;
        public Dictionary<string, bool> RequiredStates => _requiredStates;

        public GoalHolder(GoalConfig config)
        {
            _config = config;
            _requiredStates = new();
            foreach (StateCreator creator in _config.RequiredStates)
            {
                _requiredStates.Add(creator.Config.Key, creator.Value);
            }
        }
    }
}