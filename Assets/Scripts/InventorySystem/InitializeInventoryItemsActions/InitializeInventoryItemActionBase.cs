using MMFramework.TrackerSystem;
using System;

namespace WarHeroes.InventorySystem
{
    public abstract class InitializeInventoryItemActionBase
    {
        public abstract void Execute<TTracker, TTrackable, TTrackInfo, TID, TEnum>(
            InventoryBase<TTracker, TTrackable, TTrackInfo, TID, TEnum> inventory,
            TTrackable trackable)
            where TTracker : TrackerBase<TTrackable, TTrackInfo, TID>
            where TTrackable : ITrackable<TTrackInfo, TID>
            where TTrackInfo : TrackData<TID>
            where TID : IConvertible
            where TEnum : Enum;
    }
}
