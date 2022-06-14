using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
using RPG.Attributes;
using RPG.SceneManagement;

namespace RPG.Control
{
    public class Respawner : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private bool _respawnOnDeadAtStart = false;

        [Header("Respawn Settings")]
        [SerializeField] private Transform _spawnLocation;
        [SerializeField] private float _waitTimer = 2f;
        [Tooltip("How much health will restore to player in percentage")]
        [Range(1f, 100f)]
        [SerializeField] private float _healthRegeneratePercentage = 40f;

        [Header("Settings for Player's Cinemachine (so it won't shake when warp)")]
        [Tooltip("Player GameObject Transform")]
        [SerializeField] private Transform _playerTransform;
        [Tooltip("CinemachineBrain that has ActiveCamera follows the player")]
        [SerializeField] private CinemachineBrain _playerCinemachineBrain;

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

        private void OnEnable()
        {
            _health.OnHealthLoadSetup += RespawnAtStart;
        }

        private void OnDisable()
        {
            _health.OnHealthLoadSetup -= RespawnAtStart;
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private IEnumerator RespawnRoutine()
        {
            yield return new WaitForSeconds(_waitTimer);

            yield return Transition.Instance.StartTransition(_transitionType, _startTransitionSpeed);
            ResetPlayer();
            yield return Transition.Instance.EndTransition(_transitionType, _endTransitionSpeed);

            _current = null;
        }

        private void ResetPlayer()
        {
            Vector3 warpAmount = _spawnLocation.position - _playerTransform.position;

            _navMeshAgent.Warp(_spawnLocation.position);
            _health.RegenerateHealth(_healthRegeneratePercentage);

            // Make Camera not Shaking when Warp by warn Cinemachine that we are about to warp by large amount
            if (_playerCinemachineBrain != null && _playerCinemachineBrain.ActiveVirtualCamera.Follow == _playerTransform) // Make sure this is Player Follower Camera
                _playerCinemachineBrain.ActiveVirtualCamera.OnTargetObjectWarped(_playerTransform, warpAmount);
        }
        #endregion



        #region --Methods-- (Subscriber)
        public void RespawnAtStart()
        {
            if (_health.IsDead && _respawnOnDeadAtStart)
            {
                Respawn(); // Need to do this cuz when save & exit while player is dead, nothing will trigger Respawn
            }
        }
        #endregion



        #region --Methods-- (Subscriber) ~UnityEvent~
        public void Respawn()
        {
            if (_current != null) return;

            _current = StartCoroutine(RespawnRoutine());
        }
        #endregion
    }
}