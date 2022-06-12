using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using RPG.Attributes;
using RPG.SceneManagement;

namespace RPG.Control
{
    public class Respawner : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [Header("Respawn Settings")]
        [SerializeField] private Transform _spawnLocation;
        [SerializeField] private float _waitTimer = 2f;
        [Tooltip("How much health will restore to player in percentage")]
        [Range(1f, 100f)]
        [SerializeField] private float _healthRegeneratePercentage = 40f;

        [Header("Respawn Transition Settings")]
        [SerializeField] private Transition.Types _transitionType = Transition.Types.CircleWipe;
        [Tooltip("When Start Loading to Other Scene (1 = normal speed / 2 = faster / 0.5 = slower)")]
        [SerializeField] private float _startTransitionSpeed = 1f;
        [Tooltip("When End Loading at Other Scene (1 = normal speed / 2 = faster / 0.5 = slower)")]
        [SerializeField] private float _endTransitionSpeed = 1f;
        #endregion



        #region --Fields-- (In Class)
        private Health _health;
        private NavMeshAgent _navMeshAgent;

        private Coroutine _current;
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
            _health = transform.root.GetComponentInChildren<Health>();
            _navMeshAgent = transform.root.GetComponentInChildren<NavMeshAgent>();
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private IEnumerator Respawning()
        {
            yield return new WaitForSeconds(_waitTimer);

            yield return Transition.Instance.StartTransition(_transitionType, _startTransitionSpeed);
            _navMeshAgent.Warp(_spawnLocation.position);
            _health.RegenerateHealth(_healthRegeneratePercentage);
            yield return Transition.Instance.EndTransition(_transitionType, _endTransitionSpeed);

            _current = null;
        }
        #endregion



        #region --Methods-- (Subscriber) ~UnityEvent~
        public void Respawn()
        {
            if (_current != null) return;

            _current = StartCoroutine(Respawning());
        }
        #endregion
    }
}