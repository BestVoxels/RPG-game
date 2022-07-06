using UnityEngine;
using RPG.Saving;
using System;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        #region --Events-- (Delegate as Action)
        public event Action OnExperienceGained;
        public event Action OnRefreshCurrentLevelOnly; // ONLY for calling RefreshCurrentLevel() from BaseStats

        public event Action OnExperienceLoadSetup; // Purpose : use for subscriber that will make effect on value for their fields, ex-RegenerateHealth() or UpdateMaxManaPoints(), mainly notify the one who uses old GetStat() value. So Should Not be used for Refreshing UI since order of subscribers, subscriber that effect the value might run after Refreshing UI subscriber.
        public event Action OnExperienceLoadDone; // Purpose : use for subscriber that will only refreshing UI, make no effect on value. So can guarantee no subscriber will make an effect on the value, so can use for Refreshing UI.
        #endregion



        #region --Properties-- (Auto)
        public float ExperiencePoints { get; private set; }
        #endregion



        #region --Methods-- (Built In)
        // ****TEMP**** CHEAT KEY
        private void Update()
        {
            if (Input.GetKey(KeyCode.E))
                GainExperience(Time.deltaTime * 500f);
        }
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public void GainExperience(float experience)
        {
            ExperiencePoints += experience;
            OnExperienceGained?.Invoke();
        }
        #endregion



        #region --Methods-- (Interface)
        object ISaveable.CaptureState()
        {
            return ExperiencePoints;
        }

        void ISaveable.RestoreState(object state) // Even this RestoreState Get Called as first or last, All UI still work fine, Because of other classes also update UI again once they are loaded (check at UIDisplayManager for all the subscription).
        {
            ExperiencePoints = (float)state;
            OnRefreshCurrentLevelOnly?.Invoke();

            OnExperienceLoadSetup?.Invoke();
            OnExperienceLoadDone?.Invoke();
        }
        #endregion
    }
}