using System;
using System.Collections;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu(fileName = "Untitled Spawn Target Prefab Effect", menuName = "RPG/Game Item/Effects/New Spawn Target Prefab Effect", order = 130)]
    public class SpawnTargetPrefabEffect : EffectStrategy
    {
        #region --Fields-- (Inspector)
        [SerializeField] private GameObject _prefabToSpawn;
        [Range(1f, 100f)]
        [SerializeField] private float _destroyPrefabDelay = 3f;
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private IEnumerator DestroyPrefabDelay(GameObject objectToDestroy, Action<string> onFinished)
        {
            yield return new WaitForSeconds(_destroyPrefabDelay);

            Destroy(objectToDestroy);
            onFinished?.Invoke(name);
        }
        #endregion



        #region --Methods-- (Override)
        public override void StartEffect(AbilityData data, Action<string> onFinished)
        {
            GameObject spawnedInstance = Instantiate(_prefabToSpawn, data.TargetedPoint, Quaternion.identity);

            data.StartCoroutine( DestroyPrefabDelay(spawnedInstance, onFinished) );
        }
        #endregion
    }
}