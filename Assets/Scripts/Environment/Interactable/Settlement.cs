using UnityEngine;

namespace WinterUniverse
{
    public class Settlement : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform _enterPoint;

        public Transform GetPoint()
        {
            return _enterPoint;
        }

        public float GetStopDistance()
        {
            return 0f;
        }

        public string GetText()
        {
            return "Enter Settlement";
        }

        public bool CanInteract(OwnerController owner)
        {
            return true;
        }

        public void OnInteract(OwnerController owner)
        {
            owner.OnSettlementEntered(this);
        }
    }
}