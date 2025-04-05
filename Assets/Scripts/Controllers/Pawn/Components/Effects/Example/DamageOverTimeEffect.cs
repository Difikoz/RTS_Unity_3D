namespace WinterUniverse
{
    public class DamageOverTimeEffect : Effect
    {
        private DamageTypeConfig _damageType;

        public DamageOverTimeEffect(EffectConfig config, PawnController owner, PawnController source, float value, float duration, DamageTypeConfig damageType) : base(config, owner, source, value, duration)
        {
            _damageType = damageType;
        }

        public override void OnApply()
        {
            Owner.Status.ReduceHealthCurrent(Value, _damageType, Source);
        }

        protected override void ApplyOnTick(float deltaTime)
        {
            Owner.Status.ReduceHealthCurrent(Value * deltaTime, _damageType, Source);
        }
    }
}