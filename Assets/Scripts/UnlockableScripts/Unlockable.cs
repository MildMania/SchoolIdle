using MMFramework.TasksV2;
using System;
using System.Linq;
using Sirenix.Serialization;
using UnityEngine;
using WarHeroes.InventorySystem;
using Countable = MMFramework.InventorySystem.Countable;


public interface IUnlockable
{
	Unlockable Unlockable { get; }
}

[Serializable]
public class Unlockable
{
	public IRequirement[] Requirements = Array.Empty<IRequirement>();
	public bool IsLocked { get; private set; }
	public int Count { get; private set; }

	private UnlockableTrackData _unlockableTrackData;

	// [OdinSerialize] public IRequirementData[] RequirementData
	// 	= Array.Empty<IRequirementData>();

	[SerializeField] private MMTaskExecutor _lockedTaskExecutor
		= new MMTaskExecutor();

	[SerializeField] private MMTaskExecutor _unlockedTaskExecutor
		= new MMTaskExecutor();

	public Action<bool> OnLockedChanged { get; set; }

	public void Init(UnlockableTrackData trackData)
	{
		_unlockableTrackData = trackData;
		IsLocked = trackData.IsUnlock;
		Count = trackData.CurrentCount;
	}
	
	public bool TrySetLocked(
		User user,
		bool isLocked)
	{
		if (IsLocked == isLocked)
			return false;

		if (!isLocked
			&& !TryUnlock(user))
			return false;

		IsLocked = isLocked;

		if (IsLocked)
			_lockedTaskExecutor.Execute(null);
		else
			_unlockedTaskExecutor.Execute(null);

		OnLockedChanged?.Invoke(IsLocked);

		return true;
	}

	public bool TryUnlock(User user)
	{
		var userCoinInventoryData = user.GetUserData<UserCoinInventoryData>();

		var userUnlockableData = user.GetUserData<UserUnlockableData>();
		
		Coin trackableCoin;
		userCoinInventoryData.Tracker.TryGetSingle(ECoin.Gold, out trackableCoin);
		
		
		int totalRequiredAmount = 0;
			
		foreach (var requirement in Requirements)
		{
			var requirementCoin = (RequirementCoin) requirement;

			totalRequiredAmount += requirementCoin.RequirementData.RequiredAmount;
		}

		totalRequiredAmount -= _unlockableTrackData.CurrentCount;

		if (trackableCoin.TrackData.CurrentCount >= totalRequiredAmount)
		{
			UnlockableTrackData unlockableTrackData = new UnlockableTrackData(_unlockableTrackData.TrackID, 
				_unlockableTrackData.CurrentCount + totalRequiredAmount,true);
			userUnlockableData.Tracker.TryUpsert(unlockableTrackData);
			
			CoinTrackData coinTrackData =
				new CoinTrackData(
					ECoin.Gold,
					count: trackableCoin.TrackData.CurrentCount - totalRequiredAmount);
		
			trackableCoin.UpdateData(coinTrackData);
			return true;
		}
		else
		{
			UnlockableTrackData unlockableTrackData = new UnlockableTrackData(_unlockableTrackData.TrackID, 
				_unlockableTrackData.CurrentCount + trackableCoin.TrackData.CurrentCount,
				false);
			userUnlockableData.Tracker.TryUpsert(unlockableTrackData);
			
			CoinTrackData coinTrackData =
				new CoinTrackData(
					ECoin.Gold,
					count: 0);
		
			trackableCoin.UpdateData(coinTrackData);
			
			return false;
		}



	}

	public void ForceSetLocked(
		bool isLocked,
		bool invokeTaskExecutors = false)
	{
		if (IsLocked == isLocked)
			return;

		IsLocked = isLocked;

		if (invokeTaskExecutors)
		{
			if (IsLocked)
				_lockedTaskExecutor.Execute(null);
			else
				_unlockedTaskExecutor.Execute(null);
		}

		OnLockedChanged?.Invoke(IsLocked);
	}
}