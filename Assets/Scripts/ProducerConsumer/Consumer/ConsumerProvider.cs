using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerProvider : Singleton<ConsumerProvider>
{
    Dictionary<System.Type, List<BaseConsumer>> _consumersByResource =
        new Dictionary<System.Type, List<BaseConsumer>>();

    private void Awake()
    {
        Init();
    }

    private void Init()
    {

    }

    public void AddConsumer(BaseConsumer consumer, System.Type resourceType)
    {
        List<BaseConsumer> list;
        System.Type resType = resourceType;

        _consumersByResource.TryGetValue(resType, out list);

        if (list != null)
        {
            _consumersByResource[resType].Add(consumer);
        }
        else
        {
            _consumersByResource[resType] = new List<BaseConsumer>();
            _consumersByResource[resType].Add(consumer);
        }
    }

    public List<BaseConsumer> GetConsumers(System.Type resourceType)
    {
        List<BaseConsumer> list;
        _consumersByResource.TryGetValue(resourceType, out list);

        return list;
    }

}

