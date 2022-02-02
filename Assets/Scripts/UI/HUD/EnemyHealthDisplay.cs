using UnityEngine;
using TMPro;
using RPG.Attributes;
using RPG.Combat;

namespace RPG.UI.HUD
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private TMP_Text _healthText;
        #endregion



        #region --Fields-- (In Class)
        private Fighter _playerFighter;
        private Health _enemyHealth;
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
            _playerFighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            // This work different from other Display so didn't use Action since it get updated based on when player Point to the Enemy
            _enemyHealth = _playerFighter.GetTarget();

            if (_enemyHealth == null)
            {
                _healthText.text = $"N/A";
            }
            else
            {
                _healthText.text = $"{_enemyHealth.HealthPoints.value:N0}/{_enemyHealth.MaxHealthPoints:N0}";
                //_healthText.text = $"{_enemyHealth.GetPercentage():N0}%";
            }
        }
        #endregion
    }
}