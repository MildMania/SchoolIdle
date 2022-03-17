using System.Collections.Generic;
using UnityEngine;

public class CoinWorthDefiner : WorthDefinerWrapper
{
    [SerializeField] private ECoinType _coinType = default;
    public ECoinType CoinType
    {
        get => _coinType;
        set => _coinType = value;
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
            new CoinWorth(CoinType,Count)
        };
    }
}