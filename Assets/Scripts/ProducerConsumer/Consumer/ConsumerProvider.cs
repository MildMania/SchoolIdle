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

    public void AddConsumer(IAIInteractable consumer, System.Type resourceType)
    {
        List<IAIInteractable> list;
        System.Type resType = resourceType;

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

    public List<IAIInteractable> GetConsumers(System.Type resourceType)
    {
        List<IAIInteractable> list;
        _consumersByResource.TryGetValue(resourceType, out list);

        return list;
    }

}

