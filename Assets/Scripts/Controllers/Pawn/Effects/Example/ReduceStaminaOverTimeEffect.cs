namespace WinterUniverse
{
    public class ReduceStaminaOverTimeEffect : Effect
    {
        public ReduceStaminaOverTimeEffect(EffectConfig config, PawnController owner, PawnController source, float value, float duration) : base(config, owner, source, value, duration)
        {
        }

        public override void OnApply()
        {
            _owner.Status.ReduceStaminaCurrent(_value);
        }

        protected override void ApplyOnTick(float deltaTime)
        {
            _owner.Status.ReduceStaminaCurrent(_value);
        }
    }
}