using System;
using UnityEngine;

public class CoinController : MonoBehaviour
{
	private int _currentCoinCount;
	public Action<int> OnCoinUpdated;
	public Action<int> OnCoinInit;
	private void Awake()
	{
		Coin trackable;
		
		UserManager.Instance.LocalUser.GetUserData<UserCoinInventoryData>().Tracker.TryGetSingle(ECoin.Gold,out trackable);

		_currentCoinCount = trackable.TrackData.CurrentCount;
	}

	private void Start()
	{
		OnCoinInit?.Invoke(_currentCoinCount);
	}

	public void Collect(int currentCoinCount)
	{
		_currentCoinCount = currentCoinCount;
		OnCoinUpdated?.Invoke(currentCoinCount);
		Debug.Log("COINCONTROLLER COLLECT");
	}
}