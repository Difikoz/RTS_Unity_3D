using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Rigidbody))]
    public class InteractableChest : InteractableBase
    {
        [SerializeField] private SwitchableAnimatedObject _chest;
        //[SerializeField] private List<ItemStack> _stacks = new();

        public override bool CanInteract(PawnController pawn)
        {
            return true;// _stacks.Count > 0;
        }

        public override void Interact(PawnController pawn)
        {
            _chest.SwitchOn();
            //foreach (ItemStack stack in _stacks)
            //{
            //    pawn.Inventory.AddItem(stack);
            //}
            //_stacks.Clear();
        }
    }
}