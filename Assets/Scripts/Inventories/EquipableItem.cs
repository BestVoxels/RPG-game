using UnityEngine;
using RPG.Utils.Core;

namespace RPG.Inventories
{
    /// <summary>
    /// An inventory item that can be equipped to the player. Weapons could be a
    /// subclass of this.
    /// </summary>
    [CreateAssetMenu(fileName = "Untitled (Equipable)", menuName = "RPG/Game Item/New (Equipable)", order = 100)]
    public class EquipableItem : InventoryItem
    {
        #region --Fields-- (Inspector)
        [Tooltip("Where are we allowed to put this item.")]
        [SerializeField] private EquipLocation _allowedEquipLocation = EquipLocation.Weapon;
        [Tooltip("Condition For Equipping this item")]
        [SerializeField] private Condition _condition;
        #endregion



        #region --Properties-- (With Backing Fields)
        public EquipLocation AllowedEquipLocation { get => _allowedEquipLocation; }
        #endregion



        #region --Methods-- (Custom PUBLIC)
        /// <summary>
        /// Checking for EquipLocation & Condition of this item. Whether it satisfy of not.
        /// </summary>
        /// <param name="equipLocation">Slot Equip Location</param>
        /// <param name="gameObject">GameObject that contains PredicateEvaluator. (usually it's Player GameObject, can also be Player's Root GameObject)</param>
        /// <returns>
        /// Ture if it can Equip / False otherwise
        /// </returns>
        public bool CanEquip(EquipLocation equipLocation, GameObject gameObject)
        {
            if (equipLocation != _allowedEquipLocation) return false;
            if (!_condition.Check(gameObject.GetComponentsInChildren<IPredicateEvaluator>())) return false; // Get from Player GameObject (Ex-TraitStore.cs)

            return true;
        }
        #endregion
    }
}