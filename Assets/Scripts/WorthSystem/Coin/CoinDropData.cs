using UnityEngine;

[System.Serializable]
public class CoinDropData : DropData
{
    [SerializeField] private ECoin _coin = default;

    public ECoin Currency
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
    
    public CoinDropData(Drop drop, ECoin coin, int count) : base(drop)
    {
        _coin = coin;
        _count = count;
    }
}