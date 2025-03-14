using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(PawnAnimator))]
    [RequireComponent(typeof(PawnInteraction))]
    [RequireComponent(typeof(PawnLocomotion))]
    [RequireComponent(typeof(PawnStatus))]
    public class PawnController : MonoBehaviour
    {
        private PawnAnimator _animator;
        private PawnInteraction _interaction;
        private PawnLocomotion _locomotion;
        private PawnStatus _status;

        public PawnAnimator Animator => _animator;
        public PawnInteraction Interaction => _interaction;
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
            _interaction = GetComponent<PawnInteraction>();
            _locomotion = GetComponent<PawnLocomotion>();
            _status = GetComponent<PawnStatus>();
        }

        private void InitializeComponents()
        {
            _animator.Initialize();
            _interaction.Initialize();
            _locomotion.Initialize();
            _status.Initialize();
        }

        public void OnTick(float deltaTime)
        {
            _animator.OnTick(deltaTime);
            _interaction.OnTick(deltaTime);
            _locomotion.OnTick(deltaTime);
            _status.OnTick(deltaTime);
        }
    }
}