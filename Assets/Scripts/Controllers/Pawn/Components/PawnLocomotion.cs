using UnityEngine;
using UnityEngine.AI;

namespace WinterUniverse
{
    public class PawnLocomotion : PawnComponent
    {
        [SerializeField, Range(0.5f, 4f)] private float _acceleration = 2f;
        [SerializeField, Range(0.5f, 4f)] private float _deceleration = 4f;
        [SerializeField, Range(0f, 720f)] private float _turnSpeed = 180f;
        [SerializeField] private float _turnAngle = 45f;

        public IInteractable Interactable { get; private set; }
        public Vector3 MoveDirection { get; private set; }
        public Vector3 MoveVelocity { get; private set; }
        public float ForwardVelocity { get; private set; }
        public float RightVelocity { get; private set; }
        public float TurnVelocity { get; private set; }
        public float RemainingDistance { get; private set; }
        public bool ReachedDestination { get; private set; }

        public override void UpdateComponent()
        {
            base.UpdateComponent();
            if (Interactable != null)
            {
                if (ReachedDestination)
                {
                    if (Interactable.CanInteract(_pawn.Owner))
                    {
                        Interactable.OnInteract(_pawn.Owner);
                    }
                    StopMovement();
                }
                else
                {
                    _pawn.Agent.destination = Interactable.GetPoint().position;
                }
            }
            RemainingDistance = Vector3.Distance(transform.position, _pawn.Agent.destination);
            if (ReachedDestination)
            {
                if (RemainingDistance > _pawn.Agent.stoppingDistance)
                {
                    ReachedDestination = false;
                }
                else
                {
                    MoveVelocity = Vector3.MoveTowards(MoveVelocity, Vector3.zero, _deceleration * Time.deltaTime);
                }
            }
            else
            {
                if (RemainingDistance < _pawn.Agent.stoppingDistance)
                {
                    ReachedDestination = true;
                }
                else
                {
                    MoveDirection = (_pawn.Agent.steeringTarget - transform.position).normalized;
                    if (MoveDirection != Vector3.zero)
                    {
                        transform.rotation = Quaternion.RotateTowards(_pawn.transform.rotation, Quaternion.LookRotation(MoveDirection), _turnSpeed * Time.deltaTime);
                    }
                    if (_pawn.Status.StateHolder.CompareStateValue("Is Sprinting", true))
                    {
                        MoveDirection *= 2f;
                    }
                    MoveVelocity = Vector3.MoveTowards(MoveVelocity, MoveDirection, _acceleration * Time.deltaTime);
                }
            }
            ForwardVelocity = Vector3.Dot(MoveVelocity, transform.forward);
            RightVelocity = Vector3.Dot(MoveVelocity, transform.right);
            TurnVelocity = Vector3.SignedAngle(transform.forward, MoveDirection, Vector3.up) / _turnAngle;
            _pawn.Status.StateHolder.SetStateValue("Is Moving", MoveVelocity.magnitude > 0f);
        }

        public void SetDestination(IInteractable target)
        {
            Interactable = target;
            _pawn.Agent.destination = Interactable.GetPoint().position;
            _pawn.Agent.stoppingDistance = Interactable.GetStopDistance();
        }

        public void SetDestination(Vector3 position, float stopDistance = 0f)
        {
            if (_pawn.Status.StateHolder.CompareStateValue("Is Perfoming Action", true))
            {
                return;
            }
            Interactable = null;
            if (stopDistance > 0f)
            {
                _pawn.Agent.stoppingDistance = stopDistance;
            }
            else
            {
                _pawn.Agent.stoppingDistance = _pawn.Agent.radius;
            }
            if (NavMesh.SamplePosition(position, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                _pawn.Agent.destination = hit.position;
            }
        }

        public void StopMovement()
        {
            Interactable = null;
            _pawn.Agent.destination = transform.position;
            _pawn.Agent.stoppingDistance = _pawn.Agent.radius;
            ReachedDestination = true;
        }

        private void OnDrawGizmos()
        {
            if (_pawn != null && _pawn.Agent != null && _pawn.Agent.hasPath)
            {
                for (int i = 0; i < _pawn.Agent.path.corners.Length - 1; i++)
                {
                    Debug.DrawLine(_pawn.Agent.path.corners[i], _pawn.Agent.path.corners[i + 1], Color.blue);
                }
            }
        }
    }
}