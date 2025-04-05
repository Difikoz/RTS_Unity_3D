using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnFaction : PawnComponent
    {
        public Action OnFactionChanged;

        public FactionConfig Config { get; private set; }

        public void ChangeConfig(FactionConfig config)
        {
            Config = config;
            OnFactionChanged?.Invoke();
        }
    }
}