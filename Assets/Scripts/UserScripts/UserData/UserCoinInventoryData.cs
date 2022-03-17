using MMFramework.TrackerSystem;

public class UserCoinInventoryData : UserTrackableData<CoinTracker, Coin, CoinTrackData, ECoin>
{
    public UserCoinInventoryData(
        TrackerIOBase<CoinTrackData, ECoin> trackerIO)
        : base(trackerIO)
    {
    }
}
