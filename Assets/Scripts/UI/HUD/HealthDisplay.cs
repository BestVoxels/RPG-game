using UnityEngine;
using TMPro;
using RPG.Attributes;

namespace RPG.UI.HUD
{
    public class HealthDisplay : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private TMP_Text _healthText;
        #endregion



        #region --Fields-- (In Class)
        private Health _health;
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
            _health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.OnHealthChanged += UpdateHealthDisplay;
        }

        private void Start()
        {
            UpdateHealthDisplay();
        }

        private void OnDisable()
        {
            _health.OnHealthChanged -= UpdateHealthDisplay;
        }
        #endregion



        #region --Methods-- (Subscriber)
        private void UpdateHealthDisplay()
        {
            _healthText.text = $"{_health.HealthPoints.value:N0}/{_health.MaxHealthPoints:N0}";
            //_healthText.text = $"{_health.GetPercentage():N0}%";
        }
        #endregion
    }
}