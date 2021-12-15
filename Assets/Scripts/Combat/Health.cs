using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private float _health = 100f;
        #endregion



        #region --Fields-- (In Class)
        #endregion


        #region --Properties-- (With Backing Fields)
        #endregion



        #region --Properties-- (Auto)
        #endregion



        #region --Methods-- (Built In)
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public void TakeDamage(float damage)
        {
            _health = Mathf.Max(0f, _health - damage);
            print(_health);
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        #endregion
    }
}