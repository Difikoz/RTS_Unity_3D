using UnityEngine;
using UnityEngine.AI;

namespace WinterUniverse
{
    public class PawnLocomotion : MonoBehaviour
    {
        [SerializeField, Range(0.5f, 4f)] private float _acceleration = 2f;
        [SerializeField, Range(0.5f, 4f)] private float _deceleration = 2f;
        [SerializeField, Range(0.5f, 4f)] private float _turnSpeed = 2f;
        [SerializeField] private float _turnAngle = 45f;

        private PawnController _pawn;
        private NavMeshAgent _agent;
        private Vector3 _moveVelocity;
        private Vector3 _moveDirection;
        private bool _reachedDestination;

        public bool ReachedDestination => _reachedDestination;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _agent = GetComponentInChildren<NavMeshAgent>();
            _agent.updateRotation = false;
        }

        public void OnTick(float deltaTime)
        {
            if (!_reachedDestination)
            {
                _moveDirection = _agent.desiredVelocity.normalized;
                if (_moveDirection != Vector3.zero)
                {
                    _pawn.transform.rotation = Quaternion.Slerp(_pawn.transform.rotation, Quaternion.LookRotation(_moveDirection), _turnSpeed * deltaTime);
                }
                if (_pawn.Status.IsRunning)
                {
                    _moveDirection *= 2f;
                }
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, _moveDirection, _acceleration * deltaTime);
                if (_agent.remainingDistance < 0.5f)
                {
                    StopMovement();
                }
            }
            else
            {
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, Vector3.zero, _deceleration * deltaTime);
            }
            _pawn.Status.ForwardVelocity = Vector3.Dot(_moveVelocity, _pawn.transform.forward);
            _pawn.Status.RightVelocity = Vector3.Dot(_moveVelocity, _pawn.transform.right);
            _pawn.Status.TurnVelocity = Vector3.SignedAngle(_pawn.transform.forward, _moveDirection, _pawn.transform.up) / _turnAngle;
            _pawn.Status.IsMoving = _moveVelocity.magnitude > 0f;
            _agent.transform.localPosition = Vector3.zero;
        }

        public void SetDestination(Vector3 position, bool running = true)
        {
            StopMovement();
            if (NavMesh.SamplePosition(position, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            {
                _agent.SetDestination(hit.position);
                _reachedDestination = false;
                _pawn.Status.IsRunning = running;
            }
        }

        public void StopMovement()
        {
            _agent.ResetPath();
            _reachedDestination = true;
        }
    }
}