using UnityEngine;

namespace WinterUniverse
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInputActions _inputActions;
        private PawnController _pawn;

        public PawnController Pawn => _pawn;

        public void InitializeComponent()
        {
            _pawn = GameManager.StaticInstance.PrefabsManager.GetPawn(transform);
            _pawn.Create();
            _inputActions = new();
            _inputActions.Enable();
        }

        public void ResetComponent()
        {
            _inputActions.Disable();
        }

        public void OnTick(float deltaTime)
        {
            _pawn.OnTick(deltaTime);
        }
    }
}