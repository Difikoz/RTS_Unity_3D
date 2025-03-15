using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Restore Health Over Time", menuName = "Winter Universe/Pawn/Effect/New Restore Health Over Time")]
    public class RestoreHealthOverTimeConfig : EffectConfig
    {
        private void OnValidate()
        {
            _effectType = EffectType.RestoreHealthOverTime;
        }

        public override Effect CreateEffect(PawnController target, PawnController source, float value, float duration)
        {
            return new RestoreHealthOverTime(this, target, source, value, duration);
        }
    }
}