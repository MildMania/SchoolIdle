using System.Collections;
using System.Collections.Generic;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using UnityEngine;

public abstract class BaseLoadBehaviour<TBaseProducer, TResource> : MonoBehaviour
    where TBaseProducer : BaseProducer<TResource>
    where TResource : IResource
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

                    TResource resource = default(TResource);
                    if (_producers[index].TryRemoveAndGetLastResource(ref resource))
                    {
                        LoadCustomActions(resource);
                    }
                }

                currentTime = 0;
            }

            yield return null;
        }
    }


    public abstract void LoadCustomActions(TResource resource);
}