using UnityEngine;

namespace RPG.Core
{
    public class DestroyTarget : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private GameObject _targetToDestroy = null;
        #endregion
        


        #region --Methods-- (Animation Event)
        private void DestroyTargetGameObject()
        {
            Destroy(_targetToDestroy);
        }    
        #endregion
    }
}