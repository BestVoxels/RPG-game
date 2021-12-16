using UnityEngine;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private float _chaseDistance = 5f;
        #endregion



        #region --Fields-- (In Class)
        private Transform _player;
        private Fighter _fighter;
        private Health _health;
        #endregion



        #region --Methods-- (Built In)
        private void Start()
        {
            _player = GameObject.FindWithTag("Player").transform;
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
        }

        private void Update()
        {
            if (_health.IsDead) return;

            if (IsInChaseRange() && _fighter.CanAttack(_player.gameObject))
            {
                _fighter.Attack(_player.gameObject);
            }
            else
            {
                _fighter.CancelAttack();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _chaseDistance);
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private bool IsInChaseRange() => Vector3.Distance(transform.position, _player.position) < _chaseDistance;
        #endregion
    }
}