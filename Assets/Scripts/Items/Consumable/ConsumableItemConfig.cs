using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Consumable", menuName = "Winter Universe/Item/New Consumable")]
    public class ConsumableItemConfig : ItemConfig
    {
        [SerializeField] private List<EffectCreator> _effects = new();

        public List<EffectCreator> Effects => _effects;

        private void OnValidate()
        {
            _itemType = ItemType.Consumable;
        }

        public override void Use(PawnController pawn, bool fromInventory = true)
        {
            if (pawn.Status.StateHolder.CompareStateValue("Is Perfoming Action", true))
            {
                return;
            }
            if (!fromInventory || (fromInventory && pawn.Inventory.RemoveItem(this)))
            {
                //if (_playAnimationOnUse)
                //{
                //    pawn.Animator.PlayAction(_animationOnUse);
                //}
                pawn.Effects.ApplyEffects(_effects, null);
            }
        }
    }
}