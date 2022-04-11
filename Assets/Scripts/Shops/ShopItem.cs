using RPG.Inventories;

namespace RPG.Shops
{
    public class ShopItem
    {
        public InventoryItem item;
        public int availability;
        public float price;
        public int quantityInTransaction;



        #region --Constructors-- (PUBLIC)
        public ShopItem(InventoryItem item, int availability, float price, int quantityInTransaction)
        {
            this.item = item;
            this.availability = availability;
            this.price = price;
            this.quantityInTransaction = quantityInTransaction;
        }
        #endregion
    }
}