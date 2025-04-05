using UnityEngine;

namespace WinterUniverse
{
    public abstract class EffectConfig : BasicInfoConfig
    {
        public abstract Effect GetEffect(PawnController target, PawnController source, float value, float duration);
    }
}