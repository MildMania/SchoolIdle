using System.Collections.Generic;
using UnityEngine;

public class CoinWorthDefiner : WorthDefinerWrapper
{
    [SerializeField] private ECoin _coin = default;
    public ECoin Coin
    {
        get => _coin;
        set => _coin = value;
    }

    [SerializeField] private int _count = 0;
    public int Count
    {
        get => _count;
        set => _count = value;
    }
    public override List<IWorth> GetWorths()
    {
        return new List<IWorth>
        {
            new CoinWorth(Coin,Count)
        };
    }
}