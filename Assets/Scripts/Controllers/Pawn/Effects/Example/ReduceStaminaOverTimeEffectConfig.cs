using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Reduce Stamina Over Time", menuName = "Winter Universe/Pawn/Effect/New Reduce Stamina Over Time")]
    public class ReduceStaminaOverTimeEffectConfig : EffectConfig
    {
        private void OnValidate()
        {
            _effectType = EffectType.ReduceStaminaOverTime;
        }

        public override Effect CreateEffect(PawnController target, PawnController source, float value, float duration)
        {
            return new ReduceStaminaOverTimeEffect(this, target, source, value, duration);
        }
    }
}