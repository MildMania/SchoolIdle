namespace WarHeroes.InventorySystem
{
    public class Slot
    {
        public int ID { get; private set; }
        public Slot(int id)
        {
            ID = id;
        }

        public IEquippable Equippable { get; private set; }

        public bool IsEmpty => Equippable == null;

        public bool TryEquip(IEquippable equippable)
        {
            if (!IsEmpty)
                return false;

            if (!equippable.Equippable.TrySetEquipped(true))
                return false;

            Equippable = equippable;

            return true;
        }

        public bool TryUnequip()
        {
            if (IsEmpty)
                return false;

            Equippable.Equippable.TrySetEquipped(false);

            Equippable = null;

            return true;
        }
    }
}
