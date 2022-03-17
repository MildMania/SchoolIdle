using UnityEngine;

[System.Serializable]
public class CoinWorth : IWorth
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

	public CoinWorth(
		ECoinType coinType,
		int count)
	{
		_coinType = coinType;
		_count = count;
	}
}