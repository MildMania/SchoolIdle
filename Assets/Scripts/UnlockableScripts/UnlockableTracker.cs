using MMFramework.TrackerSystem;

public class UnlockableTrackable : ITrackable<UnlockableTrackData, string>
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

public class UnlockableTracker : TrackerBase<UnlockableTrackable, UnlockableTrackData, string>
{
	public UnlockableTracker(
		TrackerIOBase<UnlockableTrackData, string> trackerIO) 
		: base(trackerIO)
	{
	}

	protected override TrackerModifierBase<UnlockableTrackable, UnlockableTrackData, string> GetTrackerModifier()
	{
		return new UnlockableTrackerModifier(this);
	}
}

public class UnlockableTrackerModifier : TrackerModifierBase<UnlockableTrackable, UnlockableTrackData, string>
{
	public UnlockableTrackerModifier(TrackerBase<UnlockableTrackable, UnlockableTrackData, string> tracker) 
		: base(tracker)
	{
	}

	public override ETrackerModifyAction UpdateTrackable(
		UnlockableTrackable existingTrackable,
		UnlockableTrackable newTrackable)
	{
		existingTrackable.TrackData.CurrentCount = newTrackable.TrackData.CurrentCount;
		existingTrackable.TrackData.IsUnlock = newTrackable.TrackData.IsUnlock;

		return base.UpdateTrackable(existingTrackable, newTrackable);
	}
}