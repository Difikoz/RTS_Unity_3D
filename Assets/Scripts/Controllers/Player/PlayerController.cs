using UnityEngine;

namespace WinterUniverse
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInputActions _inputActions;
        private PawnController _pawn;
        private bool _sprintPressed;

        public PawnController Pawn => _pawn;
        public bool SprintPressed => _sprintPressed;

        public void InitializeComponent()
        {
            _pawn = GameManager.StaticInstance.PrefabsManager.GetPawn(transform);
            _pawn.Create();
            _inputActions = new();
            _inputActions.Enable();
            _inputActions.Player.Sprint.performed += ctx => OnSprintPerfomed();
            _inputActions.Player.Sprint.canceled += ctx => OnSprintCanceled();
            _inputActions.Player.Interact.performed += ctx => OnInteractPerfomed();
            _inputActions.Player.ResetTarget.performed += ctx => OnResetTargetPerfomed();
            _inputActions.Player.FollowTarget.performed += ctx => OnFollowTargetPerfomed();
            _inputActions.Player.AttackTarget.performed += ctx => OnAttackTargetPerfomed();
        }

        public void ResetComponent()
        {
            _inputActions.Player.Sprint.performed -= ctx => OnSprintPerfomed();
            _inputActions.Player.Sprint.canceled -= ctx => OnSprintCanceled();
            _inputActions.Player.Interact.performed -= ctx => OnInteractPerfomed();
            _inputActions.Player.ResetTarget.performed -= ctx => OnResetTargetPerfomed();
            _inputActions.Player.FollowTarget.performed -= ctx => OnFollowTargetPerfomed();
            _inputActions.Player.AttackTarget.performed -= ctx => OnAttackTargetPerfomed();
            _inputActions.Disable();
        }

        public void OnTick(float deltaTime)
        {
            _pawn.OnTick(deltaTime);
        }

        private void OnSprintPerfomed()
        {
            _sprintPressed = true;
        }

        private void OnSprintCanceled()
        {
            _sprintPressed = false;
        }

        private void OnInteractPerfomed()
        {
            if (_pawn.Interaction.Target != null)
            {
                _pawn.Interaction.InteractWithTarget();
            }
            else
            {
                _pawn.Interaction.InteractWithInteractable();
            }
        }

        private void OnResetTargetPerfomed()
        {
            _pawn.Interaction.ResetTarget();
        }

        private void OnFollowTargetPerfomed()
        {
            _pawn.Interaction.FollowTarget(_sprintPressed);
        }

        private void OnAttackTargetPerfomed()
        {
            _pawn.Interaction.AttackTarget(_sprintPressed);
        }
    }
}