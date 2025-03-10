using UnityEngine;

namespace WinterUniverse
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private PawnController _pawn;
        [SerializeField] private float _followSpeed = 10f;
        [SerializeField] private float _rotateSpeed = 15f;

        private PlayerInputActions _inputActions;
        private Vector2 _cursorInput;
        private Vector2 _moveInput;
        private Vector2 _lookInput;
        private bool _rightClickPressed;
        private Ray _cameraRay;

        private void Awake()
        {
            _inputActions = new();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.Camera.Cursor.performed += ctx => _cursorInput = ctx.ReadValue<Vector2>();
            _inputActions.Camera.LeftClick.performed += ctx => OnInteractPressed();
            _inputActions.Camera.RightClick.performed += ctx => OnRightClickPressed();
            _inputActions.Camera.RightClick.canceled += ctx => OnRightClickUnpressed();
        }

        private void OnDisable()
        {
            _inputActions.Camera.Cursor.performed -= ctx => _cursorInput = ctx.ReadValue<Vector2>();
            _inputActions.Camera.LeftClick.performed -= ctx => OnInteractPressed();
            _inputActions.Camera.RightClick.performed -= ctx => OnRightClickPressed();
            _inputActions.Camera.RightClick.canceled -= ctx => OnRightClickUnpressed();
            _inputActions.Disable();
        }

        private void OnInteractPressed()
        {
            _cameraRay = Camera.main.ScreenPointToRay(_cursorInput);
            if (Physics.Raycast(_cameraRay, out RaycastHit hit, 1000f))
            {
                _pawn.Locomotion.SetDestination(hit.point);
            }
        }

        private void OnRightClickPressed()
        {
            _rightClickPressed = true;
        }

        private void OnRightClickUnpressed()
        {
            _rightClickPressed = false;
        }

        private void Update()
        {
            OnTick(Time.deltaTime);
        }

        public void OnTick(float deltaTime)
        {
            _moveInput = _inputActions.Camera.Move.ReadValue<Vector2>();
            // move
            transform.position = Vector3.Lerp(transform.position, _pawn.transform.position, _followSpeed * deltaTime);
            if (_rightClickPressed)
            {
                _lookInput = _inputActions.Camera.Look.ReadValue<Vector2>();
                // rotate
            }
        }
    }
}