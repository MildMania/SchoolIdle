using System;
using UnityEngine;

public class CoinController : MonoBehaviour
{
	// [SerializeField] private CoinWorthCollector _coinWorthCollector;

	// [SerializeField] private BaseCollectibleDetector _collectibleDetector;
	//
	// public Action<Collectible> OnCollectibleCollected;
	// private void Awake()
	// {
	// 	_collectibleDetector.OnDetected += OnDetected;
	// }
	//
	//
	// private void OnApplicationQuit()
	// {
	// 	_collectibleDetector.OnDetected -= OnDetected;
	// }
	//
	// private void OnDetected(Collectible collectible)
	// {
	// 	Collect();
	// 	Debug.Log("COINCONTROLLER COLLECT");
	// 	OnCollectibleCollected?.Invoke(collectible);
	// }

	public void Collect()
	{
		Debug.Log("COINCONTROLLER COLLECT");
		// _coinWorthCollector.CollectWorth(new CoinWorth());
	}
}