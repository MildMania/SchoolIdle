public interface IUnlockableDataProvider
{
	int GetUnlockable(string unlockableID);
	void SetUnlockable(string unlockableID, int currentCount, bool isUnlock);
}