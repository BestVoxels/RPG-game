using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private GameObject _targetToDestroy = null;
        #endregion



        #region --Methods-- (Built In)
        private void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                if (_targetToDestroy != null)
                {
                    Destroy(_targetToDestroy);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        #endregion
    }
}