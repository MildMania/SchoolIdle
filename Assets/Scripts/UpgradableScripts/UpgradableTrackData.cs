using System;
using System.Collections.Generic;
using Boomlagoon.JSON;
using MMFramework.TrackerSystem;
using WarHeroes.InventorySystem;
using MMUtilities = MMFramework.Utilities.Utilities;

public class UpgradableTrackData : TrackData<EUpgradable>
{
	public int Level { get; set; }

	public Dictionary<string, string> Attributes;
	public UpgradableTrackData(
		EUpgradable upgradable, 
		int level,Dictionary<string,string> attributes)
		: base(upgradable)
	{
		Level = level;
		Attributes = attributes;
	}

	public UpgradableTrackData(JSONObject trackDataObj)
		: base(trackDataObj)
	{
	}

	private const string LEVEL = "Level";
	private const string ATTRIBUTES = "Attributes";


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
		
		var attributesJson = new JSONObject();

		foreach (var attribute in Attributes)
		{
			attributesJson.Add(attribute.Key,attribute.Value);
		}
		
		trackableObj.Add(ATTRIBUTES,attributesJson);
		
		base.SerializeCustomActions(ref trackableObj);
	}
}