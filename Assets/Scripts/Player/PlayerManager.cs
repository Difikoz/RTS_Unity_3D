using UnityEngine;

namespace WinterUniverse
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PawnController _pawn;

        private PlayerInputActions _inputActions;

        private void Awake()
        {
            _inputActions = new();
            _pawn.Create();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        private void Update()
        {
            _pawn.OnTick(Time.deltaTime);
        }
    }
}