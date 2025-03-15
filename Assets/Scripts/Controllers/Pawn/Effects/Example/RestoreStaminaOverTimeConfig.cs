using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Restore Stamina Over Time", menuName = "Winter Universe/Pawn/Effect/New Restore Stamina Over Time")]
    public class RestoreStaminaOverTimeConfig : EffectConfig
    {
        private void OnValidate()
        {
            _effectType = EffectType.RestoreStaminaOverTime;
        }

        public override Effect CreateEffect(PawnController target, PawnController source, float value, float duration)
        {
            return new RestoreStaminaOverTime(this, target, source, value, duration);
        }
    }
}