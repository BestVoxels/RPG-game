namespace RPG.Inventories
{
    /// <summary>
    /// Can Add more Providers, but User is set in stone.
    /// Provider Examples
    ///  - Coin.cs, uses it all no remaining to put in inventory.
    ///  - Equipment.cs, when pickup several of daggers, might do auto equip for first dagger then the remaining put in inventory.
    /// 
    /// --Definition--
    /// Provider - class that inherit interface and provide body of method.
    /// User - class that call interface method. Inventory.cs, call interface method at AddToFirstEmptySlot()
    /// </summary>
    public interface IItemUsage
    {
        public int Use(InventoryItem item, int number);
    }
}