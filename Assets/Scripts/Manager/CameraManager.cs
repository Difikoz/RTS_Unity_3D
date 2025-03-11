using UnityEngine;

namespace WinterUniverse
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private bool _freeMove;
        [SerializeField] private Transform _heightRoot;
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private Transform _rotationRoot;
        [SerializeField] private float _rotateSpeed = 45f;
        [SerializeField] private float _minAngle = 45f;
        [SerializeField] private float _maxAngle = 90f;
        [SerializeField] private Transform _zoomRoot;
        [SerializeField] private float _zoomStep = 2f;
        [SerializeField] private float _zoomSpeed = 4f;
        [SerializeField] private float _minZoom = 10f;
        [SerializeField] private float _maxZoom = 100f;

        private PlayerInputActions _inputActions;
        private Vector3 _currentWorldPosition;
        private Vector3 _currentZoomPosition;
        private Vector2 _cursorInput;
        private Vector2 _moveInput;
        private Vector2 _lookInput;
        private bool _lockTargetPressed;
        private float _xRot;
        private Ray _cameraRay;
        private RaycastHit _checkHeightHit;

        public void InitializeComponent()
        {
            _inputActions = new();
            _inputActions.Enable();
            _inputActions.Camera.Cursor.performed += ctx => _cursorInput = ctx.ReadValue<Vector2>();
            _inputActions.Camera.Zoom.performed += ctx => OnZoomInputPerfomed(ctx.ReadValue<Vector2>());
            _inputActions.Camera.Interact.performed += ctx => OnInteractInputPerfomed();
            _inputActions.Camera.LookTarget.performed += ctx => OnLockTargetInputPerfomed();
            _inputActions.Camera.LookTarget.canceled += ctx => OnLockTargetInputCanceled();
            _xRot = _rotationRoot.localEulerAngles.x;
            _currentZoomPosition = _zoomRoot.localPosition;
        }

        public void ResetComponent()
        {
            _inputActions.Camera.Cursor.performed -= ctx => _cursorInput = ctx.ReadValue<Vector2>();
            _inputActions.Camera.Zoom.performed -= ctx => OnZoomInputPerfomed(ctx.ReadValue<Vector2>());
            _inputActions.Camera.Interact.performed -= ctx => OnInteractInputPerfomed();
            _inputActions.Camera.LookTarget.performed -= ctx => OnLockTargetInputPerfomed();
            _inputActions.Camera.LookTarget.canceled -= ctx => OnLockTargetInputCanceled();
            _inputActions.Disable();
        }

        private void OnZoomInputPerfomed(Vector2 value)
        {
            _currentZoomPosition.z = Mathf.Clamp(_currentZoomPosition.z + (value.y * _zoomStep), -_maxZoom, -_minZoom);
        }

        private void OnInteractInputPerfomed()
        {
            _cameraRay = Camera.main.ScreenPointToRay(_cursorInput);
            if (Physics.Raycast(_cameraRay, out RaycastHit hit, 1000f))
            {
                GameManager.StaticInstance.ControllersManager.Player.Pawn.Locomotion.SetDestination(hit.point);
            }
        }

        private void OnLockTargetInputPerfomed()
        {
            _lockTargetPressed = true;
            // lock target???
        }

        private void OnLockTargetInputCanceled()
        {
            _lockTargetPressed = false;
        }

        public void OnTick(float deltaTime)
        {
            if (_freeMove)
            {
                _moveInput = _inputActions.Camera.Move.ReadValue<Vector2>();
                if (_moveInput != Vector2.zero)
                {
                    _currentWorldPosition += (transform.forward * _moveInput.y + transform.right * _moveInput.x).normalized * _moveSpeed * deltaTime;
                }
                if (Physics.Raycast(_heightRoot.position, Vector3.down, out _checkHeightHit, 1000f, GameManager.StaticInstance.LayersManager.ObstacleMask))
                {
                    _currentWorldPosition.y = _checkHeightHit.point.y;
                }
                else if (Physics.Raycast(_heightRoot.position + (Vector3.up * 1000f), Vector3.down, out _checkHeightHit, 1000f, GameManager.StaticInstance.LayersManager.ObstacleMask))
                {
                    _currentWorldPosition.y = _checkHeightHit.point.y;
                }
                if (transform.position != _currentWorldPosition)
                {
                    transform.position = Vector3.MoveTowards(transform.position, _currentWorldPosition, _moveSpeed * deltaTime);
                }
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, GameManager.StaticInstance.ControllersManager.Player.Pawn.transform.position, _moveSpeed * deltaTime);
                _currentWorldPosition = transform.position;
            }
            if (_lockTargetPressed)
            {
                _lookInput = _inputActions.Camera.Look.ReadValue<Vector2>();
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
            if (_zoomRoot.localPosition != _currentZoomPosition)
            {
                _zoomRoot.localPosition = Vector3.MoveTowards(_zoomRoot.localPosition, _currentZoomPosition, (_zoomSpeed + Vector3.Distance(_zoomRoot.localPosition, _currentZoomPosition)) * deltaTime);
            }
        }
    }
}