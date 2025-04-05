using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class EffectsHolder
    {
        public Action OnEffectsChanged;

        private PawnController _pawn;
        private readonly float _tickDelay = 0.5f;
        private float _tickTime;

        public List<Effect> AllEffects { get; private set; }

        public EffectsHolder(PawnController pawn)
        {
            _pawn = pawn;
            AllEffects = new();
        }

        public void HandleEffects()
        {
            if (_tickTime >= _tickDelay)
            {
                _tickTime = 0f;
                for (int i = AllEffects.Count - 1; i >= 0; i--)
                {
                    AllEffects[i].OnTick(_tickDelay);
                }
                OnEffectsChanged?.Invoke();
            }
            else
            {
                _tickTime += Time.deltaTime;
            }
        }

        public void ApplyEffects(ConsumableItemConfig consumable)
        {
            if (_pawn.Inventory.RemoveItem(consumable))
            {
                ApplyEffects(consumable.Effects, _pawn);
            }
        }

        public void ApplyEffects(List<EffectCreator> effects)
        {
            ApplyEffects(effects, _pawn);
        }

        public void ApplyEffects(List<EffectCreator> effects, PawnController source)
        {
            foreach (EffectCreator effect in effects)
            {
                if (effect.Triggered)
                {
                    AddEffect(effect.Config.GetEffect(_pawn, source, effect.Value, effect.Duration));
                }
            }
        }

        public void AddEffect(Effect effect)
        {
            AllEffects.Add(effect);
        }

        public void RemoveEffect(Effect effect)
        {
            if (AllEffects.Contains(effect))
            {
                AllEffects.Remove(effect);
            }
        }
    }
}