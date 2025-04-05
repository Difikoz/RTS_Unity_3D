using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnCombat : PawnComponent
    {
        public Action OnTargetChanged;

        public PawnController Target { get; private set; }
        public Vector3 DirectionToTarget { get; private set; }
        public float DistanceToTarget { get; private set; }
        public float AngleToTarget { get; private set; }

        public override void UpdateComponent()
        {
            base.UpdateComponent();
            if (Target != null)
            {
                if (Target.Status.StateHolder.CompareStateValue("Is Dead", true))
                {
                    ResetTarget();
                }
                else if (Target != _pawn)
                {
                    DirectionToTarget = (Target.transform.position - transform.position).normalized;
                    DistanceToTarget = Vector3.Distance(transform.position, Target.transform.position);
                    AngleToTarget = Vector3.SignedAngle(transform.forward, DirectionToTarget, Vector3.up);
                }
            }
        }

        public void SetTarget(PawnController target)
        {
            if (target != null)
            {
                Target = target;
                OnTargetChanged?.Invoke();
            }
            else
            {
                ResetTarget();
            }
        }

        public void ResetTarget()
        {
            Target = null;
            OnTargetChanged?.Invoke();
        }
    }
}