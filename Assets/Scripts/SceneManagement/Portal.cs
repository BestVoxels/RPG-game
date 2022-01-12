using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        private enum DestinationIdentifier
        {
            A,
            B,
            C,
            D,
            E
        }



        #region --Fields-- (Inspector)
        [SerializeField] private int _sceneIndexToLoad = -1;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private DestinationIdentifier _destination;

        [Header("Transition")]
        [SerializeField] private Transition.Types _transitionType = Transition.Types.CircleWipe;
        [Tooltip("When Start Loading to Other Scene (1 = normal speed)")]
        [SerializeField] private float _startTransitionSpeed = 1f;
        [Tooltip("When End Loading at Other Scene (1 = normal speed)")]
        [SerializeField] private float _endTransitionSpeed = 1f;
        #endregion



        #region --Methods-- (Built In)
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(LoadLevelWithTransition());
            }
        }
        #endregion



        #region --Methods-- (Custom PRIVATE) ~Coroutines~
        private IEnumerator LoadLevelWithTransition()
        {
            if (_sceneIndexToLoad < 0)
            {
                Debug.LogError("Please Set SceneIndexToLoad First!");
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            // StartingTransition
            yield return Transition.Instance.StartTransition(_transitionType, _startTransitionSpeed);

            SavingWrapper.Instance.Save();

            // StartLoadingLevel Once Transition is Done
            yield return Transition.Instance.LoadAsynchronously(_sceneIndexToLoad);

            SavingWrapper.Instance.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            SavingWrapper.Instance.Save();

            // EndingTransition Once Level is Loaded
            yield return Transition.Instance.EndTransition(_transitionType, _endTransitionSpeed);
            
            Destroy(gameObject);
        }
        #endregion



        #region --Methods-- (Custom PRIVATE) ~Utilities~
        private Portal GetOtherPortal()
        {
            Portal[] portals = FindObjectsOfType<Portal>();

            foreach (Portal each in portals)
                if (each != this && each._destination == _destination)
                    return each;

            return null;
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal._spawnPoint.position); // No Conflict with NavMeshAgent by setting position this way
            player.transform.rotation = otherPortal._spawnPoint.rotation;
        }
        #endregion
    }
}