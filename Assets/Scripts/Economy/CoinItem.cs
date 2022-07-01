using UnityEngine;
using RPG.Inventories;

namespace RPG.Economy
{
    /// <summary>
    /// An inventory item that will be transfer to Coin Economy System of the collector.
    /// </summary>
    /// <remarks>
    /// This class should be used as is. No need for Subclasses or any class itself implementation.
    /// </remarks>
    [CreateAssetMenu(fileName = "Untitled Coin", menuName = "RPG/Game Item/New Coin (Economy)", order = 150)]
    public class CoinItem : InventoryItem
    {
    }
}