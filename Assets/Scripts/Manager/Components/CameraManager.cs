using UnityEngine;

namespace WinterUniverse
{
    public class CameraManager : BasicComponent
    {
        [SerializeField] private Transform _groundCheckPoint;
        [SerializeField] private float _groundCheckDistance = 1000f;
        [SerializeField] private float _followSpeed = 10f;
        [SerializeField] private float _minMoveSpeed = 10f;
        [SerializeField] private float _maxMoveSpeed = 100f;
        [SerializeField, Range(1f, 10f)] private float _moveSpeedAccelerationMultiplier = 1.1f;
        [SerializeField] private Transform _rotationRoot;
        [SerializeField] private float _rotateSpeed = 45f;
        [SerializeField] private float _minAngle = 30f;
        [SerializeField] private float _maxAngle = 90f;
        [SerializeField] private Transform _zoomRoot;
        [SerializeField] private float _zoomStep = 5f;
        [SerializeField] private float _zoomSpeed = 10f;
        [SerializeField] private float _zoomMin = 10f;
        [SerializeField] private float _zoomMax = 100f;
        [SerializeField] private Transform _collisionRoot;
        [SerializeField] private float _collisionRadius = 0.25f;
        [SerializeField] private float _collisionAvoidanceSpeed = 10f;

        private PlayerInputActions _inputActions;
        private PawnController _target;
        private Vector2 _moveInput;
        private Vector2 _rotateInput;
        private float _currentMoveSpeed;
        private float _xRot;
        private float _requiredZoom;
        private float _currentZoom;
        private Vector3 _collisionCurrentOffset;
        private float _collisionDefaultOffset;
        private float _collisionRequiredOffset;
        private RaycastHit _collisionHit;
        private RaycastHit _groundHit;
        private bool _followPawn;

        public Ray CameraRay => Camera.main.ScreenPointToRay(_inputActions.Camera.Cursor.ReadValue<Vector2>());

        public override void InitializeComponent()
        {
            _inputActions = new();
        }

        public override void EnableComponent()
        {
            _inputActions.Enable();
            _inputActions.Camera.FollowPawn.performed += ctx => _followPawn = !_followPawn;
            _inputActions.Camera.Zoom.performed += ctx => OnZoomPerfomed(ctx.ReadValue<float>());
            _requiredZoom = -_zoomRoot.localPosition.z;
            _currentZoom = _requiredZoom;
            _xRot = _rotationRoot.localEulerAngles.x;
            _collisionDefaultOffset = _collisionRoot.localPosition.z;
            _currentMoveSpeed = _minMoveSpeed;
        }

        public override void DisableComponent()
        {
            _inputActions.Camera.FollowPawn.performed -= ctx => _followPawn = !_followPawn;
            _inputActions.Camera.Zoom.performed -= ctx => OnZoomPerfomed(ctx.ReadValue<float>());
            _inputActions.Disable();
        }

        public override void LateUpdateComponent()
        {
            base.LateUpdateComponent();
            if (_followPawn && _target != null)
            {
                HandleFollowing();
            }
            else
            {
                HandleMovement();
            }
            HandleRotation();
            HandleZooming();
            HandleCollision();
        }

        private void HandleFollowing()
        {
            transform.position = Vector3.Lerp(transform.position, _target.transform.position, _followSpeed * Time.deltaTime);
        }

        private void HandleMovement()
        {
            _moveInput = _inputActions.Camera.Move.ReadValue<Vector2>();
            if (_moveInput != Vector2.zero)
            {
                transform.Translate((Vector3.forward * _moveInput.y + Vector3.right * _moveInput.x) * _currentMoveSpeed * Time.deltaTime);
                if (Physics.Raycast(_groundCheckPoint.position, Vector3.down, out _groundHit, _groundCheckDistance, GameManager.StaticInstance.LayersManager.GroundMask))
                {
                    transform.position = Vector3.Lerp(transform.position, _groundHit.point, _currentMoveSpeed * Time.deltaTime);
                }
                if (_currentMoveSpeed < _maxMoveSpeed)
                {
                    _currentMoveSpeed += _currentMoveSpeed * _moveSpeedAccelerationMultiplier * Time.deltaTime;
                }
            }
            else
            {
                _currentMoveSpeed = _minMoveSpeed;
            }
        }

        private void HandleRotation()
        {
            if (_inputActions.Camera.ToggleRotate.IsPressed())
            {
                _rotateInput = _inputActions.Camera.Rotate.ReadValue<Vector2>();
                if (_rotateInput.x != 0f)
                {
                    transform.Rotate(Vector3.up * _rotateInput.x * _rotateSpeed * Time.deltaTime);
                }
                if (_rotateInput.y != 0f)
                {
                    _xRot = Mathf.Clamp(_xRot - (_rotateInput.y * _rotateSpeed * Time.deltaTime), _minAngle, _maxAngle);
                    _rotationRoot.localRotation = Quaternion.Euler(_xRot, 0f, 0f);
                }
            }
        }

        private void HandleZooming()
        {
            if (_currentZoom != _requiredZoom)
            {
                _currentZoom = Mathf.MoveTowards(_currentZoom, _requiredZoom, (Mathf.Abs(_requiredZoom - _currentZoom) + _zoomSpeed) * Time.deltaTime);
                _zoomRoot.localPosition = Vector3.back * _currentZoom;
            }
        }

        private void HandleCollision()
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
            _collisionCurrentOffset.z = Mathf.Lerp(_collisionRoot.localPosition.z, _collisionRequiredOffset, _collisionAvoidanceSpeed * Time.deltaTime);
            _collisionRoot.localPosition = _collisionCurrentOffset;
        }

        private void OnZoomPerfomed(float value)
        {
            if (value > 0f)
            {
                _requiredZoom = Mathf.Clamp(_requiredZoom - _zoomStep, _zoomMin, _zoomMax);
            }
            else if (value < 0f)
            {
                _requiredZoom = Mathf.Clamp(_requiredZoom + _zoomStep, _zoomMin, _zoomMax);
            }
        }

        public void SetTarget(PawnController target)
        {
            if (target != null)
            {
                _target = target;
                _followPawn = true;
            }
            else
            {
                ResetTarget();
            }
        }

        public void ResetTarget()
        {
            _target = null;
            _followPawn = false;
        }
    }
}