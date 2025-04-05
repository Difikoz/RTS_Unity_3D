using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class EffectCreator
    {
        [SerializeField, Range(0f, 1f)] private float _chance = 0.5f;
        [SerializeField] private EffectConfig _config;
        [SerializeField, Range(-999999f, 999999f)] private float _value;
        [SerializeField, Range(0f, 999999f)] private float _duration;

        public float Chance => _chance;
        public EffectConfig Config => _config;
        public float Value => _value;
        public float Duration => _duration;

        public bool Triggered => _chance >= Random.value;
    }
}