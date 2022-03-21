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

        UserGenericData genericData =
            new UserGenericData(
                new JSONDataIO<UserGenericData.GenericData>(
                    Path.Combine(DATA_FOLDER_PATH, "GenericData")));



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

        return new User(
            isLocalUser: true,
            inventoryController,
            unlockableUpdater,
            genericData,
            coinInventoryData,
            unlockableData);
    }
    
}
