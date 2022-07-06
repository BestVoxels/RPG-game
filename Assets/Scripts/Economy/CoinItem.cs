using UnityEngine;
using RPG.Inventories;

namespace RPG.Economy
{
    /// <summary>
    /// An inventory item that will be transfer to Coin Economy System of the collector. (Mechanism is at Coin.cs)
    /// </summary>
    /// <remarks>
    /// This class should be used as is. No need to have Subclasses OR itself class implementation.
    /// </remarks>
    [CreateAssetMenu(fileName = "Untitled Coin", menuName = "RPG/Game Item/New Coin (Economy)", order = 150)]
    public class CoinItem : InventoryItem
    {
    }
}