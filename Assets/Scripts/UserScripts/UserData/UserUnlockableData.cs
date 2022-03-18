using MMFramework.TrackerSystem;

public class UserUnlockableData : UserTrackableData<UnlockableTracker, UnlockableTrackable, UnlockableTrackData, string>,
	IUnlockableDataProvider
{
	public UserUnlockableData(
		TrackerIOBase<UnlockableTrackData, string> trackerIO)
		: base(trackerIO)
	{
	}
    
	public int GetUnlockable(string statisticsType)
	{
		if (!Tracker.TryGetSingleTrackable(statisticsType, out UnlockableTrackable trackable))
			return 0;

		return trackable.TrackData.CurrentCount;
	}

	public void SetUnlockable(string unlockableID, int currentCount, bool isUnlock)
	{
		Tracker.TryGetSingleTrackable(unlockableID, out UnlockableTrackable trackable);

		Tracker.TryAddToTracker(new UnlockableTrackData(unlockableID, currentCount,isUnlock));
	}
}