using UnityEngine;

[System.Serializable]
public class CoinWorth : IWorth
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

	public CoinWorth(
		ECoin coin,
		int count)
	{
		_coin = coin;
		_count = count;
	}
}