using System;
using UnityEngine;


[RequireComponent(typeof(PaperProducerFovController))]
public class FovBasedPaperProducerDetector : BasePaperProducerDetector
{
    [SerializeField] private PaperProducerFovController _paperProducerFOVController;

    private void Awake()
    {
        SubscribeToFovController();
        _paperProducerFOVController.SetActive(true);
    }

    private void OnDestroy()
    {
        _paperProducerFOVController.SetActive(false);
        UnsubscribeFromFovController();
    }

    private void SubscribeToFovController()
    {
        _paperProducerFOVController.OnTargetEnteredFieldOfView += OnTargetEnteredFieldOfView;
        _paperProducerFOVController.OnTargetExitedFieldOfView += OnTargetExitedFieldOfView;
    }

    private void UnsubscribeFromFovController()
    {
        _paperProducerFOVController.OnTargetEnteredFieldOfView -= OnTargetEnteredFieldOfView;
        _paperProducerFOVController.OnTargetExitedFieldOfView -= OnTargetExitedFieldOfView;
    }

    private void OnTargetEnteredFieldOfView(PaperProducer paperProducer)
    {
        LastDetected = paperProducer;
        OnDetected?.Invoke(paperProducer);
    }

    private void OnTargetExitedFieldOfView(PaperProducer paperProducer)
    {
        OnEnded?.Invoke(paperProducer);
    }
}