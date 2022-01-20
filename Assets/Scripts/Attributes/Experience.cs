using UnityEngine;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private float _experiencePoints = 0;
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public void GainExperience(float experience)
        {
            _experiencePoints += experience;
        }
        #endregion
    }
}