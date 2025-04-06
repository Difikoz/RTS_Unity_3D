using UnityEngine;

namespace WinterUniverse
{
    public class ActionFighting : ActionBase
    {
        public override void OnStart()
        {
            base.OnStart();
            _npc.Chase(_npc.Pawn.Detection.GetClosestEnemy());
        }
    }
}