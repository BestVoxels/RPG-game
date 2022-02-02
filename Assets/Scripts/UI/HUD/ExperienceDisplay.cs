using UnityEngine;
using TMPro;
using RPG.Stats;

namespace RPG.UI.HUD
{
    public class ExperienceDisplay : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private TMP_Text _experienceText;
        #endregion



        #region --Fields-- (In Class)
        private Experience _experience;
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
            _experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        private void OnEnable()
        {
            _experience.OnExperienceLoaded += UpdateExperienceDisplay;
            _experience.OnExperienceGained += UpdateExperienceDisplay;
        }

        private void Start()
        {
            UpdateExperienceDisplay();
        }

        private void OnDisable()
        {
            _experience.OnExperienceLoaded -= UpdateExperienceDisplay;
            _experience.OnExperienceGained -= UpdateExperienceDisplay;
        }
        #endregion



        #region --Methods-- (Subscriber)
        private void UpdateExperienceDisplay()
        {
            _experienceText.text = $"{_experience.ExperiencePoints}";
        }
        #endregion
    }
}