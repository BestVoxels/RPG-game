using UnityEngine;
using TMPro;
using RPG.Stats;

namespace RPG.UI.HUD
{
    public class LevelDisplay : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private TMP_Text _levelText;
        #endregion



        #region --Fields-- (In Class)
        private BaseStats _baseStats;
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
            _baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void OnEnable()
        {
            _baseStats.OnLevelChanged += UpdateLevelDisplay;
        }

        private void Start()
        {
            UpdateLevelDisplay();
        }

        private void OnDisable()
        {
            _baseStats.OnLevelChanged -= UpdateLevelDisplay;
        }
        #endregion



        #region --Methods-- (Subscriber)
        private void UpdateLevelDisplay()
        {
            _levelText.text = $"{_baseStats.GetLevel()}";
        }
        #endregion
    }
}