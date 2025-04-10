using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Stat Modifier", menuName = "Winter Universe/Pawn/Effect/New Stat Modifier")]
    public class StatModifierEffectConfig : EffectConfig
    {
        [SerializeField] private StatConfig _stat;
        [SerializeField] private StatModifierType _modifierType;

        public override Effect GetEffect(PawnController target, PawnController source, float value, float duration)
        {
            return new StatModifierEffect(this, target, source, value, duration, _stat, _modifierType);
        }
    }
}