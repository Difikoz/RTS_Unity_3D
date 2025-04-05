using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnEquipment : PawnComponent
    {
        public Action OnEquipmentChanged;

        public WeaponSlot WeaponSlot { get; private set; }
        public ArmorSlot ArmorSlot { get; private set; }

        public override void InitializeComponent()
        {
            base.InitializeComponent();
            WeaponSlot = GetComponentInChildren<WeaponSlot>();
            ArmorSlot = GetComponentInChildren<ArmorSlot>();
        }

        public void EquipWeapon(WeaponItemConfig config, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (config == null || _pawn.Status.StateHolder.CompareStateValue("Is Perfoming Action", true))
            {
                return;
            }
            if (removeNewFromInventory)
            {
                _pawn.Inventory.RemoveItem(config);
            }
            UnequipWeapon(addOldToInventory);
            WeaponSlot.ChangeConfig(config);
            _pawn.Status.StateHolder.SetStateValue("Equipped Weapon", true);
            OnEquipmentChanged?.Invoke();
        }

        public void UnequipWeapon(bool addOldToInventory = true)
        {
            if (WeaponSlot.Config == null)
            {
                return;
            }
            if (addOldToInventory)
            {
                _pawn.Inventory.AddItem(WeaponSlot.Config);
            }
            WeaponSlot.ChangeConfig(null);
            _pawn.Status.StateHolder.SetStateValue("Equipped Weapon", false);
            OnEquipmentChanged?.Invoke();
        }

        public void EquipArmor(ArmorItemConfig config, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (config == null || _pawn.Status.StateHolder.CompareStateValue("Is Perfoming Action", true))
            {
                return;
            }
            if (removeNewFromInventory)
            {
                _pawn.Inventory.RemoveItem(config);
            }
            UnequipArmor(addOldToInventory);
            ArmorSlot.ChangeConfig(config);
            _pawn.Status.StateHolder.SetStateValue("Equipped Armor", true);
            OnEquipmentChanged?.Invoke();
        }

        public void UnequipArmor(bool addOldToInventory = true)
        {
            if (ArmorSlot.Config == null)
            {
                return;
            }
            if (addOldToInventory)
            {
                _pawn.Inventory.AddItem(ArmorSlot.Config);
            }
            ArmorSlot.ChangeConfig(null);
            _pawn.Status.StateHolder.SetStateValue("Equipped Armor", false);
            OnEquipmentChanged?.Invoke();
        }
    }
}