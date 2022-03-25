using System.Collections;
using System.Collections.Generic;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using UnityEngine;

public class MoneyLoadBehaviour : MonoBehaviour
{
    [SerializeField] private UpdatedFormationController _updatedFormationController;
    [SerializeField] private MoneyProducerFovController _moneyProducerFovController;
    [SerializeField] private float _loadDelay = .3f;

    private List<MoneyProducer> _moneyProducers = new List<MoneyProducer>();


    private void Awake()
    {
        _moneyProducerFovController.OnTargetEnteredFieldOfView += OnMoneyProducerEnteredFieldOfView;
        _moneyProducerFovController.OnTargetExitedFieldOfView += OnMoneyProducerExitedFieldOfView;
    }


    private void OnDestroy()
    {
        _moneyProducerFovController.OnTargetEnteredFieldOfView -= OnMoneyProducerEnteredFieldOfView;
        _moneyProducerFovController.OnTargetExitedFieldOfView -= OnMoneyProducerExitedFieldOfView;


        StopAllCoroutines();
    }

    private void OnMoneyProducerEnteredFieldOfView(MoneyProducer moneyProducer)
    {
        _moneyProducers.Add(moneyProducer);
    }

    private void OnMoneyProducerExitedFieldOfView(MoneyProducer moneyProducer)
    {
        _moneyProducers.Remove(moneyProducer);
    }

    [PhaseListener(typeof(GamePhase), true)]
    public void OnGamePhaseStarted()
    {
        StartCoroutine(LoadRoutine());
    }

    private IEnumerator LoadRoutine()
    {
        float currentTime = 0;

        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentTime > _loadDelay)
            {
                if (_moneyProducers.Count > 0)
                {
                    int index = (int) Random.Range(0, _moneyProducers.Count - 0.1f);
                    Money money = null;
                    if (_moneyProducers[index].TryRemoveAndGetLastProducible(ref money))
                    {
                        Transform targetTransform = _updatedFormationController.GetLastTargetTransform(money.transform);
                        money.MoveProducible(targetTransform, _updatedFormationController.Container);
                    }
                }

                currentTime = 0;
            }


            yield return null;
        }
    }
}