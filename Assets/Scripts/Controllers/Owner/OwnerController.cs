using UnityEngine;

namespace WinterUniverse
{
    public class OwnerController : BasicComponent
    {
        public PawnController Pawn { get; private set; }

        public override void InitializeComponent()
        {
            Pawn = GameManager.StaticInstance.PrefabsManager.GetPawn();
            GameManager.StaticInstance.PawnsManager.AddController(Pawn);
        }

        public void MoveTo(Vector3 position)
        {
            Pawn.Locomotion.SetDestination(position);
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
    }
}