using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnDetection : PawnComponent
    {
        [SerializeField] private float _detectionDelay = 2f;

        private Collider[] _detectedColliders;
        private float _detectionTime;

        public List<PawnController> DetectedEnemies { get; private set; }
        public List<PawnController> DetectedNeutrals { get; private set; }
        public List<PawnController> DetectedAllies { get; private set; }
        public List<IInteractable> DetectedInteractables { get; private set; }

        public override void InitializeComponent()
        {
            base.InitializeComponent();
            DetectedEnemies = new();
            DetectedNeutrals = new();
            DetectedAllies = new();
            DetectedInteractables = new();
        }

        public override void UpdateComponent()
        {
            base.UpdateComponent();
            if (_detectionTime >= _detectionDelay)
            {
                DetectedEnemies.Clear();
                DetectedNeutrals.Clear();
                DetectedAllies.Clear();
                DetectedInteractables.Clear();
                float distance;
                _detectedColliders = Physics.OverlapSphere(transform.position, _pawn.Status.StatsHolder.GetStat("View Distance").CurrentValue, GameManager.StaticInstance.LayersManager.DetectableMask);
                foreach (Collider collider in _detectedColliders)
                {
                    distance = Vector3.Distance(transform.position, collider.transform.position);
                    if (collider.TryGetComponent(out IInteractable interactable) && interactable.CanInteract(_pawn.Owner))
                    {
                        DetectedInteractables.Add(interactable);
                    }
                    else if (collider.TryGetComponent(out PawnController pawn) && pawn != _pawn && pawn.Status.StateHolder.CompareStateValue("Is Dead", false))
                    {
                        if (distance <= _pawn.Status.StatsHolder.GetStat("Hear Radius").CurrentValue || TargetIsVisible(pawn))
                        {
                            switch (_pawn.Faction.Config.GetState(pawn.Faction.Config))
                            {
                                case RelationshipState.Enemy:
                                    DetectedEnemies.Add(pawn);
                                    break;
                                case RelationshipState.Neutral:
                                    DetectedNeutrals.Add(pawn);
                                    break;
                                case RelationshipState.Ally:
                                    DetectedAllies.Add(pawn);
                                    break;
                            }
                        }
                    }
                }
                _pawn.Status.StateHolder.SetStateValue("Detected Enemy", DetectedEnemies.Count > 0);
                _pawn.Status.StateHolder.SetStateValue("Detected Neutral", DetectedNeutrals.Count > 0);
                _pawn.Status.StateHolder.SetStateValue("Detected Ally", DetectedAllies.Count > 0);
            }
            else
            {
                _detectionTime += Time.deltaTime;
            }
        }

        public bool TargetInRange(Transform t)
        {
            float distance = Vector3.Distance(transform.position, t.position);
            if (distance <= _pawn.Status.StatsHolder.GetStat("Hear Radius").CurrentValue)
            {
                return true;
            }
            else if (distance <= _pawn.Status.StatsHolder.GetStat("View Distance").CurrentValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TargetIsVisible(Transform t)
        {
            Vector3 direction = (t.position - _pawn.Animator.Eyes.position).normalized;
            if (Vector3.Angle(_pawn.Animator.Eyes.forward, direction) > _pawn.Status.StatsHolder.GetStat("View Angle").CurrentValue / 2f)
            {
                return false;
            }
            if (Physics.Linecast(_pawn.Animator.Eyes.position, t.position, GameManager.StaticInstance.LayersManager.ObstacleMask))
            {
                return false;
            }
            return true;
        }

        public bool TargetIsVisible(PawnController pawn)
        {
            Vector3 direction = (pawn.Animator.Body.position - _pawn.Animator.Eyes.position).normalized;
            if (Vector3.Angle(_pawn.Animator.Eyes.forward, direction) > _pawn.Status.StatsHolder.GetStat("View Angle").CurrentValue / 2f)
            {
                return false;
            }
            if (Physics.Linecast(_pawn.Animator.Eyes.position, pawn.Animator.Body.position, GameManager.StaticInstance.LayersManager.ObstacleMask))
            {
                return false;
            }
            return true;
        }

        public bool TargetIsEnemy(PawnController target)
        {
            return _pawn.Faction.Config.GetState(target.Faction.Config) == RelationshipState.Enemy;
        }

        public bool TargetIsNeutral(PawnController target)
        {
            return _pawn.Faction.Config.GetState(target.Faction.Config) == RelationshipState.Neutral;
        }

        public bool TargetIsAlly(PawnController target)
        {
            return _pawn.Faction.Config.GetState(target.Faction.Config) == RelationshipState.Ally;
        }

        public PawnController GetClosestEnemy()
        {
            PawnController closestPawn = null;
            float maxDistance = float.MaxValue;
            float distance;
            foreach (PawnController pawn in DetectedEnemies)
            {
                distance = Vector3.Distance(transform.position, pawn.transform.position);
                if (distance < maxDistance)
                {
                    maxDistance = distance;
                    closestPawn = pawn;
                }
            }
            return closestPawn;
        }

        public PawnController GetClosestNeutral()
        {
            PawnController closestPawn = null;
            float maxDistance = float.MaxValue;
            float distance;
            foreach (PawnController pawn in DetectedNeutrals)
            {
                distance = Vector3.Distance(transform.position, pawn.transform.position);
                if (distance < maxDistance)
                {
                    maxDistance = distance;
                    closestPawn = pawn;
                }
            }
            return closestPawn;
        }

        public PawnController GetClosestAlly()
        {
            PawnController closestPawn = null;
            float maxDistance = float.MaxValue;
            float distance;
            foreach (PawnController pawn in DetectedAllies)
            {
                distance = Vector3.Distance(transform.position, pawn.transform.position);
                if (distance < maxDistance)
                {
                    maxDistance = distance;
                    closestPawn = pawn;
                }
            }
            return closestPawn;
        }
    }
}