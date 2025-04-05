using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class StateCreator
    {
        [SerializeField] private StateKeyConfig _config;
        [SerializeField] private bool _value;

        public StateKeyConfig Config => _config;
        public bool Value => _value;
    }
}