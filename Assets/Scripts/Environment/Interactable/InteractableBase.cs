using UnityEngine;

namespace WinterUniverse
{
    public abstract class InteractableBase : EventTriggerZone
    {
        protected override void OnPawnEnter(PawnController pawn)
        {
            pawn.Interaction.AddInteractable(this);
        }

        protected override void OnPawnExit(PawnController pawn)
        {
            pawn.Interaction.RemoveInteractable(this);
        }

        public abstract bool CanInteract(PawnController pawn);
        public abstract void Interact(PawnController pawn);
    }
}