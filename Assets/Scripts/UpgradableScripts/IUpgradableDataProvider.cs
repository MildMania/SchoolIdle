using System.Collections.Generic;

public interface IUpgradableDataProvider
{
	int GetUpgradableLevel(EUpgradable upgradable);
	void SetUpgradable(EUpgradable upgradable, int level, Dictionary<string,string> attributes);
}