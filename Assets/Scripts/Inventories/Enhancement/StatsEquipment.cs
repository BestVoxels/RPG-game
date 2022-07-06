using System.Collections.Generic;
using RPG.Stats;

namespace RPG.Inventories.Enhancement
{
    /// <summary>
    /// An Enhance version of Equipment.cs is StatsEquipment.cs, for Checking StatsModifier on Equipable Items.
    /// Provides a store for the items equipped to a player. Items are stored by
    /// their equip locations.
    /// 
    /// This component should be placed on the GameObject tagged "Player".
    /// </summary>
    public class StatsEquipment : Equipment, IModifierProvider
    {
        #region --Methods-- (Interface)
        IEnumerable<float> IModifierProvider.GetAdditiveModifiers(StatType statType)
        {
            foreach (EquipLocation eachSlotLocation in GetAllPopulatedSlots())
            {
                IModifierProvider equipableItem = GetItemInSlot(eachSlotLocation) as IModifierProvider; // Only Get EquipableItem that has Stats by checking whether it implement IModifierProvider or not
                if (equipableItem == null) continue;

                foreach (float eachAdditiveModifier in equipableItem.GetAdditiveModifiers(statType))
                {
                    yield return eachAdditiveModifier;
                }
            }
        }

        IEnumerable<float> IModifierProvider.GetPercentageModifiers(StatType statType)
        {
            foreach (EquipLocation eachSlotLocation in GetAllPopulatedSlots())
            {
                IModifierProvider equipableItem = GetItemInSlot(eachSlotLocation) as IModifierProvider; // Only Get EquipableItem that has Stats by checking whether it implement IModifierProvider or not
                if (equipableItem == null) continue;

                foreach (float eachPercentageModifier in equipableItem.GetPercentageModifiers(statType))
                {
                    yield return eachPercentageModifier;
                }
            }
        }
        #endregion
    }
}