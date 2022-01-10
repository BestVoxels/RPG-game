using UnityEngine;
using UnityEngine.SceneManagement;
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
        [Tooltip("When Start Loading to Other Scene")]
        [SerializeField] private float _startTransitionTime = 1f;
        [Tooltip("When Get Loaded on Other Scene")]
        [SerializeField] private float _endTransitionTime = 1f;
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



        #region --Methods-- (Custom PRIVATE)
        private IEnumerator LoadLevelWithTransition()
        {
            if (_sceneIndexToLoad < 0)
            {
                Debug.LogError("Please Set SceneIndexToLoad First!");
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            yield return Transition.Instance.StartTransition(_transitionType, _startTransitionTime);
            print("started");
            yield return LoadAsynchronously();
            print("loaded");
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return Transition.Instance.EndTransition(otherPortal._transitionType, _endTransitionTime);
            print("ended");
            Destroy(gameObject);
        }

        private IEnumerator LoadAsynchronously()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneIndexToLoad);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                print(progress);

                yield return null;
            }
            print("LOADED");
            yield break;
        }

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