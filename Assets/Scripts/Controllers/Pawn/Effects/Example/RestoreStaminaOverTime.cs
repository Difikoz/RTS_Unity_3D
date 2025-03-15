namespace WinterUniverse
{
    public class RestoreStaminaOverTime : Effect
    {
        public RestoreStaminaOverTime(EffectConfig config, PawnController owner, PawnController source, float value, float duration) : base(config, owner, source, value, duration)
        {
        }

        public override void OnApply()
        {
            _owner.Status.RestoreStaminaCurrent(_value);
        }

        protected override void ApplyOnTick(float deltaTime)
        {
            _owner.Status.RestoreStaminaCurrent(_value * deltaTime);
        }
    }
}