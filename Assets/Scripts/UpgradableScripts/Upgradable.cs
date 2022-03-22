using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class Upgradable : SerializedMonoBehaviour
{
	[SerializeField] private EUpgradable _upgradableType;

	[OdinSerialize] private Dictionary<string, float> _upgradableAttributes;
	
	public IRequirement[] RequirementData
		= Array.Empty<IRequirement>();

	public Action<UpgradableTrackData> OnUpgradableTrackDataInit;
	public Action<int> OnLevelUpgraded;

	private UpgradableTrackData _upgradableTrackData;

	private void Awake()
	{
		List<IRequirement> reqList = RequirementData.ToList();
		foreach (var levelToReq in GameConfigManager.Instance.GameConfig.LevelToRequirementCoin)
		{
			RequirementDataCoin dataCoin = new RequirementDataCoin(ECoin.Gold, (int) levelToReq.Value);
			reqList.Add(dataCoin.CreateRequirement(UserManager.Instance.LocalUser));
		}
		RequirementData = reqList.ToArray();
	}


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
		OnUpgradableTrackDataInit?.Invoke(_upgradableTrackData);
	}

	public bool TryUpgrade(User user)
	{
		return RequirementUtilities.TrySatisfyRequirements(
			user, RequirementData);
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (TryUpgrade(UserManager.Instance.LocalUser))
			{
				var requirementCoin = (RequirementCoin) RequirementData[_upgradableTrackData.Level-1];
				int totalRequiredAmount = requirementCoin.RequirementData.RequiredAmount;
				
				UpgradableTrackData upgradableTrackData = new UpgradableTrackData(_upgradableTrackData.TrackID, 
					++_upgradableTrackData.Level,_upgradableAttributes);
				
				var userUpgradableData = UserManager.Instance.LocalUser.GetUserData<UserUpgradableData>();
				userUpgradableData.Tracker.TryUpsert(upgradableTrackData);

				var userCoinInventoryData = UserManager.Instance.LocalUser.GetUserData<UserCoinInventoryData>();
				Coin trackableCoin;
				userCoinInventoryData.Tracker.TryGetSingle(ECoin.Gold, out trackableCoin);
				
				CoinTrackData coinTrackData =
					new CoinTrackData(
						ECoin.Gold,
						count: trackableCoin.TrackData.CurrentCount - totalRequiredAmount);
		
				trackableCoin.UpdateData(coinTrackData);
				UserManager.Instance.LocalUser.SaveData(OnSaved);
				
				void OnSaved()
				{
					Debug.Log("upgrade saved");
				}
				Debug.Log("UPGRADE YAPILDI.");
			};
		}
	}
}