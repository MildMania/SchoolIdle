using MMFramework.TrackerSystem;
using WarHeroes.InventorySystem;


public class CoinTracker : TrackerBase<Coin, CoinTrackData, ECoin>
{
    public CoinTracker(
        TrackerIOBase<CoinTrackData, ECoin> trackerIO)
        : base(trackerIO)
    {
    }

    protected override TrackerModifierBase<Coin, CoinTrackData, ECoin> GetTrackerModifier()
    {
        return new CoinTrackerModifier(this);
    }
}

public class CoinTrackerModifier : TrackerModifierBase<Coin, CoinTrackData, ECoin>
{
    public CoinTrackerModifier(TrackerBase<Coin, CoinTrackData, ECoin> tracker)
        : base(tracker)
    {
    }

    public override ETrackerModifyAction UpdateTrackable(
        Coin existingTrackable,
        Coin newTrackable)
    {
        existingTrackable.Countable.IncreaseCount(
            newTrackable.Countable.Count);

        return base.UpdateTrackable(existingTrackable, newTrackable);
    }
}


public class CoinInventory : InventoryBase<CoinTracker, Coin, CoinTrackData, ECoin, EInventory>,
    ICounter
{
    private Counter _counter;

    public Counter Counter
    {
        get
        {
            if (_counter == null)
                _counter = new Counter(this);

            return _counter;
        }
    }

    public CoinInventory(
        CoinTracker tracker)
        : base(tracker)
    {
    }

    protected override TrackerLoadedTaskBase[] CreateTrackerLoadedTasks()
    {
        return new TrackerLoadedTaskBase[]
        {
            new TrackerLoadedTask_AddDefaultItems(
                new CoinTrackData(ECoin.Gold, 0))
        };
    }

    protected override EInventory GetInventoryType()
    {
        return EInventory.Currency;
    }

    public void CountUpdated(ICountable countable)
    {
        Coin coin = countable as Coin;

        Tracker.TrackerUpdated(coin);
    }
}