using UnityEngine;

namespace WinterUniverse
{
    public class PawnAnimator : PawnComponent
    {
        private Animator _animator;

        public override void InitializeComponent()
        {
            base.InitializeComponent();
            _animator = GetComponent<Animator>();
        }

        public override void UpdateComponent()
        {
            base.UpdateComponent();
            _animator.SetFloat("Forward Velocity", _pawn.Locomotion.ForwardVelocity);
            _animator.SetFloat("Right Velocity", _pawn.Locomotion.RightVelocity);
            _animator.SetFloat("Turn Velocity", _pawn.Locomotion.TurnVelocity);
            _animator.SetBool("Is Moving", _pawn.Status.StateHolder.CompareStateValue("Is Moving", true));
        }

        public void PlayActionAnimation(string name, float fadeTime = 0.1f, bool isPerfomingAction = true)
        {
            _pawn.Status.StateHolder.SetStateValue("Is Perfoming Action", isPerfomingAction);
            _animator.CrossFade(name, fadeTime);
        }
    }
}