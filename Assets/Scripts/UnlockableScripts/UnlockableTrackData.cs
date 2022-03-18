using Boomlagoon.JSON;
using MMFramework.TrackerSystem;
using WarHeroes.InventorySystem;
using MMUtils = MMFramework.Utilities.Utilities;

public class UnlockableTrackData : TrackData<string>,
	ICountableTrackData
{
	public int CurrentCount { get; set; }
	public bool IsUnlock { get; set; }

	public UnlockableTrackData(
		string trackID,
		int currentCount, bool isUnlock)
		: base(trackID)
	{
		CurrentCount = currentCount;
		IsUnlock = isUnlock;
	}

	public UnlockableTrackData(JSONObject trackDataObj)
		: base(trackDataObj)
	{
	}

	private const string CURRENTCOUNT = "CurrentCount";

	private const string ISUNLOCK = "IsUnlock";

	protected override string DeserializeTrackableID(string trackableIDStr)
	{
		return MMUtils.IdentifyObjectEnum<string>(trackableIDStr);
	}

	protected override void DeserializeCustomActions(JSONObject trackObj)
	{
		CurrentCount = (int) trackObj.GetNumber(CURRENTCOUNT);

		IsUnlock = trackObj.GetBoolean(ISUNLOCK);


		base.DeserializeCustomActions(trackObj);
	}

	protected override void SerializeCustomActions(ref JSONObject trackableObj)
	{
		trackableObj.Add(CURRENTCOUNT, CurrentCount);
		
		trackableObj.Add(ISUNLOCK, IsUnlock);

		base.SerializeCustomActions(ref trackableObj);
	}
}