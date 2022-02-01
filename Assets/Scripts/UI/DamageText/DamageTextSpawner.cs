using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private DamageText _damageTextPrefab = null;
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public void Spawn(float damageAmount)
        {
            DamageText damageText = Instantiate<DamageText>(_damageTextPrefab, transform);
            damageText.text = $"{damageAmount}";
        }
        #endregion
    }
}