using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducerProvider : Singleton<ProducerProvider>
{
    Dictionary<System.Type, List<BaseProducer>> _producersByResource =
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
        }

    }


    public List<BaseProducer> GetProducers(System.Type resourceType)
    {
        List<BaseProducer> list;
        _producersByResource.TryGetValue(resourceType, out list);

        return list;
    }

}
