using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class WeaponSlot : MonoBehaviour
    {
        private PawnController _pawn;
        private WeaponItemConfig _config;
        private GameObject _model;

        public WeaponItemConfig Config => _config;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
        }

        public void ChangeConfig(WeaponItemConfig config)
        {
            if (_config != null)
            {
                LeanPool.Despawn(_model);
                _model = null;
            }
            _config = config;
            if (_config != null)
            {
                _model = LeanPool.Spawn(_config.Model, transform);
            }
        }

        public bool CanAttack()
        {
            return _config != null && !_pawn.Status.StateHolder.CompareStateValue("Is Perfoming Action", false);
        }

        public void OnAttack()
        {
            _pawn.Animator.PlayActionAnimation("Attack");
        }
    }
}