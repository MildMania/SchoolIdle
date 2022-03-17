using UnityEngine;

[System.Serializable]
public class CoinDropData : DropData
{
    [SerializeField] private ECoinType _coinType = default;

    public ECoinType Currency
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
    
    public CoinDropData(Drop drop, ECoinType coinType, int count) : base(drop)
    {
        _coinType = coinType;
        _count = count;
    }
}