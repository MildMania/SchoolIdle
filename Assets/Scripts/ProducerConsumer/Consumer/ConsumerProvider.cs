using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerProvider : Singleton<ConsumerProvider>
{
    Dictionary<IResource, List<IAIInteractable>> _consumersByResource =
        new Dictionary<IResource, List<IAIInteractable>>();

    private void Awake()
    {
        Init();
    }

    private void Init()
    {

    }

    public void AddProducer(IAIInteractable consumer, IResource resourceType)
    {
        List<IAIInteractable> list;

        _consumersByResource.TryGetValue(resourceType, out list);

        if (list != null)
        {
            _consumersByResource[resourceType].Add(consumer);
        }
        else
        {
            _consumersByResource[resourceType] = new List<IAIInteractable>();
        }
    }

    public List<IAIInteractable> GetConsumers(IResource resourceType)
    {
        List<IAIInteractable> list;
        _consumersByResource.TryGetValue(resourceType, out list);

        return list;
    }

}

