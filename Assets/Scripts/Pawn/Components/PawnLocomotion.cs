using UnityEngine;
using UnityEngine.AI;

namespace WinterUniverse
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class PawnLocomotion : MonoBehaviour
    {
        [SerializeField, Range(0.5f, 4f)] private float _acceleration = 2f;
        [SerializeField, Range(0.5f, 4f)] private float _deceleration = 2f;
        [SerializeField, Range(0.5f, 4f)] private float _turnSpeed = 2f;
        [SerializeField] private float _turnAngle = 45f;

        private PawnController _pawn;
        private NavMeshAgent _agent;
        private CapsuleCollider _collider;
        [SerializeField] private Transform _followTarget;
        [SerializeField] private Vector3 _moveDirection;
        [SerializeField] private Vector3 _moveVelocity;
        private float _stopDistance;

        public bool ReachedDestination => !_agent.hasPath;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _agent = GetComponent<NavMeshAgent>();
            _collider = GetComponent<CapsuleCollider>();
            ResetFollowTarget();
        }

        public void OnTick(float deltaTime)
        {
            if (_agent.hasPath)
            {
                if (_followTarget != null)
                {
                    _agent.destination = _followTarget.position;
                }
                _moveDirection = (_agent.steeringTarget - transform.position).normalized;
                if (_moveDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(_pawn.transform.rotation, Quaternion.LookRotation(_moveDirection), _turnSpeed * deltaTime);
                }
                if (_pawn.Status.IsSprinting)
                {
                    _moveDirection *= 2f;
                }
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, _moveDirection, _acceleration * deltaTime);
                if (_agent.remainingDistance < _stopDistance)
                {
                    StopMovement();
                }
            }
            else
            {
                if (_followTarget != null)
                {
                    _moveDirection = (_followTarget.position - transform.position).normalized;
                    if (Vector3.Distance(transform.position, _followTarget.position) > _stopDistance)
                    {
                        _agent.destination = _followTarget.position;
                    }
                }
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, Vector3.zero, _deceleration * deltaTime);
            }
            _pawn.Status.ForwardVelocity = Vector3.Dot(_moveVelocity, transform.forward);
            _pawn.Status.RightVelocity = Vector3.Dot(_moveVelocity, transform.right);
            _pawn.Status.TurnVelocity = Vector3.SignedAngle(transform.forward, _moveDirection, transform.up) / _turnAngle;
            _pawn.Status.IsMoving = _moveVelocity.magnitude > 0f;
        }

        public void SetDestination(Vector3 position, bool sprint = true)
        {
            ResetFollowTarget();
            if (_pawn.Status.IsPerfomingAction)
            {
                return;
            }
            if (NavMesh.SamplePosition(position, out NavMeshHit hit, 25f, NavMesh.AllAreas))
            {
                _agent.destination = hit.position;
                _pawn.Status.IsSprinting = sprint;
            }
        }

        public void StopMovement()
        {
            _agent.ResetPath();
        }

        public void SetFollowTarget(Transform target, bool sprint = true, float stopDistance = 0f)
        {
            if (target != null)
            {
                _followTarget = target;
                _pawn.Status.IsSprinting = sprint;
                if (stopDistance > 0f)
                {
                    _stopDistance = stopDistance;
                }
                else
                {
                    _stopDistance = _agent.radius;
                }
                _agent.destination = _followTarget.position;
            }
            else
            {
                ResetFollowTarget();
            }
        }

        public void ResetFollowTarget()
        {
            _followTarget = null;
            _stopDistance = _agent.radius;
            StopMovement();
        }

        private void OnDrawGizmos()
        {
            if (_agent != null && _agent.hasPath)
            {
                for (int i = 0; i < _agent.path.corners.Length - 1; i++)
                {
                    Debug.DrawLine(_agent.path.corners[i], _agent.path.corners[i + 1], Color.blue);
                }
            }
        }
    }
}