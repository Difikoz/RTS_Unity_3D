namespace WinterUniverse
{
    public abstract class Effect
    {
        public EffectConfig Config { get; private set; }
        public PawnController Owner { get; private set; }
        public PawnController Source { get; private set; }
        public float Value { get; private set; }
        public float Duration { get; private set; }

        public Effect(EffectConfig config, PawnController owner, PawnController source, float value, float duration)
        {
            Config = config;
            Owner = owner;
            Source = source;
            Value = value;
            Duration = duration;
        }

        public virtual void OnApply()
        {

        }

        public void OnTick(float deltaTime)
        {
            if (Duration > 0f)
            {
                ApplyOnTick(deltaTime);
                Duration -= deltaTime;
            }
            else
            {
                Owner.Status.EffectsHolder.RemoveEffect(this);
            }
        }

        protected virtual void ApplyOnTick(float deltaTime)
        {

        }

        public virtual void OnRemove()
        {

        }
    }
}