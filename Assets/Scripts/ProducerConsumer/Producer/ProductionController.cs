using System.Collections;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using UnityEngine;

public class ProductionController<TProducer, TProducible> : MonoBehaviour where TProducer : BaseProducer<TProducible>
    where TProducible : IProducible
{
    [SerializeField] private TProducer _producer;
    [SerializeField] private TProducible _producible;
    [SerializeField] private float _productionDelay = 0.2f;


    [PhaseListener(typeof(GamePhase), true)]
    public void OnGamePhaseStarted()
    {
        StartCoroutine(ProduceRoutine(_producible));
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator ProduceRoutine(TProducible producible)
    {
        float currentTime = 0;

        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentTime > _productionDelay)
            {
                _producer.Produce(producible);
                currentTime = 0;
            }


            yield return null;
        }
    }
}