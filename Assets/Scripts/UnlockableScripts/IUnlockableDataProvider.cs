using System;

public interface IUnlockableDataProvider
{
	int GetUnlockable(Guid unlockableID);
	void SetUnlockable(Guid unlockableID, int currentCount, bool isUnlock);
}