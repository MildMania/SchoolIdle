using System;
using UnityEngine;


public class TriggerBasedGateDetector : BaseGateDetector
{
	[SerializeField] private TriggerObjectHitController _gateObjectHitController;

	private void Awake()
	{
		_gateObjectHitController.OnHitTriggerObject += OnHitTriggerObject;
	}

	private void OnDestroy()
	{
		_gateObjectHitController.OnHitTriggerObject  -= OnHitTriggerObject;
	}

	private void OnHitTriggerObject(TriggerObject triggerObject)
	{
		var gate = triggerObject.GetComponentInParent<GateBase>();
		LastDetected = gate;
		OnDetected?.Invoke(gate);
	}
}