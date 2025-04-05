using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class WeaponSlot : MonoBehaviour
    {
        private GameObject _model;

        public WeaponItemConfig Config { get; private set; }

        public void ChangeConfig(WeaponItemConfig config)
        {
            if (_model != null)
            {
                LeanPool.Despawn(_model);
                _model = null;
            }
            Config = config;
            if (Config != null)
            {
                _model = LeanPool.Spawn(Config.Model, transform);
            }
        }
    }
}