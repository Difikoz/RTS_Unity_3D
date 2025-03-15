using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnStatus : MonoBehaviour
    {
        public Action<float, float> OnHealthChanged;
        public Action<float, float> OnStaminaChanged;
        public Action OnDied;
        public Action OnRevived;

        [SerializeField] private StatCreatorConfig _statCreatorConfig;
        [SerializeField] private StateHolderConfig _stateHolderConfig;
        [SerializeField] private StateCreatorConfig _stateCreatorConfig;
        [SerializeField] protected float _regenerationTickCooldown = 0.2f;
        [SerializeField] protected float _healthRegenerationDelayCooldown = 10f;
        [SerializeField] protected float _staminaRegenerationDelayCooldown = 5f;

        private PawnController _pawn;
        private StatHolder _statHolder;
        private StateHolder _stateHolder;

        protected float _healthCurrent;
        protected float _staminaCurrent;
        protected float _healthRegenerationCurrentTickTime;
        protected float _healthRegenerationCurrentDelayTime;
        protected float _staminaRegenerationCurrentTickTime;
        protected float _staminaRegenerationCurrentDelayTime;

        public StatHolder StatHolder => _statHolder;
        public StateHolder StateHolder => _stateHolder;
        public float HealthPercent => _healthCurrent / _statHolder.HealthMax.CurrentValue;
        public float StaminaPercent => _staminaCurrent / _statHolder.StaminaMax.CurrentValue;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _statHolder = new(_statCreatorConfig.Stats);
            _stateHolder = new(_stateHolderConfig.States);
            _stateHolder.Initialize(_stateCreatorConfig.States);
        }

        public void OnTick(float deltaTime)
        {
            if (_healthRegenerationCurrentDelayTime >= _healthRegenerationDelayCooldown)
            {
                if (_healthRegenerationCurrentTickTime >= _regenerationTickCooldown)
                {
                    RestoreHealthCurrent(_statHolder.HealthRegeneration.CurrentValue * _regenerationTickCooldown);
                    _healthRegenerationCurrentTickTime = 0f;
                }
                else
                {
                    _healthRegenerationCurrentTickTime += deltaTime;
                }
            }
            else
            {
                _healthRegenerationCurrentDelayTime += deltaTime;
            }
            if (_staminaRegenerationCurrentDelayTime >= _staminaRegenerationDelayCooldown)
            {
                if (_staminaRegenerationCurrentTickTime >= _regenerationTickCooldown)
                {
                    RestoreStaminaCurrent(_statHolder.StaminaRegeneration.CurrentValue * _regenerationTickCooldown);
                    _staminaRegenerationCurrentTickTime = 0f;
                }
                else
                {
                    _staminaRegenerationCurrentTickTime += deltaTime;
                }
            }
            else
            {
                _staminaRegenerationCurrentDelayTime += deltaTime;
            }
        }

        public void ReduceHealthCurrent(float value, DamageTypeConfig type, PawnController source = null)
        {
            if (_stateHolder.CompareStateValue("Is Dead", true) || value <= 0f)
            {
                return;
            }
            if (source != null)
            {

            }
            float resistance = _statHolder.GetStat(type.ResistanceStat.ID).CurrentValue;
            if (resistance < 100f)
            {
                _healthRegenerationCurrentDelayTime = 0f;
                value -= value * resistance / 100f;
                _healthCurrent = Mathf.Clamp(_healthCurrent - value, 0f, _statHolder.HealthMax.CurrentValue);
                if (_healthCurrent <= 0f)
                {
                    Die(source);
                }
                else
                {
                    OnHealthChanged?.Invoke(_healthCurrent, _statHolder.HealthMax.CurrentValue);
                }
            }
        }

        public void RestoreHealthCurrent(float value)
        {
            if (_stateHolder.CompareStateValue("Is Dead", true) || value <= 0f)
            {
                return;
            }
            _healthCurrent = Mathf.Clamp(_healthCurrent + value, 0f, _statHolder.HealthMax.CurrentValue);
            OnHealthChanged?.Invoke(_healthCurrent, _statHolder.HealthMax.CurrentValue);
        }

        public void ReduceStaminaCurrent(float value)
        {
            if (_stateHolder.CompareStateValue("Is Dead", true) || value <= 0f)
            {
                return;
            }
            _staminaRegenerationCurrentDelayTime = 0f;
            _staminaCurrent = Mathf.Clamp(_staminaCurrent - value, 0f, _statHolder.StaminaMax.CurrentValue);
            OnStaminaChanged?.Invoke(_staminaCurrent, _statHolder.StaminaMax.CurrentValue);
        }

        public void RestoreStaminaCurrent(float value)
        {
            if (_stateHolder.CompareStateValue("Is Dead", true) || value <= 0f)
            {
                return;
            }
            _staminaCurrent = Mathf.Clamp(_staminaCurrent + value, 0f, _statHolder.StaminaMax.CurrentValue);
            OnStaminaChanged?.Invoke(_staminaCurrent, _statHolder.StaminaMax.CurrentValue);
        }

        private void Die(PawnController source = null)
        {
            if (_stateHolder.CompareStateValue("Is Dead", true))
            {
                return;
            }
            if (source != null)
            {
                // check target
            }
            _healthCurrent = 0f;
            _staminaCurrent = 0f;
            OnHealthChanged?.Invoke(_healthCurrent, _statHolder.HealthMax.CurrentValue);
            OnStaminaChanged?.Invoke(_staminaCurrent, _statHolder.StaminaMax.CurrentValue);
            _stateHolder.SetStateValue("Is Dead", true);
            _pawn.Animator.PlayActionAnimation("Death");
            OnDied?.Invoke();
        }

        public void Revive()
        {
            _pawn.Animator.PlayActionAnimation("Revive");
            _stateHolder.SetStateValue("Is Dead", false);
            RestoreHealthCurrent(_statHolder.HealthMax.CurrentValue);
            RestoreStaminaCurrent(_statHolder.StaminaMax.CurrentValue);
            OnRevived?.Invoke();
        }
    }
}