using UnityEngine;

namespace WinterUniverse
{
    public class PawnController : MonoBehaviour
    {
        private PawnAnimator _animator;
        private PawnLocomotion _locomotion;
        private PawnStatus _status;

        public PawnAnimator Animator => _animator;
        public PawnLocomotion Locomotion => _locomotion;
        public PawnStatus Status => _status;

        public void Create()
        {
            GetComponents();
            InitializeComponents();
        }

        private void GetComponents()
        {
            _animator = GetComponent<PawnAnimator>();
            _locomotion = GetComponent<PawnLocomotion>();
            _status = GetComponent<PawnStatus>();
        }

        private void InitializeComponents()
        {
            _animator.Initialize();
            _locomotion.Initialize();
            _status.Initialize();
        }

        public void OnTick(float deltaTime)
        {
            _animator.OnTick(deltaTime);
            _locomotion.OnTick(deltaTime);
            _status.OnTick(deltaTime);
        }
    }
}