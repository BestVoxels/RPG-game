using System.Collections.Generic;
using System;
using UnityEngine;
using RPG.Inventories;
using RPG.Control;
using RPG.Core;
using RPG.Movement;

namespace RPG.Shops
{
    public class Shop : MonoBehaviour, IRaycastable
    {
        #region --Fields-- (Inspector)
        [SerializeField] private string _shopTitleName;
        [SerializeField] private StockItemConfig[] _stockItems;
        #endregion



        #region --Events-- (Delegate as Action)
        public event Action OnShopUpdated;
        #endregion



        #region --Fields-- (In Class)
        private ActionScheduler _actionScheduler;
        private Shopper _shopper;
        #endregion



        #region --Properties-- (With Backing Fields)
        public string ShopTitleName { get { return _shopTitleName; } }
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
            _actionScheduler = GameObject.FindWithTag("Player").GetComponent<ActionScheduler>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_shopper == null) return;

                _shopper.SetActiveShop(this);
                _actionScheduler.StopCurrentAction();

                _shopper = null;
            }
        }
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public IEnumerable<ShopItem> GetFilteredItems()
        {
            foreach (StockItemConfig eachStock in _stockItems)
            {
                yield return new ShopItem(eachStock.item, eachStock.initialStock, GetShopItemPrice(eachStock), 0);
            }
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
            print($"{item.GetDisplayName()} : {quantity}");
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private int GetShopItemPrice(StockItemConfig stockItem)
        {
            int defaultPrice = stockItem.item.GetPrice();
            float discountAmount = (defaultPrice / 100f) * (-stockItem.buyingDiscountPercentage); // negate so that positive percentage mean deduct out of defaultPrice & negative percentage mean add on to defaultPrice
            
            return (int)Math.Round(defaultPrice + discountAmount, MidpointRounding.AwayFromZero); //2.5 will be 3
        }
        #endregion



        #region --Methods-- (Interface)
        CursorType IRaycastable.GetCursorType()
        {
            return CursorType.Shop;
        }

        bool IRaycastable.HandleRaycast(PlayerController playerController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                playerController.GetComponent<Mover>().StartMoveAction(transform.position, 1f);

                _shopper = playerController.GetComponentInChildren<Shopper>();
            }

            return true;
        }
        #endregion



        #region --Classes-- (Custom PRIVATE)
        [System.Serializable]
        private class StockItemConfig
        {
            public InventoryItem item;
            public int initialStock;
            [Tooltip("Negative Value Mean on top on the product price, make it more expensive. Positive make it cheaper.")]
            [Range(-100f,100f)]
            public float buyingDiscountPercentage;
        }
        #endregion
    }
}