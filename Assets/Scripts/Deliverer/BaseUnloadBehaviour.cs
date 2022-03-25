using System.Collections;
using System.Collections.Generic;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using UnityEngine;

public abstract class BaseUnloadBehaviour<TBaseConsumer, TConsumable> : MonoBehaviour
    where TBaseConsumer : BaseConsumer<TConsumable>
    where TConsumable : IConsumable
{
    [SerializeField] protected UpdatedFormationController _updatedFormationController;
    [SerializeField] protected Deliverer _deliverer;
    [SerializeField] private float _unloadDelay = .3f;

    protected List<TBaseConsumer> _consumers = new List<TBaseConsumer>();


    protected void OnProducerEnteredFieldOfView(TBaseConsumer producer)
    {
        _consumers.Add(producer);
    }

    protected void OnProducerExitedFieldOfView(TBaseConsumer producer)
    {
        _consumers.Remove(producer);
    }

    [PhaseListener(typeof(GamePhase), true)]
    public void OnGamePhaseStarted()
    {
        StartCoroutine(UnloadRoutine());
    }

    private IEnumerator UnloadRoutine()
    {
        float currentTime = 0;

        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentTime > _unloadDelay)
            {
                if (_consumers.Count > 0)
                {
                    int index = (int) Random.Range(0, _consumers.Count - 0.1f);
                    if (_deliverer.Papers.Count > 0)
                    {
                        UnloadCustomActions(index);
                    }
                }

                currentTime = 0;
            }

            yield return null;
        }
    }


    public abstract void UnloadCustomActions(int index);
}