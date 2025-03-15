using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Winter Universe/Item/New Weapon")]
    public class WeaponItemConfig : ItemConfig
    {
        [SerializeField] private float _range = 2f;
        [SerializeField] private float _knockback = 2f;
        [SerializeField] private List<DamageType> _damageTypes = new();
        [SerializeField] private List<StatModifierCreator> _modifiers = new();
        [SerializeField] private List<EffectCreator> _effects = new();

        public float Range => _range;
        public float Knockback => _knockback;
        public List<DamageType> DamageTypes => _damageTypes;
        public List<StatModifierCreator> Modifiers => _modifiers;
        public List<EffectCreator> Effects => _effects;

        private void OnValidate()
        {
            _itemType = ItemType.Weapon;
        }

        public override void Use(PawnController pawn, bool fromInventory = true)
        {
            pawn.Equipment.EquipWeapon(this, fromInventory);
        }
    }
}