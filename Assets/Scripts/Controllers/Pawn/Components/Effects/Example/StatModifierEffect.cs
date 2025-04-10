namespace WinterUniverse
{
    public class StatModifierEffect : Effect
    {
        private StatModifierCreator _modifier;
        private StatConfig _stat;
        private StatModifierType _modifierType;

        public StatModifierEffect(EffectConfig config, PawnController owner, PawnController source, float value, float duration, StatConfig stat, StatModifierType modifierType) : base(config, owner, source, value, duration)
        {
            _stat = stat;
            _modifierType = modifierType;
        }

        public override void OnApply()
        {
            _modifier = new(_stat, new(_modifierType, Value));
            Owner.Status.StatsHolder.AddStatModifier(_modifier);
        }

        public override void OnRemove()
        {
            Owner.Status.StatsHolder.RemoveStatModifier(_modifier);
        }
    }
}