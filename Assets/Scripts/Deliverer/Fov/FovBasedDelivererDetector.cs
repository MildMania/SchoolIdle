using System;
using UnityEngine;


[RequireComponent(typeof(DelivererFovController))]
public class FovBasedDelivererDetector : BaseDelivererDetector
{
    [SerializeField] private DelivererFovController _delivererFOVController;

    private void Awake()
    {
        SubscribeToFovController();
        _delivererFOVController.SetActive(true);
    }

    private void OnDestroy()
    {
        _delivererFOVController.SetActive(false);
        UnsubscribeFromFovController();
    }

    private void SubscribeToFovController()
    {
        _delivererFOVController.OnTargetEnteredFieldOfView += OnTargetEnteredFieldOfView;
        _delivererFOVController.OnTargetExitedFieldOfView += OnTargetExitedFieldOfView;
    }

    private void UnsubscribeFromFovController()
    {
        _delivererFOVController.OnTargetEnteredFieldOfView -= OnTargetEnteredFieldOfView;
        _delivererFOVController.OnTargetExitedFieldOfView -= OnTargetExitedFieldOfView;
    }

    private void OnTargetEnteredFieldOfView(Deliverer deliverer)
    {
        LastDetected = deliverer;
        OnDetected?.Invoke(deliverer);
    }

    private void OnTargetExitedFieldOfView(Deliverer deliverer)
    {
        OnEnded?.Invoke(deliverer);
    }
}