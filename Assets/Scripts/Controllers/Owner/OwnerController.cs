using UnityEngine;

namespace WinterUniverse
{
    public abstract class OwnerController : BasicComponent
    {
        public PawnController Pawn { get; private set; }

        public override void InitializeComponent()
        {
            Pawn = GameManager.StaticInstance.PrefabsManager.GetPawn();
            Pawn.ChangeOwner(this);
            GameManager.StaticInstance.PawnsManager.AddController(Pawn);
        }

        public void MoveTo(Vector3 position)
        {
            Pawn.Locomotion.SetDestination(position);
        }

        public void Chase(PawnController pawn)
        {
            Pawn.Combat.SetTarget(pawn);
            Pawn.Locomotion.SetDestination((IInteractable)pawn);
        }

        public void StopMovement()
        {
            Pawn.Locomotion.StopMovement();
        }

        public void ToggleSprint()
        {
            Pawn.Status.StateHolder.ToggleStateValue("Is Sprinting");
        }

        public void ToggleSprint(bool enabled)
        {
            Pawn.Status.StateHolder.SetStateValue("Is Sprinting", enabled);
        }

        public abstract void OnPawnChased(OwnerController owner);
        public abstract void OnSettlementEntered(Settlement settlement);
    }
}