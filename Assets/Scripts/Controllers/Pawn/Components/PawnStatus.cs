using UnityEngine;

namespace WinterUniverse
{
    public class PawnStatus : MonoBehaviour
    {
        private PawnController _pawn;

        public float ForwardVelocity;
        public float RightVelocity;
        public float TurnVelocity;
        public bool IsPerfomingAction;
        public bool IsMoving;
        public bool IsSprinting;
        public bool IsDead;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
        }

        public void OnTick(float deltaTime)
        {

        }
    }
}