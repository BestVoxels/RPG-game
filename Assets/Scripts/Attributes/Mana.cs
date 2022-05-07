using System;
using UnityEngine;

namespace RPG.Attributes
{
    public class Mana : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [Min(1f)]
        [SerializeField] private float _regenerateSpeed = 10f;
        #endregion



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

        private void Update()
        {
            RegenerateMana();
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



        #region --Methods-- (Custom PRIVATE)
        private void RegenerateMana()
        {
            if (ManaPoints >= MaxManaPoints) return;

            ManaPoints += Time.deltaTime * _regenerateSpeed;
            ManaPoints = Mathf.Clamp(ManaPoints, 0f, MaxManaPoints);

            OnManaPointsUpdated?.Invoke();
        }
        #endregion
    }
}