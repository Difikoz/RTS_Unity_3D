using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class StateHolder
    {
        public Action OnStatesChanged;

        public Dictionary<string, bool> States { get; private set; }

        public StateHolder(List<StateKeyConfig> states)
        {
            States = new();
            foreach (StateKeyConfig state in states)
            {
                SetStateValue(state.Key, false);
            }
        }

        public void ApplyStates(List<StateCreator> states)
        {
            foreach (StateCreator creator in states)
            {
                SetStateValue(creator.Config.Key, creator.Value);
            }
        }

        public bool HasState(string key)
        {
            return States.ContainsKey(key);
        }

        public bool CompareStateValue(string key, bool value)
        {
            if (States.ContainsKey(key))
            {
                return States[key] == value;
            }
            Debug.Log($"not have {key} state to compare");
            return false;
        }

        public void SetStateValue(string key, bool value)
        {
            if (States.ContainsKey(key))
            {
                States[key] = value;
            }
            else
            {
                States.Add(key, value);
            }
            OnStatesChanged?.Invoke();
        }

        public void ToggleStateValue(string key)
        {
            if (States.ContainsKey(key))
            {
                States[key] = !States[key];
            }
            else
            {
                States.Add(key, true);
            }
            OnStatesChanged?.Invoke();
        }
    }
}