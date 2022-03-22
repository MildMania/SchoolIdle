using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class Upgradable : SerializedMonoBehaviour
{
	[SerializeField] private EUpgradable _upgradableType;

	[OdinSerialize] private Dictionary<string, string> _upgradableAttributes;
	
	[OdinSerialize] public IRequirement[] RequirementData
		= Array.Empty<IRequirement>();

	public Action<int> OnLevelUpgraded;

	private UpgradableTrackData _upgradableTrackData;
	private void Start()
	{
		UpgradableTrackable upgradableTrackable;

		if (UserManager.Instance.LocalUser.GetUserData<UserUpgradableData>().Tracker
			.TryGetSingle(_upgradableType, out upgradableTrackable))
		{
			_upgradableTrackData = upgradableTrackable.TrackData;
		}
		else
		{
			_upgradableTrackData = new UpgradableTrackData(_upgradableType, 1,_upgradableAttributes);
			upgradableTrackable = new UpgradableTrackable(_upgradableTrackData);
			UserManager.Instance.LocalUser.GetUserData<UserUpgradableData>().Tracker.TryCreate(upgradableTrackable);
		} 
	}

	public bool TryUpgrade(User user)
	{
		return RequirementUtilities.TrySatisfyRequirements(
			user, RequirementData);
	}
}