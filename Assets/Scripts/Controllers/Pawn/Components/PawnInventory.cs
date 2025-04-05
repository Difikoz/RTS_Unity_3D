using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnInventory : PawnComponent
    {
        public Action OnInventoryChanged;

        public Dictionary<string, ItemStack> Stacks { get; private set; }

        public override void InitializeComponent()
        {
            base.InitializeComponent();
            Stacks = new();
        }

        public void AddItem(ItemStack stack)
        {
            AddItem(stack.Item, stack.Amount);
        }

        public void AddItem(ItemConfig item, int amount = 1)
        {
            if (Stacks.ContainsKey(item.ID))
            {
                Stacks[item.ID].AddToStack(amount);
            }
            else
            {
                Stacks.Add(item.ID, new(item, amount));
            }
            UpdateInventory();
        }

        public bool RemoveItem(ItemStack stack)
        {
            return RemoveItem(stack.Item, stack.Amount);
        }

        public bool RemoveItem(ItemConfig item, int amount = 1)
        {
            if (AmountOfItem(item) < amount)
            {
                return false;
            }
            if (Stacks[item.ID].Amount > amount)
            {
                Stacks[item.ID].RemoveFromStack(amount);
            }
            else
            {
                Stacks.Remove(item.ID);
            }
            UpdateInventory();
            return true;
        }

        public bool DropItem(ItemStack stack)
        {
            return DropItem(stack.Item, stack.Amount);
        }

        public bool DropItem(ItemConfig item, int amount = 1)
        {
            if (RemoveItem(item, amount))
            {
                //spawn
                UpdateInventory();
                return true;
            }
            return false;
        }

        public int AmountOfItem(ItemConfig item)
        {
            if (Stacks.ContainsKey(item.ID))
            {
                return Stacks[item.ID].Amount;
            }
            return 0;
        }

        public bool GetWeapon(out WeaponItemConfig item)
        {
            item = null;
            int price = 0;
            foreach (KeyValuePair<string, ItemStack> stack in Stacks)
            {
                if (stack.Value.Item.ItemType == ItemType.Weapon && stack.Value.Item.Price > price)
                {
                    item = (WeaponItemConfig)stack.Value.Item;
                    price = item.Price;
                }
            }
            return item != null;
        }

        public bool GetArmor(out ArmorItemConfig item)
        {
            item = null;
            int price = 0;
            foreach (KeyValuePair<string, ItemStack> stack in Stacks)
            {
                if (stack.Value.Item.ItemType == ItemType.Armor && stack.Value.Item.Price > price)
                {
                    item = (ArmorItemConfig)stack.Value.Item;
                    price = item.Price;
                }
            }
            return item != null;
        }

        public bool GetConsumable(out ConsumableItemConfig item)
        {
            item = null;
            int price = 0;
            foreach (KeyValuePair<string, ItemStack> stack in Stacks)
            {
                if (stack.Value.Item.ItemType == ItemType.Consumable && stack.Value.Item.Price > price)
                {
                    item = (ConsumableItemConfig)stack.Value.Item;
                    price = item.Price;
                }
            }
            return item != null;
        }

        private void UpdateInventory()
        {
            OnInventoryChanged?.Invoke();
        }
    }
}