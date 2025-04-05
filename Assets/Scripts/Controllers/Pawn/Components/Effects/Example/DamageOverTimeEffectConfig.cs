using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Damage Over Time", menuName = "Winter Universe/Pawn/Effect/New Damage Over Time")]
    public class DamageOverTimeEffectConfig : EffectConfig
    {
        [SerializeField] private DamageTypeConfig _damageType;

        public override Effect GetEffect(PawnController target, PawnController source, float value, float duration)
        {
            return new DamageOverTimeEffect(this, target, source, value, duration, _damageType);
        }
    }
}