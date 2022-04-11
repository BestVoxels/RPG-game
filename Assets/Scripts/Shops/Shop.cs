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
            yield return new ShopItem(InventoryItem.GetFromID("f52bc728f5144-435a-a4bf-440f52bc728f"), 10, 59f, 0);
            yield return new ShopItem(InventoryItem.GetFromID("643aa476-f955-47fd-9edf-0b39f6c1fc28"), 10, 159f, 0);
            //yield return new ShopItem(InventoryItem.GetFromID("960b28d1-f08f-4c27-86a2-78d1212af28e"), 10, 99f, 0);
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
    }
}