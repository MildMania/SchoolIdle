using System.Collections;
using System.Collections.Generic;
using MMFramework.Utilities;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using UnityEngine;

public class MoneyLoadBehaviour : BaseLoadBehaviour<MoneyProducer, Money>
{
    [SerializeField] private CoinWorthCollector _coinWorthCollector;
    [SerializeField] private MoneyProducerFovController _moneyProducerFovController;

    private void Awake()
    {
        _moneyProducerFovController.OnTargetEnteredFieldOfView += OnProducerEnteredFieldOfView;
        _moneyProducerFovController.OnTargetExitedFieldOfView += OnProducerExitedFieldOfView;
    }


    private void OnDestroy()
    {
        _moneyProducerFovController.OnTargetEnteredFieldOfView -= OnProducerEnteredFieldOfView;
        _moneyProducerFovController.OnTargetExitedFieldOfView -= OnProducerExitedFieldOfView;
        
        StopAllCoroutines();
    }
    

    public override void LoadCustomActions(Money money)
    {
        money.OnMoveRoutineFinished += OnMoveRoutineFinished;
        Transform targetTransform = _deliverer.transform;
        money.MoveConsumable(targetTransform, _updatedFormationController.Container);
    }

    private void OnMoveRoutineFinished(Money money)
    {
        money.OnMoveRoutineFinished -= OnMoveRoutineFinished;
        var worthDefiner = money.GetComponent<CoinWorthDefiner>();
        _coinWorthCollector.CollectWorth(new CoinWorth(worthDefiner.Coin,worthDefiner.Count));
    }
}