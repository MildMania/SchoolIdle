using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducerProvider : Singleton<ProducerProvider>
{
    Dictionary<System.Type, List<BaseProducer>> _producersByResource =
        new Dictionary<System.Type, List<BaseProducer>>();
    Dictionary<System.Type, List<BaseProducer>> _producersByRecentlyUsedResource =
        new Dictionary<System.Type, List<BaseProducer>>();

    private void Awake()
    {
        Init();
    }

    private void Init()
    {

    }

    public void AddProducer(BaseProducer producer, System.Type resourceType)
    {
        List<BaseProducer> list;
        System.Type resType = resourceType;

        _producersByResource.TryGetValue(resType, out list);

        if (list != null)
        {
            _producersByResource[resType].Add(producer);

        }
        else
        {
            _producersByResource[resType] = new List<BaseProducer>();
            _producersByResource[resType].Add(producer);

            _producersByRecentlyUsedResource[resType] = new List<BaseProducer>();
        }

    }


    public List<BaseProducer> GetProducers(System.Type resourceType)
    {
        List<BaseProducer> list;
        _producersByResource.TryGetValue(resourceType, out list);

        if (list.Count == 0)
        {
            _producersByResource[resourceType] = _producersByRecentlyUsedResource[resourceType];
            _producersByRecentlyUsedResource[resourceType] = new List<BaseProducer>();
            _producersByResource.TryGetValue(resourceType, out list);
        }

        return list;
    }

    public void ReserveProducer(System.Type type, BaseProducer producer)
    {
        _producersByRecentlyUsedResource[type].Add(producer);
        _producersByResource[type].Remove(producer);
    }

    public void ReleaseProducer(System.Type type, BaseProducer producer)
    {
        _producersByResource[type].Add(producer);
        _producersByRecentlyUsedResource[type].Remove(producer);
    }
}