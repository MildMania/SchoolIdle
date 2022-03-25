using System.Collections;
using System.Collections.Generic;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using UnityEngine;

public abstract class BaseLoadBehaviour<TBaseProducer, TProducible> : MonoBehaviour
    where TBaseProducer : BaseProducer<TProducible>
    where TProducible : IProducible
{
    [SerializeField] protected UpdatedFormationController _updatedFormationController;
    [SerializeField] protected Deliverer _deliverer;
    [SerializeField] private float _loadDelay = .3f;

    private List<TBaseProducer> _producers = new List<TBaseProducer>();


    protected void OnProducerEnteredFieldOfView(TBaseProducer producer)
    {
        _producers.Add(producer);
    }

    protected void OnProducerExitedFieldOfView(TBaseProducer producer)
    {
        _producers.Remove(producer);
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
                if (_producers.Count > 0)
                {
                    int index = (int) Random.Range(0, _producers.Count - 0.1f);

                    TProducible producible = default(TProducible);
                    if (_producers[index].TryRemoveAndGetLastProducible(ref producible))
                    {
                        LoadCustomActions(producible);
                    }
                }

                currentTime = 0;
            }

            yield return null;
        }
    }


    public abstract void LoadCustomActions(TProducible producible);
}