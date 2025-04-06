using UnityEngine;

namespace WinterUniverse
{
    public class PlayerController : OwnerController
    {
        private PlayerInputActions _inputActions;
        private bool _setDestinationPerfomed;
        private bool _toggleSprintPerfomed;
        private bool _stopMovementPerfomed;

        public override void InitializeComponent()
        {
            base.InitializeComponent();
            _inputActions = new();
        }

        public override void EnableComponent()
        {
            base.EnableComponent();
            _inputActions.Enable();
            _inputActions.Pawn.SetDestination.performed += ctx => _setDestinationPerfomed = true;
            _inputActions.Pawn.ToggleSprint.canceled += ctx => _toggleSprintPerfomed = true;
            _inputActions.Pawn.StopMovement.performed += ctx => _stopMovementPerfomed = true;
        }

        public override void DisableComponent()
        {
            _inputActions.Pawn.SetDestination.performed -= ctx => _setDestinationPerfomed = true;
            _inputActions.Pawn.ToggleSprint.canceled -= ctx => _toggleSprintPerfomed = true;
            _inputActions.Pawn.StopMovement.performed -= ctx => _stopMovementPerfomed = true;
            _inputActions.Disable();
            base.DisableComponent();
        }

        public override void UpdateComponent()
        {
            base.UpdateComponent();
            if (_setDestinationPerfomed)
            {
                _setDestinationPerfomed = false;
                if (Physics.Raycast(GameManager.StaticInstance.CameraManager.CameraRay, out RaycastHit hit, 1000f, GameManager.StaticInstance.LayersManager.DetectableMask))
                {
                    if (hit.collider.TryGetComponent(out PawnController pawn))
                    {
                        Chase(pawn);
                    }
                    else
                    {
                        MoveTo(hit.point);
                    }
                    GameManager.StaticInstance.CameraManager.SetTarget(Pawn);
                }
            }
            else if (_stopMovementPerfomed)
            {
                _stopMovementPerfomed = false;
                StopMovement();
            }
            if (_toggleSprintPerfomed)
            {
                _toggleSprintPerfomed = false;
                ToggleSprint();
            }
        }

        public override void OnPawnChased(OwnerController owner)
        {
            throw new System.NotImplementedException();
        }

        public override void OnSettlementEntered(Settlement settlement)
        {
            throw new System.NotImplementedException();
        }
    }
}