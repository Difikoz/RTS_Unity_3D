using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ArmorSlot : MonoBehaviour
    {
        [SerializeField] private List<ArmorRenderer> _armorRenderes = new();

        public ArmorItemConfig Config { get; private set; }

        public void ChangeConfig(ArmorItemConfig config)
        {
            Config = config;
            foreach (ArmorRenderer ar in _armorRenderes)
            {
                ar.Toggle(ar.Config == Config);
            }
        }
    }
}