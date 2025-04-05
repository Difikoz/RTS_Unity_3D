using UnityEngine;

namespace WinterUniverse
{
    public abstract class ItemConfig : BasicInfoConfig
    {
        [SerializeField] protected ItemType _itemType;
        [SerializeField] protected GameObject _model;
        [SerializeField] protected float _weight = 0.5f;
        [SerializeField] protected int _price = 100;

        public ItemType ItemType => _itemType;
        public GameObject Model => _model;
        public float Weight => _weight;
        public int Price => _price;

        public void Use(PawnController pawn)
        {
            switch (_itemType)
            {
                case ItemType.Weapon:
                    pawn.Equipment.EquipWeapon((WeaponItemConfig)this);
                    break;
                case ItemType.Armor:
                    pawn.Equipment.EquipArmor((ArmorItemConfig)this);
                    break;
                case ItemType.Consumable:
                    pawn.Status.EffectsHolder.ApplyEffects((ConsumableItemConfig)this);
                    break;
                case ItemType.Resource:
                    break;
            }
        }
    }
}