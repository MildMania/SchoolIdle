using System;
using MMFramework.TrackerSystem;

public class UnlockableTrackable : ITrackable<UnlockableTrackData, Guid>
{
	public UnlockableTrackData TrackData { get; private set; }

	public UnlockableTrackable(
		UnlockableTrackData data)
	{
		TrackData = data;
	}

	public void UpdateData(UnlockableTrackData data)
	{
		TrackData.TrackID = data.TrackID;
		TrackData.CurrentCount = data.CurrentCount;
		TrackData.IsUnlock = data.IsUnlock;
	}
}

public class UnlockableTracker : TrackerBase<UnlockableTrackable, UnlockableTrackData, Guid>
{
	public UnlockableTracker(
		TrackerIOBase<UnlockableTrackData, Guid> trackerIO) 
		: base(trackerIO)
	{
	}
	
}