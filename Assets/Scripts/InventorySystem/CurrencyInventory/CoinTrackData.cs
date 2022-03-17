using Boomlagoon.JSON;
using MMFramework.TrackerSystem;
using WarHeroes.InventorySystem;
using MMUtilities = MMFramework.Utilities.Utilities;

public class CoinTrackData : TrackData<ECoin>,
    ICountableTrackData
{
    public int Count { get; set; }

    public CoinTrackData(
        ECoin trackID,
        int count)
        : base(trackID)
    {
        Count = count;
    }

    public CoinTrackData(JSONObject trackDataObj)
        : base(trackDataObj)
    {
    }

    private const string COUNT = "Count";

    protected override ECoin DeserializeTrackableID(string trackableIDStr)
    {
        return MMUtilities.IdentifyObjectEnum<ECoin>(trackableIDStr);
    }

    protected override void DeserializeCustomActions(JSONObject trackObj)
    {
        Count = (int) trackObj.GetNumber(COUNT);

        base.DeserializeCustomActions(trackObj);
    }

    protected override void SerializeCustomActions(ref JSONObject trackableObj)
    {
        trackableObj.Add(COUNT, Count);

        base.SerializeCustomActions(ref trackableObj);
    }
}