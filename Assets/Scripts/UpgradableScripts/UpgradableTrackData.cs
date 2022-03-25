using System;
using System.Collections.Generic;
using Boomlagoon.JSON;
using MMFramework.TrackerSystem;
using WarHeroes.InventorySystem;
using MMUtilities = MMFramework.Utilities.Utilities;

public class UpgradableTrackData : TrackData<EUpgradable>
{
	public int Level { get; set; }
	
	public UpgradableTrackData(
		EUpgradable upgradable, 
		int level)
		: base(upgradable)
	{
		Level = level;
	}

	public UpgradableTrackData(JSONObject trackDataObj)
		: base(trackDataObj)
	{
	}

	private const string LEVEL = "Level";

	protected override EUpgradable DeserializeTrackableID(string trackableIDStr)
	{
		return MMUtilities.IdentifyObjectEnum<EUpgradable>(trackableIDStr);
	}

	protected override void DeserializeCustomActions(JSONObject trackObj)
	{
		Level = (int) trackObj.GetNumber(LEVEL);

		base.DeserializeCustomActions(trackObj);
	}

	protected override void SerializeCustomActions(ref JSONObject trackableObj)
	{
		trackableObj.Add(LEVEL, Level);
		
		
		base.SerializeCustomActions(ref trackableObj);
	}
}