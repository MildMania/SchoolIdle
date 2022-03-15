using UnityEngine;

public class TriggerBasedConsumerDetector : BaseConsumerDetector
{
	[SerializeField] private TriggerObjectHitController _consumerHitController;


	private void Awake()
	{
		_consumerHitController.OnHitTriggerObject += OnHitTriggerObject;
		_consumerHitController.OnHitEndedTriggerObject += OnHitEndedTriggerObject;
	}

	private void OnHitEndedTriggerObject(TriggerObject triggerObject)
	{
		var consumer = triggerObject.GetComponentInParent<ConsumerBase>();
		OnEnded?.Invoke(consumer);
	}

	private void OnDestroy()
	{
		_consumerHitController.OnHitTriggerObject -= OnHitTriggerObject;
		_consumerHitController.OnHitEndedTriggerObject -= OnHitEndedTriggerObject;
	}

	private void OnHitTriggerObject(TriggerObject triggerObject)
	{
		var consumer = triggerObject.GetComponentInParent<ConsumerBase>();
		LastDetected = consumer;
		OnDetected?.Invoke(consumer);
	}
}