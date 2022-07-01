using System;
using UnityEngine;
using RPG.Saving;
using RPG.Inventories;

namespace RPG.Economy
{
    public class Coin : MonoBehaviour, ISaveable, IItemUsage
    {
        #region --Fields-- (Inspector)
        [Min(0)]
        [SerializeField] private int _starterCoinPoints = 500;
        #endregion



        #region --Events-- (Delegate as Action)
        public event Action OnCoinPointsUpdated;
        #endregion



        #region --Properties-- (Auto)
        public int CoinPoints { get; private set; }
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
            CoinPoints = _starterCoinPoints;
        }
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public void UpdateCoinPoints(int amount)
        {
            CoinPoints += amount;

            //CoinPoints = Mathf.Clamp(CoinPoints, 0, CoinPoints);

            OnCoinPointsUpdated?.Invoke();
        }
        #endregion



        #region --Methods-- (Interface)
        object ISaveable.CaptureState()
        {
            return CoinPoints;
        }

        void ISaveable.RestoreState(object state)
        {
            CoinPoints = (int)state;

            OnCoinPointsUpdated?.Invoke();
        }

        int IItemUsage.Use(InventoryItem item, int number)
        {
            var result = item as CoinItem;
            if (result == null) return number; // return full remaining, filter Only for CoinItem

            UpdateCoinPoints(number * item.GetPrice()); // how many CoinItem pickup x itself price

            return 0; // return 0 remaining
        }
        #endregion
    }
}