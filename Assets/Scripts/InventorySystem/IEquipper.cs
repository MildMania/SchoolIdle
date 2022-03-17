using System;
using System.Collections.Generic;
using System.Linq;

namespace WarHeroes.InventorySystem
{
    public interface IEquipper
    {
        Equipper Equipper { get; }
        void EquippableStateUpdated(IEquippable equippable);
}

    public class Equipper
{
        private IEquipper _equipper;

        public List<Slot> Slots { get; private set; }

        public Action<IEquippable> OnEquippableStateUpdated { get; set; }
        
        // TODO DISCARD STATIC EVENT
        public static Action<IEquippable> OnEquippableStateUpdated_Static { get; set; }

        public Equipper(
            IEquipper equipper,
            int slotCount)
        {
            _equipper = equipper;

            Slots = new List<Slot>();

            for (int i = 0; i < slotCount; i++)
                Slots.Add(new Slot(i));
        }

        public bool TryEquip(IEquippable equippable)
        {
            if (!TryGetEmptySlot(out Slot slot))
            {
                slot = Slots[0];
                TryUnequip(slot);
            }

            return TryEquip(equippable, slot);
        }

        public bool TryEquip(
            IEquippable equippable,
            int slotID)
        {
            if (!TryGetSlotByID(slotID, out Slot slot))
                return false;

            return TryEquip(equippable, slot);
        }

        private bool TryEquip(
            IEquippable equippable,
            Slot slot)
        {
            if (!slot.TryEquip(equippable))
                return false;

            _equipper.EquippableStateUpdated(equippable);

            OnEquippableStateUpdated?.Invoke(equippable);

            OnEquippableStateUpdated_Static?.Invoke(equippable);

            return true;
        }

        public bool TryUnequip(
            IEquippable equippable)
        {
            if (!TryGetSlot(equippable, out Slot slot))
                return false;

            if (!slot.TryUnequip())
                return false;

            OnEquippableStateUpdated?.Invoke(equippable);
            
            OnEquippableStateUpdated_Static?.Invoke(equippable);

            return true;
        }

        public bool TryUnequip(
            Slot slot)
        {
            IEquippable equippable = slot.Equippable;

            if (!slot.TryUnequip())
                return false;

            _equipper.EquippableStateUpdated(equippable);

            OnEquippableStateUpdated?.Invoke(equippable);
            
            OnEquippableStateUpdated_Static?.Invoke(equippable);

            return true;
        }

        public IEquippable GetSingleEquippedEquippable()
        {
            return Slots.Single(val => val.Equippable.Equippable.IsEquipped).Equippable;
        }

        public List<IEquippable> GetEquippedEquippables()
        {
            List<IEquippable> equippables = new List<IEquippable>();

            foreach (Slot slot in Slots)
                if (!slot.IsEmpty)
                    equippables.Add(slot.Equippable);

            return equippables;
        }

        private bool TryGetEmptySlot(out Slot slot)
        {
            slot = Slots.FirstOrDefault(val => val.IsEmpty);

            if (slot == null)
                return false;

            return true;
        }

        private bool TryGetSlotByID(int slotID, out Slot slot)
        {
            slot = Slots.FirstOrDefault(val => val.ID == slotID);

            if (slot == null)
                return false;

            return true;
        }

        private bool TryGetSlot(IEquippable equippable, out Slot slot)
        {
            slot = Slots.FirstOrDefault(val => val.Equippable == equippable);

            if (slot == null)
                return false;

            return true;
        }
    }
}
