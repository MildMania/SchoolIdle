using MMFramework.TrackerSystem;
using WarHeroes.InventorySystem;


public class Coin : ITrackable<CoinTrackData, ECoin>,
    ICountable
{
    public CoinTrackData TrackData { get; private set; }

    private Countable _countable;

    public Countable Countable
    {
        get
        {
            if (_countable == null)
                _countable = new Countable(TrackData)
                    .WithMinCount(0);

            return _countable;
        }
    }

    public Coin(
        CoinTrackData data)
    {
        TrackData = data;
    }

    public void UpdateData(CoinTrackData data)
    {
        TrackData.TrackID = data.TrackID;
        TrackData.Count = data.Count;
    }
}