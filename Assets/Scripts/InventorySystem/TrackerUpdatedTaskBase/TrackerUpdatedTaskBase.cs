using MMFramework.TrackerSystem;
using System;

namespace WarHeroes.InventorySystem
{
    public abstract class TrackerUpdatedTaskBase
    {
        public abstract void Execute<TTracker, TTrackable, TTrackData, TID, TEnum>(
            InventoryBase<TTracker, TTrackable, TTrackData, TID, TEnum> inventory,
            TTracker tracker)
            where TTracker : TrackerBase<TTrackable, TTrackData, TID>
            where TTrackable : ITrackable<TTrackData, TID>
            where TTrackData : TrackData<TID>
            where TID : IConvertible
            where TEnum : Enum;
    }
}
