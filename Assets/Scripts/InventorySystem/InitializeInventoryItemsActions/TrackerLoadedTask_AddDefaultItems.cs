using MMFramework.TrackerSystem;
using System;
using System.Linq;

namespace WarHeroes.InventorySystem
{
    public class TrackerLoadedTask_AddDefaultItems : TrackerLoadedTaskBase
    {
        private ITrackData[] _trackDataArr;

        public TrackerLoadedTask_AddDefaultItems(
            params ITrackData[] defaultTrackInfos)
        {
            if (defaultTrackInfos == null)
                _trackDataArr = new ITrackData[0];
            else
                _trackDataArr = defaultTrackInfos.ToArray();
        }

        public override void Execute<TTracker, TTrackable, TTrackData, TID1, TEnum>(
            InventoryBase<TTracker, TTrackable, TTrackData, TID1, TEnum> inventory, 
            TTracker tracker)
        {
            foreach (ITrackData trackData in _trackDataArr)
            {
                TTrackData data = trackData as TTrackData;

                if (data == null)
                    continue;

                if (tracker.HasTrackable(data.TrackID))
                    continue;

                tracker.TryUpsert(data, false);
            }

            tracker.Write();
        }
    }
}
