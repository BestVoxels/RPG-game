using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
    public class Mover : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        #endregion



        #region --Fields-- (In Class)
        private NavMeshAgent _agent;
        private Animator _animator;
        #endregion



        #region --Methods-- (Built In)
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            AnimateCharacter();
        }
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public void MoveTo(Vector3 destination)
        {
            _agent.isStopped = false;
            _agent.SetDestination(destination);
        }

        public void Stop()
        {
            _agent.isStopped = true;
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private void AnimateCharacter()
        {
            Vector3 globalVelocity = _agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(globalVelocity); // Convert Velocity from Global into Local, velocity relative to player point of view. (this case localVelocity & globalVelocity return same magnitude)

            float forwardSpeed = localVelocity.z; // We could use .magnitude BUT it just speed in all direction, also count when move left, right. LOOK MORE NATURAL with .z which only takes forward speed

            _animator.SetFloat("MoveSpeed", forwardSpeed);
        }
        #endregion
    }
}