using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public abstract class ActionBase : MonoBehaviour
    {
        [SerializeField] protected ActionConfig _config;

        protected NPCController _npc;
        private float _timerTime;
        private Dictionary<string, bool> _effectsForCheck = new();
        private Dictionary<string, bool> _conditionsToStart = new();
        private Dictionary<string, bool> _conditionsToAbort = new();
        private Dictionary<string, bool> _conditionsToComplete = new();
        private Dictionary<string, bool> _effectsOnStart = new();
        private Dictionary<string, bool> _effectsOnAbort = new();
        private Dictionary<string, bool> _effectsOnComplete = new();

        public ActionConfig Config => _config;
        public Dictionary<string, bool> Conditions => _conditionsToStart;
        public Dictionary<string, bool> Effects => _effectsForCheck;

        public virtual void Initialize()
        {
            _npc = GetComponentInParent<NPCController>();
            foreach (StateCreator creator in _config.Results)
            {
                _effectsForCheck.Add(creator.Config.Key, creator.Value);
            }
            foreach (StateCreator creator in _config.ConditionsToStart)
            {
                _conditionsToStart.Add(creator.Config.Key, creator.Value);
            }
            foreach (StateCreator creator in _config.ConditionsToAbort)
            {
                _conditionsToAbort.Add(creator.Config.Key, creator.Value);
            }
            foreach (StateCreator creator in _config.ConditionsToComplete)
            {
                _conditionsToComplete.Add(creator.Config.Key, creator.Value);
            }
            foreach (StateCreator creator in _config.EffectsOnStart)
            {
                _effectsOnStart.Add(creator.Config.Key, creator.Value);
            }
            foreach (StateCreator creator in _config.EffectsOnAbort)
            {
                _effectsOnAbort.Add(creator.Config.Key, creator.Value);
            }
            foreach (StateCreator creator in _config.EffectsOnComplete)
            {
                _effectsOnComplete.Add(creator.Config.Key, creator.Value);
            }
        }

        public bool IsAchievable()
        {
            return true;
        }

        public bool IsAchievable(Dictionary<string, bool> givenConditions)
        {
            foreach (KeyValuePair<string, bool> condition in _conditionsToStart)
            {
                if (!givenConditions.ContainsKey(condition.Key))
                {
                    return false;
                }
                else if (givenConditions[condition.Key] != condition.Value)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CompareGivenConditionsWithPawnStates(Dictionary<string, bool> givenConditions)
        {
            foreach (KeyValuePair<string, bool> condition in givenConditions)
            {
                if (_npc.Pawn.Status.StateHolder.HasState(condition.Key) && !_npc.Pawn.Status.StateHolder.CompareStateValue(condition.Key, condition.Value))
                {
                    return false;
                }
            }
            return true;
        }

        public virtual bool CanStart()
        {
            if (_conditionsToStart.Count > 0 && !CompareGivenConditionsWithPawnStates(_conditionsToStart))
            {
                return false;
            }
            return true;
        }

        public virtual bool CanAbort()
        {
            if (_conditionsToAbort.Count > 0 && CompareGivenConditionsWithPawnStates(_conditionsToAbort))
            {
                return true;
            }
            return false;
        }

        public virtual bool CanComplete()
        {
            if (_config.CompleteOnReachedDestination && _npc.Pawn.Locomotion.ReachedDestination)
            {
                return true;
            }
            if (_config.CompleteOnAnimationEnd && _npc.Pawn.Status.StateHolder.CompareStateValue("Is Perfoming Action", false))
            {
                return true;
            }
            if (_config.CompleteOnTimerEnd && Time.time >= _timerTime + _config.TimerDuration)
            {
                return true;
            }
            if (_conditionsToComplete.Count > 0 && CompareGivenConditionsWithPawnStates(_conditionsToComplete))
            {
                return true;
            }
            return false;
        }

        public virtual void OnStart()
        {
            _timerTime = Time.time;
            foreach (KeyValuePair<string, bool> effect in _effectsOnStart)
            {
                _npc.Pawn.Status.StateHolder.SetStateValue(effect.Key, effect.Value);
            }
            if (_config.PlayAnimationOnStart)
            {
                _npc.Pawn.Animator.PlayActionAnimation(_config.AnimationOnStart);
            }
        }

        public virtual void OnAbort()
        {
            foreach (KeyValuePair<string, bool> effect in _effectsOnAbort)
            {
                _npc.Pawn.Status.StateHolder.SetStateValue(effect.Key, effect.Value);
            }
            if (_config.StopMovementOnAbort)
            {
                _npc.Pawn.Locomotion.StopMovement();
            }
        }

        public virtual void OnComplete()
        {
            foreach (KeyValuePair<string, bool> effect in _effectsOnComplete)
            {
                _npc.Pawn.Status.StateHolder.SetStateValue(effect.Key, effect.Value);
            }
            if (_config.StopMovementOnComplete)
            {
                _npc.Pawn.Locomotion.StopMovement();
            }
        }

        public virtual void OnUpdate(float deltaTime)
        {

        }
    }
}