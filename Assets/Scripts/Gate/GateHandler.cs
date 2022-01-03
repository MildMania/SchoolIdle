using System;
using UnityEngine;


public class GateHandler : MonoBehaviour
{
	[SerializeField] private BaseGateDetector _baseGateDetector;
	public Action<GateBase> OnGateCollided { get; set; }

	private void Awake()
	{
		_baseGateDetector.OnDetected += OnDetected;
	}

	private void OnDestroy()
	{
		_baseGateDetector.OnDetected -= OnDetected;
	}

	private void OnDetected(GateBase gate)
	{
		if (gate.TryCollide())
		{
			OnGateCollided?.Invoke(gate);
		}
	}
}