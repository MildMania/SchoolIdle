using System;
using UnityEngine;


[RequireComponent(typeof(PaperConsumerFovController))]
public class FovBasedPaperConsumerDetector : BasePaperConsumerDetector
{
    [SerializeField] private PaperConsumerFovController _paperConsumerFOVController;

    private void Awake()
    {
        SubscribeToFovController();
        _paperConsumerFOVController.SetActive(true);
    }

    private void OnDestroy()
    {
        _paperConsumerFOVController.SetActive(false);
        UnsubscribeFromFovController();
    }

    private void SubscribeToFovController()
    {
        _paperConsumerFOVController.OnTargetEnteredFieldOfView += OnTargetEnteredFieldOfView;
        _paperConsumerFOVController.OnTargetExitedFieldOfView += OnTargetExitedFieldOfView;
    }

    private void UnsubscribeFromFovController()
    {
        _paperConsumerFOVController.OnTargetEnteredFieldOfView -= OnTargetEnteredFieldOfView;
        _paperConsumerFOVController.OnTargetExitedFieldOfView -= OnTargetExitedFieldOfView;
    }

    private void OnTargetEnteredFieldOfView(PaperConsumer paperConsumer)
    {
        LastDetected = paperConsumer;
        OnDetected?.Invoke(paperConsumer);
    }

    private void OnTargetExitedFieldOfView(PaperConsumer paperConsumer)
    {
        OnEnded?.Invoke(paperConsumer);
    }
}