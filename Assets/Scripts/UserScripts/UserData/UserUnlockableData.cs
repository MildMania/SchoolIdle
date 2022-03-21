using System;
using MMFramework.TrackerSystem;

public class UserUnlockableData : UserTrackableData<UnlockableTracker, UnlockableTrackable, UnlockableTrackData, Guid>,
	IUnlockableDataProvider
{
	public UserUnlockableData(
		TrackerIOBase<UnlockableTrackData, Guid> trackerIO)
		: base(trackerIO)
	{
	}
    
	public int GetUnlockable(Guid unlockableID)
	{
		if (!Tracker.TryGetSingle(unlockableID, out UnlockableTrackable trackable))
			return 0;

		return trackable.TrackData.CurrentCount;
	}

	public void SetUnlockable(Guid unlockableID, int currentCount, bool isUnlock)
	{
		Tracker.TryGetSingle(unlockableID, out UnlockableTrackable trackable);

		Tracker.TryUpsert(new UnlockableTrackData(unlockableID, currentCount,isUnlock));
	}
}