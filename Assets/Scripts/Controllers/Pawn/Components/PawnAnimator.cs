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
            _animator.SetFloat("Forward Velocity", _pawn.Input.ForwardVelocity);
            _animator.SetFloat("Right Velocity", _pawn.Input.RightVelocity);
            _animator.SetFloat("Turn Velocity", _pawn.Input.TurnVelocity);
            _animator.SetBool("Is Moving", _pawn.Status.StateHolder.CompareStateValue("Is Moving", true));
        }

        public void PlayActionAnimation(string name, float fadeTime = 0.1f, bool isPerfomingAction = true)
        {
            _pawn.Status.StateHolder.SetStateValue("Is Perfoming Action", isPerfomingAction);
            _animator.CrossFade(name, fadeTime);
        }
    }
}