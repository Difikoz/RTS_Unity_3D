using UnityEngine;

namespace WinterUniverse
{
    public interface IInteractable
    {
        public Transform GetPoint();
        public string GetText();
        public bool CanInteract(PawnController pawn);
        public void OnInteract(PawnController pawn);
    }
}