using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnStatus : PawnComponent
    {
        public Action<float, float> OnHealthChanged;
        public Action<float, float> OnStaminaChanged;
        public Action OnDied;
        public Action OnRevived;

        [SerializeField] private StateCreatorConfig _testStateCreatorConfig;
        [SerializeField] private float _regenerationTick = 0.2f;
        [SerializeField] private float _healthRegenerationDelay = 10f;
        [SerializeField] private float _staminaRegenerationDelay = 5f;

        private float _healthCurrent;
        private float _staminaCurrent;
        private float _healthRegenerationCurrentTick;
        private float _staminaRegenerationCurrentTick;
        private float _healthRegenerationCurrentDelay;
        private float _staminaRegenerationCurrentDelay;

        public StatsHolder StatsHolder { get; private set; }
        public StateHolder StateHolder { get; private set; }
        public EffectsHolder EffectsHolder { get; private set; }
        public float HealthPercent => _healthCurrent / StatsHolder.GetStat("Health Max").CurrentValue;
        public float StaminaPercent => _staminaCurrent / StatsHolder.GetStat("Stamina Max").CurrentValue;

        public override void InitializeComponent()
        {
            base.InitializeComponent();
            StatsHolder = new(GameManager.StaticInstance.ConfigsManager.PawnStats);
            StateHolder = new(GameManager.StaticInstance.ConfigsManager.PawnStates);
            EffectsHolder = new(_pawn);
            StateHolder.ApplyStates(_testStateCreatorConfig.States);// rework to apply on spawn
        }

        public override void UpdateComponent()
        {
            base.UpdateComponent();
            if (_healthRegenerationCurrentDelay >= _healthRegenerationDelay)
            {
                if (_healthRegenerationCurrentTick >= _regenerationTick)
                {
                    RestoreHealthCurrent(StatsHolder.GetStat("Health Regeneration").CurrentValue * _regenerationTick);
                    _healthRegenerationCurrentTick = 0f;
                }
                else
                {
                    _healthRegenerationCurrentTick += Time.deltaTime;
                }
            }
            else
            {
                _healthRegenerationCurrentDelay += Time.deltaTime;
            }
            if (_staminaRegenerationCurrentDelay >= _staminaRegenerationDelay)
            {
                if (_staminaRegenerationCurrentTick >= _regenerationTick)
                {
                    RestoreStaminaCurrent(StatsHolder.GetStat("Stamina Regeneration").CurrentValue * _regenerationTick);
                    _staminaRegenerationCurrentTick = 0f;
                }
                else
                {
                    _staminaRegenerationCurrentTick += Time.deltaTime;
                }
            }
            else
            {
                _staminaRegenerationCurrentDelay += Time.deltaTime;
            }
            EffectsHolder.HandleEffects();
        }

        public void ReduceHealthCurrent(float value, DamageTypeConfig type, PawnController source = null)
        {
            if (StateHolder.CompareStateValue("Is Dead", true) || value <= 0f)
            {
                return;
            }
            if (source != null)
            {

            }
            float resistance = StatsHolder.GetStat(type.ResistanceStat.ID).CurrentValue;
            if (resistance < 100f)
            {
                _healthRegenerationCurrentDelay = 0f;
                value -= value * resistance / 100f;
                value *= StatsHolder.GetStat("Damage Taken").CurrentValue / 100f;
                _healthCurrent = Mathf.Clamp(_healthCurrent - value, 0f, StatsHolder.GetStat("Health Max").CurrentValue);
                if (_healthCurrent <= 0f)
                {
                    Die(source);
                }
                else
                {
                    OnHealthChanged?.Invoke(_healthCurrent, StatsHolder.GetStat("Health Max").CurrentValue);
                }
            }
        }

        public void RestoreHealthCurrent(float value)
        {
            if (StateHolder.CompareStateValue("Is Dead", true) || value <= 0f)
            {
                return;
            }
            _healthCurrent = Mathf.Clamp(_healthCurrent + value, 0f, StatsHolder.GetStat("Health Max").CurrentValue);
            OnHealthChanged?.Invoke(_healthCurrent, StatsHolder.GetStat("Health Max").CurrentValue);
        }

        public void ReduceStaminaCurrent(float value)
        {
            if (StateHolder.CompareStateValue("Is Dead", true) || value <= 0f)
            {
                return;
            }
            _staminaRegenerationCurrentDelay = 0f;
            _staminaCurrent = Mathf.Clamp(_staminaCurrent - value, 0f, StatsHolder.GetStat("Stamina Max").CurrentValue);
            OnStaminaChanged?.Invoke(_staminaCurrent, StatsHolder.GetStat("Stamina Max").CurrentValue);
        }

        public void RestoreStaminaCurrent(float value)
        {
            if (StateHolder.CompareStateValue("Is Dead", true) || value <= 0f)
            {
                return;
            }
            _staminaCurrent = Mathf.Clamp(_staminaCurrent + value, 0f, StatsHolder.GetStat("Stamina Max").CurrentValue);
            OnStaminaChanged?.Invoke(_staminaCurrent, StatsHolder.GetStat("Stamina Max").CurrentValue);
        }

        private void Die(PawnController source = null)
        {
            if (StateHolder.CompareStateValue("Is Dead", true))
            {
                return;
            }
            if (source != null)
            {
                // check target
            }
            _healthCurrent = 0f;
            _staminaCurrent = 0f;
            OnHealthChanged?.Invoke(_healthCurrent, StatsHolder.GetStat("Health Max").CurrentValue);
            OnStaminaChanged?.Invoke(_staminaCurrent, StatsHolder.GetStat("Stamina Max").CurrentValue);
            StateHolder.SetStateValue("Is Dead", true);
            _pawn.Animator.PlayActionAnimation("Death");
            OnDied?.Invoke();
        }

        public void Revive()
        {
            _pawn.Animator.PlayActionAnimation("Revive");
            StateHolder.SetStateValue("Is Dead", false);
            RestoreHealthCurrent(StatsHolder.GetStat("Health Max").CurrentValue);
            RestoreStaminaCurrent(StatsHolder.GetStat("Stamina Max").CurrentValue);
            OnRevived?.Invoke();
        }
    }
}