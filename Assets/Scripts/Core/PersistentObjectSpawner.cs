using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private GameObject _persistentObjectPrefab;
        #endregion



        #region --Fields-- (In Class)
        private static bool _hasSpawned = false;
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
            if (_hasSpawned) return;

            SpawnPersistentObject();

            _hasSpawned = true;
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private void SpawnPersistentObject()
        {
            GameObject persistentObject = Instantiate(_persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
        #endregion
    }
}