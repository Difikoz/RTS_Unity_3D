using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Animator))]
    public class PawnAnimator : MonoBehaviour
    {
        private PawnController _pawn;
        private Animator _animator;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _animator = GetComponent<Animator>();
        }

        public void OnTick(float deltaTime)
        {
            _animator.SetFloat("Forward Velocity", _pawn.Status.ForwardVelocity);
            _animator.SetFloat("Right Velocity", _pawn.Status.RightVelocity);
            _animator.SetFloat("Turn Velocity", _pawn.Status.TurnVelocity);
            _animator.SetBool("Is Moving", _pawn.Status.IsMoving);
        }

        public void PlayActionAnimation(string name, float fadeTime = 0.1f, bool isPerfomingAction = true)
        {
            _pawn.Status.IsPerfomingAction = isPerfomingAction;
            _animator.CrossFade(name, fadeTime);
        }
    }
}