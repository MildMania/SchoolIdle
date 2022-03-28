using System;
using UnityEngine;


[RequireComponent(typeof(MoneyProducerFovController))]
public class FovBasedMoneyProducerDetector : BaseMoneyProducerDetector
{
    [SerializeField] private MoneyProducerFovController _moneyProducerFOVController;

    private void Awake()
    {
        SubscribeToFovController();
        _moneyProducerFOVController.SetActive(true);
    }

    private void OnDestroy()
    {
        _moneyProducerFOVController.SetActive(false);
        UnsubscribeFromFovController();
    }

    private void SubscribeToFovController()
    {
        _moneyProducerFOVController.OnTargetEnteredFieldOfView += OnTargetEnteredFieldOfView;
        _moneyProducerFOVController.OnTargetExitedFieldOfView += OnTargetExitedFieldOfView;
    }

    private void UnsubscribeFromFovController()
    {
        _moneyProducerFOVController.OnTargetEnteredFieldOfView -= OnTargetEnteredFieldOfView;
        _moneyProducerFOVController.OnTargetExitedFieldOfView -= OnTargetExitedFieldOfView;
    }

    private void OnTargetEnteredFieldOfView(MoneyProducer moneyProducer)
    {
        LastDetected = moneyProducer;
        OnDetected?.Invoke(moneyProducer);
    }

    private void OnTargetExitedFieldOfView(MoneyProducer moneyProducer)
    {
        OnEnded?.Invoke(moneyProducer);
    }
}