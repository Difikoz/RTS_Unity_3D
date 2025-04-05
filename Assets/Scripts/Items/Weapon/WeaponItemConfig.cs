using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Winter Universe/Item/New Weapon")]
    public class WeaponItemConfig : ItemConfig
    {
        [SerializeField] private List<StatModifierCreator> _modifiers = new();
        [SerializeField] private List<DamageType> _damageTypes = new();
        [SerializeField] private List<EffectCreator> _damageEffects = new();

        public List<StatModifierCreator> Modifiers => _modifiers;
        public List<DamageType> DamageTypes => _damageTypes;
        public List<EffectCreator> DamageEffects => _damageEffects;

        private void OnValidate()
        {
            _itemType = ItemType.Weapon;
        }
    }
}