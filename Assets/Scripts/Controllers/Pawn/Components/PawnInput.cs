using UnityEngine;

namespace WinterUniverse
{
    public class PawnInput : MonoBehaviour
    {
        protected PawnController _pawn;

        public float ForwardVelocity;
        public float RightVelocity;
        public float TurnVelocity;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
        }
    }
}