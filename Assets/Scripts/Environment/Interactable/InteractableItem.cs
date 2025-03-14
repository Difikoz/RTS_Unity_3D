using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Rigidbody))]
    public class InteractableItem : InteractableBase
    {
        //private ItemConfig _item;
        //private int _amount = 1;
        //private GameObject _model;

        //public void Initialize(ItemStack stack)
        //{
        //    Initialize(stack.Item, stack.Amount);
        //}

        //public void Initialize(ItemConfig item, int amount = 1)
        //{
        //    _item = item;
        //    _amount = amount;
        //    _model = LeanPool.Spawn(_item.Model, transform);
        //}

        public override bool CanInteract(PawnController pawn)
        {
            return true;// _item != null && _amount > 0;
        }

        public override void Interact(PawnController pawn)
        {
            //pawn.Inventory.AddItem(_item, _amount);
            //LeanPool.Despawn(_model);
            LeanPool.Despawn(gameObject);
        }
    }
}