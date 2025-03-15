namespace WinterUniverse
{
    public class ReduceHealthOverTimeEffect : Effect
    {
        private DamageTypeConfig _damageType;

        public ReduceHealthOverTimeEffect(EffectConfig config, PawnController owner, PawnController source, float value, float duration, DamageTypeConfig damageType) : base(config, owner, source, value, duration)
        {
            _damageType = damageType;
        }

        public override void OnApply()
        {
            _owner.Status.ReduceHealthCurrent(_value, _damageType, _source);
        }

        protected override void ApplyOnTick(float deltaTime)
        {
            _owner.Status.ReduceHealthCurrent(_value * deltaTime, _damageType, _source);
        }
    }
}