using UnityEngine;
using RPG.Attributes;

namespace RPG.UI.InGame
{
    public class DamageTextSpawner : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private DamageText _damageTextPrefab = null;
        #endregion



        #region --Fields-- (In Class)
        private Health _health;
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
            _health = GetComponentInParent<Health>();
        }

        private void OnEnable()
        {
            _health.OnTakeDamage += Spawn;
        }

        private void OnDisable()
        {
            _health.OnTakeDamage -= Spawn;
        }
        #endregion



        #region --Methods-- (Subscriber)
        private void Spawn(float damageAmount)
        {
            DamageText damageText = Instantiate<DamageText>(_damageTextPrefab, transform);
            damageText.text = $"{damageAmount}";
        }
        #endregion
    }
}