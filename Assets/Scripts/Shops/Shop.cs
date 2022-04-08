using System.Collections.Generic;
using System;
using UnityEngine;
using RPG.Inventories;

namespace RPG.Shops
{
    public class Shop : MonoBehaviour
    {
        #region --Events-- (Delegate as Action)
        public event Action OnShopUpdated;
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public IEnumerable<ShopItem> GetFilteredItems()
        {
            return null;
        }

        public void SelectFilter(ItemCategory itemCategory)
        {

        }

        public ItemCategory GetCurrentFilter()
        {
            return ItemCategory.None;
        }

        public void SelectShopMode(bool isBuying)
        {

        }

        public bool IsBuyingMode()
        {
            return true;
        }

        public bool CanTransact()
        {
            return true;
        }

        public void ConfirmTransaction()
        {

        }

        public float GetTransactionTotal()
        {
            return 0f;
        }

        public void AddToTransaction(InventoryItem item, int quantity)
        {

        }
        #endregion



        #region --Classes-- (Custom PUBLIC)
        public class ShopItem
        {
            public InventoryItem item;
            public int availability;
            public float price;
            public int quantityInTransaction;
        }
        #endregion
    }
}