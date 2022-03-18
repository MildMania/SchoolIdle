using MMFramework.TasksV2;
using System;
using UnityEngine;


public interface IUnlockable
{
	Unlockable Unlockable { get; }
}

[Serializable]
public class Unlockable
{
	[SerializeField] public bool IsLocked { get; private set; } = true;

	[SerializeField] public IRequirementData[] RequirementData
		= Array.Empty<IRequirementData>();

	[SerializeField] private MMTaskExecutor _lockedTaskExecutor
		= new MMTaskExecutor();

	[SerializeField] private MMTaskExecutor _unlockedTaskExecutor
		= new MMTaskExecutor();
	
	public UnlockableTrackData TrackData { get; set; }
	public Action<bool> OnLockedChanged { get; set; }

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

	private bool TryUnlock(User user)
	{
		return RequirementUtilities.TrySatisfyRequirements(
			user, RequirementData);
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