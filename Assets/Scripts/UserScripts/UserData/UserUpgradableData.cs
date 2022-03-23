using System;
using System.Collections.Generic;
using MMFramework.TrackerSystem;

public class UserUpgradableData : UserTrackableData<UpgradableTracker, UpgradableTrackable, UpgradableTrackData, EUpgradable>,
	IUpgradableDataProvider
{
	public UserUpgradableData(
		TrackerIOBase<UpgradableTrackData, EUpgradable> trackerIO)
		: base(trackerIO)
	{
	}
    
	public int GetUpgradableLevel(EUpgradable upgradable)
	{
		if (!Tracker.TryGetSingle(upgradable, out UpgradableTrackable trackable))
			return 0;

		return trackable.TrackData.Level;
	}

	public void SetUpgradable(EUpgradable upgradable, int level)
	{
		Tracker.TryUpsert(new UpgradableTrackData(upgradable, level));
	}
}