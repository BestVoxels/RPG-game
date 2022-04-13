using UnityEngine;
using RPG.Inventories;

namespace RPG.Shops
{
    public class ShopItem
    {
        #region --Fields-- (In Class)
        private InventoryItem _item;
        private int _availability;
        private int _price;
        private int _quantityInTransaction;
        #endregion



        #region --Properties-- (With Backing Fields)
        public InventoryItem Item { get { return _item; } }
        public string Name { get { return _item.GetDisplayName(); } }
        public Sprite Icon { get { return _item.GetIcon(); } }
        public int Availability { get { return _availability; } }
        public int Price { get { return _price; } }
        public int QuantityInTransaction { get { return _quantityInTransaction; } }
        #endregion



        #region --Constructors-- (PUBLIC)
        public ShopItem(InventoryItem item, int availability, int price, int quantityInTransaction)
        {
            _item = item;
            _availability = availability;
            _price = price;
            _quantityInTransaction = quantityInTransaction;
        }
        #endregion
    }
}