using UnityEngine;

namespace WinterUniverse
{
    public interface IInteractable
    {
        public Transform GetPoint();
        public float GetStopDistance();
        public string GetText();
        public bool CanInteract(OwnerController owner);
        public void OnInteract(OwnerController owner);
    }
}