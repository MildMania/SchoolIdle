using System;

public class UnlockableUpdater
{
	private IUnlockableDataProvider _unlockableDataProvider;

	public Action<string, int> OnStatisticsUpdated { get; set; }
    
	public UnlockableUpdater(
		IUnlockableDataProvider unlockableDataProvider)
	{
		_unlockableDataProvider = unlockableDataProvider;
	}

	public void UpdateUnlockable(string unlockableID, int currentCount , bool isUnlock)
	{
		_unlockableDataProvider.SetUnlockable(unlockableID, 
			_unlockableDataProvider.GetUnlockable(unlockableID) + currentCount,isUnlock);

		OnStatisticsUpdated?.Invoke(unlockableID, _unlockableDataProvider.GetUnlockable(unlockableID));
	}

	public int GetUnlockable(string statisticsType)
	{
		return _unlockableDataProvider.GetUnlockable(statisticsType);
	}
}