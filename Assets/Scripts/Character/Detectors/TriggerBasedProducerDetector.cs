using System.Collections;
using System.Collections.Generic;
using Producer.Old;
using UnityEngine;

public class TriggerBasedProducerDetector : BaseProducerDetector
{
    [SerializeField] private TriggerObjectHitController _producerHitController;


    private void Awake()
    {
        _producerHitController.OnHitTriggerObject += OnHitTriggerObject;
        _producerHitController.OnHitEndedTriggerObject += OnHitEndedTriggerObject;
    }

    private void OnHitEndedTriggerObject(TriggerObject triggerObject)
    {
        var producer = triggerObject.GetComponentInParent<ProducerBase>();
        OnEnded?.Invoke(producer);
    }

    private void OnDestroy()
    {
        _producerHitController.OnHitTriggerObject -= OnHitTriggerObject;
        _producerHitController.OnHitEndedTriggerObject -= OnHitEndedTriggerObject;
    }

    private void OnHitTriggerObject(TriggerObject triggerObject)
    {
        var producer = triggerObject.GetComponentInParent<ProducerBase>();
        LastDetected = producer;
        OnDetected?.Invoke(producer);
    }
}