using UnityEngine;

namespace WinterUniverse
{
    public class SwitchableAnimatedObject : SwitchableBase
    {
        [SerializeField] private float _animationSpeed = 1f;

        private Animator _animator;

        protected override void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            base.Awake();
        }

        public override void SwitchOn()
        {
            _animator.SetFloat("Speed", _animationSpeed);
            _animator.SetBool("Switched", true);
        }

        public override void SwitchOff()
        {
            _animator.SetFloat("Speed", _animationSpeed);
            _animator.SetBool("Switched", false);
        }
    }
}