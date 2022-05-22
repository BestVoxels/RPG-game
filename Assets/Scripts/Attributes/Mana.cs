using System;
using UnityEngine;
using RPG.Stats;
using RPG.Utils;
using RPG.Saving;

namespace RPG.Attributes
{
    public class Mana : MonoBehaviour, ISaveable
    {
        #region --Events-- (Delegate as Action)
        public event Action OnManaPointsUpdated;
        #endregion



        #region --Fields-- (In Class)
        private BaseStats _baseStats;
        private Experience _experience;
        #endregion



        #region --Properties-- (Auto)
        public AutoInit<float> ManaPoints { get; private set; }
        public AutoInit<float> MaxManaPoints { get; private set; }
        public AutoInit<float> ManaRegenRate { get; private set; }
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
            _baseStats = transform.root.GetComponentInChildren<BaseStats>();
            _experience = transform.root.GetComponentInChildren<Experience>();

            ManaPoints = new AutoInit<float>(GetMaxManaPoints);
            MaxManaPoints = new AutoInit<float>(GetMaxManaPoints);
            ManaRegenRate = new AutoInit<float>(GetManaRegenRate);
        }

        private void OnEnable()
        {
            if (_experience != null)
                _experience.OnExperienceLoadSetup += () => { UpdateMaxManaPoints(); UpdateManaRegenRate(); }; // see at Action declaration why this Action
            _baseStats.OnLevelUpSetup += () => { UpdateMaxManaPoints(); UpdateManaRegenRate(); }; // see at Action declaration why this Action
        }

        private void Update()
        {
            RegenerateMana();
        }

        private void OnDisable()
        {
            if (_experience != null)
                _experience.OnExperienceLoadSetup -= () => { UpdateMaxManaPoints(); UpdateManaRegenRate(); };
            _baseStats.OnLevelUpSetup -= () => { UpdateMaxManaPoints(); UpdateManaRegenRate(); };
        }
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public bool UseMana(float amount)
        {
            if (amount > ManaPoints.value) return false;
            
            ManaPoints.value -= amount;
            OnManaPointsUpdated?.Invoke();

            return true;
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private void RegenerateMana()
        {
            if (ManaPoints.value >= MaxManaPoints.value) return;
            
            ManaPoints.value += Time.deltaTime * ManaRegenRate.value;
            ManaPoints.value = Mathf.Clamp(ManaPoints.value, 0f, MaxManaPoints.value);

            OnManaPointsUpdated?.Invoke();
        }
        #endregion



        #region --Methods-- (Subscriber)
        private float GetMaxManaPoints() => _baseStats.GetMana();
        private float GetManaRegenRate() => _baseStats.GetManaRegenRate();

        private void UpdateMaxManaPoints() => MaxManaPoints.value = GetMaxManaPoints();
        private void UpdateManaRegenRate() => ManaRegenRate.value = GetManaRegenRate();
        #endregion



        #region --Methods-- (Interface)
        object ISaveable.CaptureState()
        {
            return ManaPoints.value;
        }

        void ISaveable.RestoreState(object state)
        {
            ManaPoints.value = (float)state;

            OnManaPointsUpdated?.Invoke();
        }
        #endregion
    }
}