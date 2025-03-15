using UnityEngine;

namespace WinterUniverse
{
    public class ActionInspectInventory : ActionBase
    {
        public override void OnStart()
        {
            base.OnStart();
            if (_npc.Pawn.Inventory.GetWeapon(out WeaponItemConfig weapon) && (_npc.Pawn.Equipment.WeaponSlot.Config == null || _npc.Pawn.Equipment.WeaponSlot.Config.Price < weapon.Price))
            {
                _npc.Pawn.Status.StateHolder.SetStateValue("Has Best Weapon", true);
            }
            else
            {
                _npc.Pawn.Status.StateHolder.SetStateValue("Has Best Weapon", false);
            }
            if (_npc.Pawn.Inventory.GetArmor(out ArmorItemConfig armor) && (_npc.Pawn.Equipment.ArmorSlot.Config == null || _npc.Pawn.Equipment.ArmorSlot.Config.Price < armor.Price))
            {
                _npc.Pawn.Status.StateHolder.SetStateValue("Has Best Armor", true);
            }
            else
            {
                _npc.Pawn.Status.StateHolder.SetStateValue("Has Best Armor", false);
            }
            _npc.Pawn.Status.StateHolder.SetStateValue("Has Consumable", _npc.Pawn.Inventory.GetConsumable(out _));
        }
    }
}