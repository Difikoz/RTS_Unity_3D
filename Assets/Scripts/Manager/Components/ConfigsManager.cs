using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ConfigsManager : BasicComponent
    {
        [SerializeField] private List<StatConfig> _pawnStats = new();
        [SerializeField] private List<StateKeyConfig> _pawnStates = new();
        [SerializeField] private List<FactionConfig> _factions = new();
        [SerializeField] private List<WeaponItemConfig> _weapons = new();
        [SerializeField] private List<ArmorItemConfig> _armors = new();
        [SerializeField] private List<ConsumableItemConfig> _consumables = new();
        [SerializeField] private List<ResourceItemConfig> _resources = new();

        private List<ItemConfig> _items;

        public List<StatConfig> PawnStats => _pawnStats;
        public List<StateKeyConfig> PawnStates => _pawnStates;
        public List<FactionConfig> Factions => _factions;
        public List<ItemConfig> Items => _items;

        public override void InitializeComponent()
        {
            base.InitializeComponent();
            _items = new();
            foreach (WeaponItemConfig config in _weapons)
            {
                _items.Add(config);
            }
            foreach (ArmorItemConfig config in _armors)
            {
                _items.Add(config);
            }
            foreach (ConsumableItemConfig config in _consumables)
            {
                _items.Add(config);
            }
            foreach (ResourceItemConfig config in _resources)
            {
                _items.Add(config);
            }
        }

        public FactionConfig GetFaction(string id)
        {
            foreach (FactionConfig config in _factions)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }

        public ItemConfig GetItem(string id)
        {
            foreach (ItemConfig config in _items)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }

        public WeaponItemConfig GetWeapon(string id)
        {
            foreach (WeaponItemConfig config in _weapons)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }

        public ArmorItemConfig GetArmor(string id)
        {
            foreach (ArmorItemConfig config in _armors)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }

        public ConsumableItemConfig GetConsumable(string id)
        {
            foreach (ConsumableItemConfig config in _consumables)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }

        public ResourceItemConfig GetResource(string id)
        {
            foreach (ResourceItemConfig config in _resources)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }
    }
}