using System;

namespace WarHeroes.InventorySystem
{
    public interface IEquippable
    {
        Equippable Equippable { get; }
    }

    public class Equippable
    {
        public bool IsEquipped
        {
            get => _equippableTrackData.IsEquipped;
            private set => _equippableTrackData.IsEquipped = value;
        }

        public Action<bool> OnSetEquipped { get; set; }

        private IEquippableTrackData _equippableTrackData;

        public Equippable(
            IEquippableTrackData equippableTrackData)
        {
            _equippableTrackData = equippableTrackData;
        }

        public bool TrySetEquipped(bool isEquipped)
        {
            if (IsEquipped == isEquipped)
                return true;

            IsEquipped = isEquipped;

            OnSetEquipped?.Invoke(IsEquipped);

            return true;
        }
    }
}
