using UnityEngine;

namespace WinterUniverse
{
    public class PawnComponent : BasicComponent
    {
        protected PawnController _pawn;

        public override void InitializeComponent()
        {
            _pawn = GetComponent<PawnController>();
        }
    }
}