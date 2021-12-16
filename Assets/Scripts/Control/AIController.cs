using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private float _chaseDistance = 5f;
        #endregion



        #region --Fields-- (In Class)
        private Transform _target;
        #endregion



        #region --Methods-- (Built In)
        private void Start()
        {
            _target = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            if (IsInChaseRange())
            {
                print($"{transform.name} will chase PLAYER!!!");
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _chaseDistance);
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private bool IsInChaseRange() => Vector3.Distance(transform.position, _target.position) < _chaseDistance;
        #endregion
    }
}