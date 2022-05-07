using System;
using UnityEngine;

namespace RPG.Attributes
{
    public class Mana : MonoBehaviour
    {
        #region --Events-- (Delegate as Action)
        public event Action OnManaPointsUpdated;
        #endregion



        #region --Properties-- (Auto)
        public float ManaPoints { get; private set; }
        public float MaxManaPoints { get; private set; } = 200;
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
            ManaPoints = MaxManaPoints;
        }
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public bool UseMana(float amount)
        {
            if (amount > ManaPoints) return false;

            ManaPoints -= amount;
            OnManaPointsUpdated?.Invoke();

            return true;
        }
        #endregion
    }
}