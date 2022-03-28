using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerProvider : Singleton<ConsumerProvider>
{
    Dictionary<System.Type, List<IAIInteractable>> _consumersByResource =
        new Dictionary<System.Type, List<IAIInteractable>>();

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
        System.Type resType = resourceType.GetType();

        _consumersByResource.TryGetValue(resType, out list);

        if (list != null)
        {
            _consumersByResource[resType].Add(consumer);
        }
        else
        {
            _consumersByResource[resType] = new List<IAIInteractable>();
            _consumersByResource[resType].Add(consumer);
        }
    }

    public List<IAIInteractable> GetConsumers(IResource resourceType)
    {
        List<IAIInteractable> list;
        _consumersByResource.TryGetValue(resourceType.GetType(), out list);

        return list;
    }

}

