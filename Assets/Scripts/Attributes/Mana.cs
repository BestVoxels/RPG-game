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
        #endregion



        #region --Properties-- (Auto)
        public AutoInit<float> ManaPoints { get; private set; }
        #endregion



        #region --Properties-- (With Backing Fields)
        public float MaxManaPoints { get { return _baseStats.GetMana(); } }
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
            _baseStats = transform.root.GetComponentInChildren<BaseStats>();

            ManaPoints = new AutoInit<float>(GetInitialMana);
        }

        private void Update()
        {
            RegenerateMana();
        }

        // Don't need to do _baseStats.OnLevelUp subscription like TraitStore.cs/Health.cs to update UI display because Update() already calls RegenerateMana()
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
            if (ManaPoints.value >= MaxManaPoints) return;

            ManaPoints.value += Time.deltaTime * _baseStats.GetManaRegenRate();
            ManaPoints.value = Mathf.Clamp(ManaPoints.value, 0f, MaxManaPoints);

            OnManaPointsUpdated?.Invoke();
        }
        #endregion



        #region --Methods-- (Subscriber)
        private float GetInitialMana() => MaxManaPoints;
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