using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private DamageText _damageTextPrefab = null;
        #endregion



        #region --Methods-- (Built In)
        private void Start()
        {
            Spawn(1f);
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private void Spawn(float damageAmount)
        {
            DamageText instance = Instantiate<DamageText>(_damageTextPrefab, transform);
        }
        #endregion
    }
}