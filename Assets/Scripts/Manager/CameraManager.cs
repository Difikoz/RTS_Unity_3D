using UnityEngine;

namespace WinterUniverse
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private float _followSpeed = 10f;
        [SerializeField] private Transform _rotationRoot;
        [SerializeField] private float _rotateSpeed = 45f;
        [SerializeField] private float _minAngle = 30f;
        [SerializeField] private float _maxAngle = 90f;
        [SerializeField] private Transform _collisionRoot;
        [SerializeField] private float _collisionRadius = 0.25f;
        [SerializeField] private float _collisionAvoidanceSpeed = 10f;

        private PlayerInputActions _inputActions;
        private Vector2 _cursorInput;
        private Vector2 _lookInput;
        private bool _freeLookPressed;
        private float _xRot;
        private Ray _cameraRay;
        private Vector3 _collisionCurrentOffset;
        private float _collisionDefaultOffset;
        private float _collisionRequiredOffset;
        private RaycastHit _collisionHit;

        public void InitializeComponent()
        {
            _inputActions = new();
            _inputActions.Enable();
            _inputActions.Camera.Cursor.performed += ctx => _cursorInput = ctx.ReadValue<Vector2>();
            _inputActions.Camera.SetDestination.performed += ctx => OnSetDestinationPerfomed();
            _inputActions.Camera.SetTarget.performed += ctx => OnSetTargetPerfomed();
            _inputActions.Camera.FreeLook.performed += ctx => OnFreeLookPerfomed();
            _inputActions.Camera.FreeLook.canceled += ctx => OnFreeLookCanceled();
            _xRot = _rotationRoot.localEulerAngles.x;
            _collisionDefaultOffset = _collisionRoot.localPosition.z;
        }

        public void ResetComponent()
        {
            _inputActions.Camera.Cursor.performed -= ctx => _cursorInput = ctx.ReadValue<Vector2>();
            _inputActions.Camera.SetDestination.performed -= ctx => OnSetDestinationPerfomed();
            _inputActions.Camera.SetTarget.performed -= ctx => OnSetTargetPerfomed();
            _inputActions.Camera.FreeLook.performed -= ctx => OnFreeLookPerfomed();
            _inputActions.Camera.FreeLook.canceled -= ctx => OnFreeLookCanceled();
            _inputActions.Disable();
        }

        private void OnSetDestinationPerfomed()
        {
            _cameraRay = Camera.main.ScreenPointToRay(_cursorInput);
            if (Physics.Raycast(_cameraRay, out RaycastHit hit, 1000f, GameManager.StaticInstance.LayersManager.ObstacleMask))
            {
                GameManager.StaticInstance.ControllersManager.Player.Pawn.Locomotion.SetDestination(hit.point, GameManager.StaticInstance.ControllersManager.Player.SprintPressed);
            }
        }

        private void OnSetTargetPerfomed()
        {
            _cameraRay = Camera.main.ScreenPointToRay(_cursorInput);
            if (Physics.Raycast(_cameraRay, out RaycastHit hit, 1000f, GameManager.StaticInstance.LayersManager.PawnMask) && hit.transform.TryGetComponent(out PawnController pawn))
            {
                GameManager.StaticInstance.ControllersManager.Player.Pawn.Interaction.SetTarget(pawn);
            }
        }

        private void OnFreeLookPerfomed()
        {
            _freeLookPressed = true;
        }

        private void OnFreeLookCanceled()
        {
            _freeLookPressed = false;
        }

        public void OnUpdate(float deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, GameManager.StaticInstance.ControllersManager.Player.Pawn.transform.position, _followSpeed * deltaTime);
            if (_freeLookPressed)
            {
                HandleFreeLook(deltaTime);
            }
            HandleCollision(deltaTime);
        }

        private void HandleFreeLook(float deltaTime)
        {
            _lookInput = _inputActions.Camera.LookAround.ReadValue<Vector2>();
            if (_lookInput.x != 0f)
            {
                transform.Rotate(Vector3.up * _lookInput.x * _rotateSpeed * deltaTime);
            }
            if (_lookInput.y != 0f)
            {
                _xRot = Mathf.Clamp(_xRot - (_lookInput.y * _rotateSpeed * deltaTime), _minAngle, _maxAngle);
                _rotationRoot.localRotation = Quaternion.Euler(_xRot, 0f, 0f);
            }
        }

        private void HandleCollision(float deltaTime)
        {
            _collisionRequiredOffset = _collisionDefaultOffset;
            Vector3 direction = (_collisionRoot.position - _rotationRoot.position).normalized;
            if (Physics.SphereCast(_rotationRoot.position, _collisionRadius, direction, out _collisionHit, Mathf.Abs(_collisionRequiredOffset), GameManager.StaticInstance.LayersManager.ObstacleMask))
            {
                _collisionRequiredOffset = -(Vector3.Distance(_rotationRoot.position, _collisionHit.point) - _collisionRadius);
            }
            if (Mathf.Abs(_collisionRequiredOffset) < _collisionRadius)
            {
                _collisionRequiredOffset = -_collisionRadius;
            }
            _collisionCurrentOffset.z = Mathf.Lerp(_collisionRoot.localPosition.z, _collisionRequiredOffset, _collisionAvoidanceSpeed * deltaTime);
            _collisionRoot.localPosition = _collisionCurrentOffset;
        }
    }
}