using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnInteraction : MonoBehaviour
    {
        public Action OnTargetChanged;
        public Action OnInteractablesChanged;

        private PawnController _pawn;
        [SerializeField] private PawnController _target;
        [SerializeField] private List<InteractableBase> _interactables = new();
        [SerializeField] private Vector3 _directionToTarget;
        [SerializeField] private float _distanceToTarget;
        [SerializeField] private float _angleToTarget;

        public PawnController Target => _target;
        public Vector3 DirectionToTarget => _directionToTarget;
        public float DistanceToTarget => _distanceToTarget;
        public float AngleToTarget => _angleToTarget;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
        }

        public void OnTick(float deltaTime)
        {
            if (_target != null)
            {
                if (_target.Status.IsDead)
                {
                    ResetTarget();
                }
                if (_target != _pawn)
                {
                    _directionToTarget = (_target.transform.position - transform.position).normalized;
                    _distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);
                    _angleToTarget = Vector3.SignedAngle(transform.forward, _directionToTarget, Vector3.up);
                }
            }
        }

        public void SetTarget(PawnController target)
        {
            if (target != null && !_pawn.Status.IsPerfomingAction)
            {
                _target = target;
                OnTargetChanged?.Invoke();
            }
            else
            {
                ResetTarget();
            }
        }

        public void ResetTarget()
        {
            _target = null;
            _directionToTarget = Vector3.zero;
            _distanceToTarget = 0f;
            _angleToTarget = 0f;
            _pawn.Locomotion.ResetFollowTarget();
            OnTargetChanged?.Invoke();
        }

        public void AddInteractable(InteractableBase interactable)
        {
            if (!_interactables.Contains(interactable))
            {
                _interactables.Add(interactable);
                OnInteractablesChanged?.Invoke();
            }
        }

        public void RemoveInteractable(InteractableBase interactable)
        {
            if (_interactables.Contains(interactable))
            {
                _interactables.Remove(interactable);
                OnInteractablesChanged?.Invoke();
            }
        }

        public void InteractWithTarget()
        {
            if (_target == null || _target == _pawn || _pawn.Status.IsPerfomingAction)
            {
                return;
            }
            // has interactable?
            // can interact?
            // interact
        }

        public void InteractWithInteractable()
        {
            if (_pawn.Status.IsPerfomingAction)
            {
                return;
            }
            if (_interactables.Count > 0)
            {
                InteractableBase interactable = GetClosestInteractable();
                if (interactable.CanInteract(_pawn))
                {
                    interactable.Interact(_pawn);
                }
            }
        }

        public InteractableBase GetClosestInteractable()
        {
            return _interactables[0];
        }

        public void FollowTarget(bool sprint = true)
        {
            if (_target == null || _target == _pawn || _pawn.Status.IsPerfomingAction)
            {
                return;
            }
            _pawn.Locomotion.SetFollowTarget(_target.transform, sprint, 6f);
        }

        public void AttackTarget(bool sprint = true)
        {
            if (_target == null || _target == _pawn || _pawn.Status.IsPerfomingAction)
            {
                return;
            }
            _pawn.Locomotion.SetFollowTarget(_target.transform, sprint, 2f);
        }
    }
}