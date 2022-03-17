using System;
using System.Linq;

namespace WarHeroes.InventorySystem
{
    public class InventoryController<TEnum>
        where TEnum : Enum
    {
        public IInventory<TEnum>[] Inventories { get; private set; }

        public InventoryController(params IInventory<TEnum>[] inventories)
        {
            Inventories = inventories.ToArray();
        }

        public bool TryGetInventory<TInventory>(out TInventory inventory)
            where TInventory : IInventory<TEnum>
        {
            inventory = (TInventory)Inventories.FirstOrDefault(
                val => val is TInventory);

            if (inventory == null)
                return false;

            return true;
        }

        public bool TryGetInventoryOfType(TEnum inventoryType, out IInventory<TEnum> inventory)
        {
            inventory = Inventories.FirstOrDefault(
                    val => val.InventoryType.Equals(inventoryType));

            if (inventory == null)
                return false;

            return true;
        }
    }
}
