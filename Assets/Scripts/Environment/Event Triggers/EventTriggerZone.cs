using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public abstract class EventTriggerZone : MonoBehaviour
    {
        protected List<PawnController> _enteredPawns = new();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PawnController pawn) && !_enteredPawns.Contains(pawn))
            {
                OnPawnEnter(pawn);
                _enteredPawns.Add(pawn);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PawnController pawn) && _enteredPawns.Contains(pawn))
            {
                _enteredPawns.Remove(pawn);
                OnPawnExit(pawn);
            }
        }

        protected abstract void OnPawnEnter(PawnController pawn);
        protected abstract void OnPawnExit(PawnController pawn);
    }
}