using System;
using SerializableData;
using System.IO;
using WarHeroes.InventorySystem;
using MMFramework.TrackerSystem;

public static class UserFactory
{

    public static User CreateUser()
    {
        return CreateDefaultUser();
    }

    private static User CreateDefaultUser()
    {
        const string DATA_FOLDER_PATH = "SaveData/UserData/";
        
        UserCoinInventoryData coinInventoryData =
            new UserCoinInventoryData(
                new JSONTrackerIO<CoinTrackData, ECoin>(
                    Path.Combine(DATA_FOLDER_PATH, "CoinInventoryData")));

        CoinInventory coinInventory =
            new CoinInventory(coinInventoryData.Tracker);

        InventoryController<EInventory> inventoryController =
            new InventoryController<EInventory>(
                coinInventory);
        
        UserUnlockableData unlockableData =
            new UserUnlockableData(
                new JSONTrackerIO<UnlockableTrackData, Guid>(
                    Path.Combine(DATA_FOLDER_PATH, "UnlockableData")));

        UnlockableUpdater unlockableUpdater
            = new UnlockableUpdater(unlockableData);

        UserUpgradableData upgradableData =
            new UserUpgradableData(
                new JSONTrackerIO<UpgradableTrackData, EUpgradable>(
                    Path.Combine(DATA_FOLDER_PATH, "UpgradableData")));

        UpgradableUpdater upgradableUpdater
            = new UpgradableUpdater(upgradableData);
        
        return new User(
            isLocalUser: true,
            inventoryController,
            unlockableUpdater,
            upgradableUpdater,
            coinInventoryData,
            unlockableData,
            upgradableData);
    }
    
}
